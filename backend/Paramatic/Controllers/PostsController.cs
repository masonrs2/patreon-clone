using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Paramatic.Models;
using Paramatic.Repositories;
using Paramatic.Services;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using Amazon.DynamoDBv2;

namespace Paramatic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly S3Service _s3Service;

        public PostController(IPostRepository repository, S3Service s3Service)
        {
            _repository = repository;
            _s3Service = s3Service;
        }

        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            try
            {
                // Try to get all posts - this will verify DB connection
                var posts = await _repository.GetAllAsync();
                return Ok(new { 
                    Status = "Connected", 
                    PostCount = posts.Count(),
                    Message = "Successfully connected to DynamoDB Posts table" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Status = "Error", 
                    Message = $"Failed to connect to DynamoDB: {ex.Message}" 
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _repository.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] Post post)
        {
            if(post.VideoContent != null && post.VideoContent.Length > 0)
            {
                using var stream = post.VideoContent.OpenReadStream();
                post.VideoUrl = await _s3Service.UploadFileAsync(stream, post.VideoContent.FileName);
            }

            await _repository.CreateAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.id }, post);
        }

        [HttpGet("debug")]
        public async Task<IActionResult> DebugAWSConnection()
        {
            try 
            {
                var client = new AmazonDynamoDBClient();
                var tables = await client.ListTablesAsync();
                
                return Ok(new { 
                    Region = client.Config.RegionEndpoint.SystemName,
                    Tables = tables.TableNames,
                    Message = "Successfully connected to AWS"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Error = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Paramatic.Models;
using Paramatic.Services;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using Amazon.DynamoDBv2;
using System.Text;  // For Encoding
using System.Security.Claims;  // For ClaimsIdentity and Claim
using Microsoft.IdentityModel.Tokens;  // For SecurityTokenDescriptor, SigningCredentials
using System.IdentityModel.Tokens.Jwt;  // For JwtSecurityTokenHandler

namespace Paramatic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] Post post)
        {
            try 
            {
                var createdPost = await _postService.CreatePostAsync(post, post.VideoContent);
                return CreatedAtAction(
                    nameof(GetPost), 
                    new { id = createdPost.id }, 
                    createdPost
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
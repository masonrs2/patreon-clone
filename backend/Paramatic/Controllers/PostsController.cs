using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Paramatic.Models;
using Paramatic.UnitOfWork;
using Paramatic.Services;
using System.IO;
using System.Threading.Tasks;

namespace Paramatic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
       private readonly IUnitOfWork _unitOfWork;
       private readonly S3Service _s3Service;

       public PostController(IUnitOfWork unitOfWork, S3Service s3Service)
       {
            _unitOfWork = unitOfWork;
            _s3Service = s3Service;
       }

       [HttpGet]
       public IActionResult GetAllPosts()
       {
        var posts = _unitOfWork.Repository<Post>().GetAll();
        return Ok(posts);
       }

       [HttpGet("{id}")]
       public IActionResult GetPost(int id)
       {
        var post = _unitOfWork.Repository<Post>().GetById(id);
        if (post == null)
            return NotFound();
        return Ok(post);
       }

       [HttpPost] 
       public async Task<IActionResult> CreatePost([FromForm] PostCreateDto postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                ImagePreview = postDto.ImagePreview,
                Description = postDto.Description,
                CreatorId = postDto.CreatorId
            };

            if(postDto.VideoContent != null && postDto.VideoContent.Length > 0)
            {
                using var stream = postDto.VideoContent.OpenReadStream();
                post.VideoUrl = await _s3Service.UploadFileAsync(stream, postDto.VideoContent.FileName);
            }

            _unitOfWork.Repository<Post>().Add(post);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
    }

    public class PostCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string ImagePreview { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatorId { get; set; } 
        public IFormFile? VideoContent { get; set; } 
    }

    public class PostUpdateDto 
    {
        public string Title { get; set; } = string.Empty;
        public string ImagePreview { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public IFormFile? VideoContent { get; set; } 
    }
}
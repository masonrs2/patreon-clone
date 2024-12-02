using Paramatic.Models;
using Paramatic.Repositories;
using Microsoft.AspNetCore.Http;

namespace Paramatic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly S3Service _s3Service;

        public PostService(IPostRepository repository, S3Service s3Service)
        {
            _repository = repository;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Post?> GetPostByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Post> CreatePostAsync(Post post, IFormFile? videoContent)
        {
            if (videoContent != null && videoContent.Length > 0)
            {
                // Upload to S3 and get the URL
                post.VideoUrl = await _s3Service.UploadVideoAsync(
                    videoContent,
                    post.CreatorId
                );
            }

            await _repository.CreateAsync(post);
            return post;
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            await _repository.UpdateAsync(post);
            return post;
        }

        public async Task DeletePostAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
} 
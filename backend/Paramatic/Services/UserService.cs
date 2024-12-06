using Paramatic.Models;
using Paramatic.Repositories;
using Microsoft.AspNetCore.Http;

namespace Paramatic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly S3Service _s3Service;

        public UserService(IUserRepository repository, S3Service s3Service) 
        {
            _repository = repository;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "User ID cannot be null or empty");
            }

            try
            {
                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found");
                }
                return user;
            }
            catch (Exception ex) when (ex is not ArgumentNullException && ex is not KeyNotFoundException)
            {
                // Log the error here if you have logging configured
                throw new ApplicationException($"Error retrieving user with ID {id}", ex);
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _repository.CreateAsync(user);
            return user;
        }

        public async Task<User> UpdateUserAsync(User user) 
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try {
                // If user doesnt exist, throw an error
                var existingUser = await _repository.GetByIdAsync(user.Id);
                if (existingUser == null) 
                {
                    throw new KeyNotFoundException($"User with ID {user.Id} not found");
                }

                await _repository.UpdateAsync(user);
                return user;
            }
            catch (Exception ex) when (ex is not ArgumentNullException && ex is not KeyNotFoundException)
            {
                throw new ApplicationException($"Error updating user with ID {user.Id}", ex);
            }
        }

        public async Task DeleteUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "User ID cannot be null or empty");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
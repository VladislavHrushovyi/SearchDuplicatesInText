using Auth.Domain.Entities;
using Auth.Domain.Models;

namespace Auth.UserDbContext.Repositories.UserRepository;

public interface IUserRepository
{
    public Task<UserEntity> Add(UserEntity entity);

    public Task<UserEntity> GetUser(UserModel model);
}
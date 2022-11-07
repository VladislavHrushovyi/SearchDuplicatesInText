using Auth.Domain.Entities;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.UserDbContext.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> Add(UserEntity entity)
    {
        var result =  await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<UserEntity> GetUser(UserModel model)
    {
        var user = await _context.Users.Where(u => u.Login == model.Email && u.Password == model.Password).FirstOrDefaultAsync();

        return user;
    }
}
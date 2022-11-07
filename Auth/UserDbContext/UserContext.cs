using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.UserDbContext;

public class UserContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }  
    
    public UserContext(DbContextOptions opt) : base(opt)
    {
        
    }
}
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using StudentPlanner.Core.Domain;

namespace Repositories;


/// <summary>
/// OBSOLETE: Replaced with UserManager
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ApplicationUser> AddUser(ApplicationUser user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;

    }

    public async Task<List<ApplicationUser>> GetAllUsers()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(usr => usr.Email == email);
    }

}

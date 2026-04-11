using System;
using Domain.Entities.Users;
using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(AppRole appRole)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppRole>> GetAllAsync(int offset, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<AppRole?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Remove(AppRole appRole)
    {
        throw new NotImplementedException();
    }

    public void Update(AppRole appRole)
    {
        throw new NotImplementedException();
    }
}

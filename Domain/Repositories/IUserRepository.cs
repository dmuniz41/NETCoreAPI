using System;
using Domain.Entities.Users;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<AppRole?> GetByIdAsync(Guid id);

    Task<List<AppRole>> GetAllAsync(int offset, int limit);

    void Add(AppRole appRole);

    void Update(AppRole appRole);

    void Remove(AppRole appRole);
}

using hma_api.Domain.Interfaces;
using hma_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Infraestructure.Repositories
{
  public class CrudRepository<T> : ICrudRepository<T> where T : class
  {
    private readonly MyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public CrudRepository(MyDbContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAll()
    {
      return await _dbSet.ToListAsync();
    }
    public async Task<T> GetById(int id)
    {
      return await _dbSet.FindAsync(id);
    }
    public async Task<T> Add(T entity)
    {
      await _dbSet.AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;
    }
    public async Task<T> Update(T entity)
    {
      _dbSet.Update(entity);
      await _context.SaveChangesAsync();
      return entity;
    }
    public async Task<bool> Delete(int id)
    {
      var entity = await GetById(id);
      if (entity == null)
      {
        return false;
      }

      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}

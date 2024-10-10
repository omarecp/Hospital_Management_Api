using hma_api.Application.Interfaces;
using hma_api.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.Services
{
  public class CrudService<T> : ICrudService<T> where T : class
  {
    private readonly ICrudRepository<T> _repository;

    public CrudService(ICrudRepository<T> repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
      return await _repository.GetAll();
    }

    public async Task<T> GetById(int id)
    {
      return await _repository.GetById(id);
    }

    public async Task<T> Add(T entity)
    {
      return await _repository.Add(entity);
    }

    public async Task<T> Update(T entity)
    {
      return await _repository.Update(entity);
    }

    public async Task<bool> Delete(int id)
    {
      return await _repository.Delete(id);
    }

  }
}

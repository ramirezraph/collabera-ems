using EMS.Models.Interfaces;

namespace EMS.Persistence.Repositories;

public interface IRepository<T> where T : IEntity
{
    ICollection<T> GetAll();
    T? GetById(Guid Id);
    T? Add(T entity);
    T? Update(Guid Id, T updatedEntity);
    T? Delete(Guid Id);
}
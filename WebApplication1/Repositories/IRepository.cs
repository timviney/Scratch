namespace WebApplication1.Repositories;

// Repository pattern example
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IEnumerable<TEntity> GetAll();
    TEntity? Get(int id);
    TEntity Add<TEntityDto>(TEntityDto dto) where TEntityDto : IEntityDto<TEntity>;
    TEntity? Delete(int id);
}

public interface IEntity
{
    int Id { get; }
}
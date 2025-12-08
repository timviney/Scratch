namespace WebApplication1.Repositories;

public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly Dictionary<int, TEntity> _entities = new();
    
    private int _nextId = 0;
    
    private int NextId()
    {
        return Interlocked.Increment(ref _nextId);
    }
    
    public IEnumerable<TEntity> GetAll()
    {
        return _entities.Values.ToList();
    }

    public TEntity? Get(int id)
    {
        _entities.TryGetValue(id, out var entity);
        return entity;
    }

    public TEntity Add<TEntityDto>(TEntityDto dto) where TEntityDto : IEntityDto<TEntity> 
    {
        int id = NextId();
        var entity = dto.Create(id);
        _entities.Add(id, entity);
        return entity; 
    }

    public TEntity? Delete(int id)
    {
        return _entities.Remove(id, out var entity) ? entity : null;
    }
}
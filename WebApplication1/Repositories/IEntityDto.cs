namespace WebApplication1.Repositories;

public interface IEntityDto<out TEntity>
{
    TEntity Create(int id);
}
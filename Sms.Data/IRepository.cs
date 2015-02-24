namespace Emails.Data
{
    public interface IRepository
    {
        bool Insert<T>(T model);
        bool Update<T>(T originalEntity, T updatedEntity);
        bool Delete(int id);
    }
}
namespace API.Data.Repositories
{
    public interface IEmployeeRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}

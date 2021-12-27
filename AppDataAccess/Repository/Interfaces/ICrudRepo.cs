using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Interfaces
{
    public interface ICrudRepo
    {
        Task<bool> Add<T>(T entity);
        Task<bool> Delete<T>(T entity);
        Task<bool> Update<T>(T entity);
        Task<bool> SaveChanges();
        Task<int> RowCount();
    }
}

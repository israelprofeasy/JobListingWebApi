using JobWebApi.AppDataAccess.DataContext;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Implementations
{
    public class CvUpLoadRepository : ICVUpload
    {
        private readonly JobDbContext _ctx;

        public CvUpLoadRepository(JobDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add<T>(T entity)
        {
            await _ctx.AddAsync(entity);
            return await SaveChanges();
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _ctx.Remove(entity);
            return await SaveChanges();
        }

        public async Task<IEnumerable<CvUpload>> GetAllUploads()
        {
            return await _ctx.CvUpload.ToListAsync();
        }

        public async Task<CvUpload> GetUpload(string userId)
        {
            return await _ctx.CvUpload.Where(x => x.AppUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<int> RowCount()
        {
            return await _ctx.CvUpload.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() >0;
        }

        public async Task<bool> Update<T>(T entity)
        {
            _ctx.Update(entity);
            return await SaveChanges();
        }
    }
}

using JobWebApi.AppDataAccess.DataContext;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Implementations
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly JobDbContext _ctx;

        public IndustryRepository(JobDbContext ctx)
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

        public async Task<IEnumerable<Industry>> GetAll()
        {
            return await _ctx.Industry.ToListAsync();
        }

        public async Task<Industry> GetIndustryById(string id)
        {
            return await _ctx.Industry.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Industry> GetIndustryByName(string name)
        {
            return await _ctx.Industry.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<int> RowCount()
        {
            return await _ctx.Industry.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update<T>(T entity)
        {
            _ctx.Update(entity);
            return await SaveChanges();
        }
    }
}

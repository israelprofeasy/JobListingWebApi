using JobWebApi.AppDataAccess.DataContext;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Implementations
{
    public class JobApplicationRepository : IJobApplicationRepo
    {
        private readonly JobDbContext _ctx;

        public JobApplicationRepository(JobDbContext ctx)
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

        public Task<IEnumerable<AppUser>> JobApplications(string JobId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> RowCount()
        {
            return await _ctx.JobApplication.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public Task<bool> Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Job>> UserApplications(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

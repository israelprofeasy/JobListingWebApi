using JobWebApi.AppDataAccess.DataContext;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.Enums;
using JobWebApi.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext _ctx;

        public JobRepository(JobDbContext ctx)
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

        public async Task<Job> GetJobById(string id)
        {
            return await _ctx.Job.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await _ctx.Job.ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByCategory(string categoryId)
        {
            return await _ctx.Job.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByIndustry(string industryId)
        {
            return await _ctx.Job.Where(c => c.IndustryId == industryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByLocation(Locations location)
        {
            return await _ctx.Job.Where(x => x.Location == location).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByName(string name)
        {
            return await _ctx.Job.Where(x => x.JobTitle.Contains(name) || x.Company.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByNature(JobNature jobNature)
        {
            return await _ctx.Job.Where(x => x.JobNature == jobNature).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            return await _ctx.Job.Where(x => x.MinimumSalary >= minimum && x.MaximumSalary <= maximum).OrderBy(x => x.MinimumSalary).ToListAsync(); 
        }

        public async Task<int> RowCount()
        {
            return await _ctx.Job.CountAsync();
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

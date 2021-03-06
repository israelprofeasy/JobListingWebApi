using JobWebApi.AppModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobWebApi.AppDataAccess.DataContext
{
    public class JobDbContext : IdentityDbContext<AppUser>
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {

        }
       public DbSet<Job> Job { get; set; }
       public DbSet<Category> Category { get; set; }
       public DbSet<Industry> Industry { get; set; }
       public DbSet<CvUpload> CvUpload { get; set; }
       public DbSet<JobApplication> JobApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<JobApplication>().HasOne(x => x.Job).WithMany(c => c.AppliedJobs).HasForeignKey(d => d.JobId);
            builder.Entity<JobApplication>().HasOne(x => x.AppUser).WithMany(c => c.AppliedJobs).HasForeignKey(d => d.AppUserId);
            builder.Entity<Job>().Property(x => x.MinimumSalary).HasColumnType("decimal(18,4)");
            builder.Entity<Job>().Property(x => x.MaximumSalary).HasColumnType("decimal(18,4)");

        }
    }
}

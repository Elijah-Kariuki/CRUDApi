using Microsoft.EntityFrameworkCore;
using CRUDApi.Models;

namespace CRUDApi.Data
{
    public class IndeedJobsContext : DbContext
    {
        public IndeedJobsContext(DbContextOptions<IndeedJobsContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
    }
}

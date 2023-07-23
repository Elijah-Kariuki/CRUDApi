using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDApi.Models;

namespace CRUDApi.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<string> GetJobsFromAPIAsync(string keyword, string location);
        Task<Job?> GetJobAsync(int id);
        Task AddJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(int id);
        bool JobExists(int id);
    }
}


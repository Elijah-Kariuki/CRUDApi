using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CRUDApi.Data;
using CRUDApi.Models;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CRUDApi.Repositories
{

    public class JobRepository : IJobRepository
    {
        private readonly IndeedJobsContext _context;
        private readonly IConfiguration _configuration;
        public JobRepository(IndeedJobsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<string> GetJobsFromAPIAsync()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://indeed-jobs-api.p.rapidapi.com/indeed-us/?offset=0&keyword=software+engineer&location=tennessee"),
                    Headers =
            {
                { "X-RapidAPI-Key", _configuration["RapidAPI:Key"] },
                { "X-RapidAPI-Host", _configuration["RapidAPI:Host"] },
            },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonResponse); // Parse JSON response into List<Job>

                        if (jobs != null && jobs.Count > 0)
                        {
                            _context.Jobs.AddRange(jobs); // Add new jobs to the context
                            await _context.SaveChangesAsync(); // Save changes

                            Console.WriteLine($"Successfully added {jobs.Count} jobs to the database."); // Log success
                        }
                        else
                        {
                            Console.WriteLine("Parsed job list is null or empty."); // Log potential issue with deserialization
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log exception during deserialization or database save
                        Console.WriteLine($"Exception encountered: {ex.Message}");
                    }

                    return jsonResponse;
                }
            }
        }




        public async Task<Job?> GetJobAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddJobAsync(Job job)
        {
            _context.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            _context.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.Interfaces;
using Newtonsoft.Json; // Add this using statement for Newtonsoft.Json

namespace CRUDApi.Controllers
{
    public class JobController : Controller
    {
        private readonly IndeedJobsContext _context;
        private readonly IJobRepository _jobRepository;

        public JobController(IndeedJobsContext context, IJobRepository jobRepository)
        {
            _context = context;
            _jobRepository = jobRepository;
        }

        // GET: Job
        // GET: Jobs
        [ResponseCache(Duration =1800)]
        public async Task<IActionResult> Index(JobSearchRequest searchRequest)
        {
            try
            {
                await _jobRepository.GetJobsFromAPIAsync(searchRequest); // Fetch jobs from API and save to the local database
                var jobs = await _jobRepository.GetJobsAsync(); // Fetch jobs from the local database
                return View(jobs);
            }
            catch (Exception ex)
            {
                return Problem($"Error fetching jobs from API: {ex.Message}");
            }
        }

        

        // GET: Job/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyLogoUrl,CompanyName,CompanyRating,CompanyReviewLink,CompanyReviews,Date,JobLocation,JobTitle,JobUrl,MultipleHiring,NextPage,PageNumber,Position,Salary,UrgentlyHiring")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Job/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyLogoUrl,CompanyName,CompanyRating,CompanyReviewLink,CompanyReviews,Date,JobLocation,JobTitle,JobUrl,MultipleHiring,NextPage,PageNumber,Position,Salary,UrgentlyHiring")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Job/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'IndeedJobsContext.Jobs' is null.");
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Job/Search
        public IActionResult Search()
        {
            return View(new JobSearchRequest());
        }

        // POST: Job/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(JobSearchRequest searchRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _jobRepository.GetJobsFromAPIAsync(searchRequest); // Fetch jobs from API and save to the local database based on search parameters
                    var jobs = await _jobRepository.GetJobsAsync(); // Fetch jobs from the local database
                    return View(nameof(Index), jobs);
                }
                catch (Exception ex)
                {
                    return Problem($"Error fetching jobs from API: {ex.Message}");
                }
            }
            return View(searchRequest);
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

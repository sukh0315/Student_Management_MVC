using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.BusinessLayer;
using Student_Management_MVC.Models;

namespace Student_Management_MVC.Controllers
{
    //Authorized controller for Results management.
    [Authorize]
    public class ResultsController : Controller
    {
        private readonly Student_Management_MVCContext _context;

        public ResultsController(Student_Management_MVCContext context)
        {
            _context = context;
        }

        // GET: Results
        //Gets all results using a lamda query.
        public IActionResult Index()
        {
            var student_Management_MVCContext = _context.Result.Include(r => r.Course).Include(r => r.Student);
            return View( student_Management_MVCContext.ToList());
        }

        // GET: Results/Details/5
        //Gets the details of results using  a lamda query.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result =  _context.Result
                .Include(r => r.Course)
                .Include(r => r.Student)
                .FirstOrDefault(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        //Gets the create form.
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName");
            ViewData["Results"] = new SelectList(Enum.GetValues(typeof(Grade)));
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds the results to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,CourseId,StudentId,Grade")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", result.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName", result.StudentId);
            return View(result);
        }

        // GET: Results/Edit/5
        //Gets the results using a lamda query for update.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = (from results in _context.Result
                          where results.Id == id
                          select results).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", result.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName", result.StudentId);
            ViewData["Results"] = new SelectList(Enum.GetValues(typeof(Grade)), result.Grade);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Update the results 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CourseId,StudentId,Grade")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", result.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName", result.StudentId);
            return View(result);
        }

        // GET: Results/Delete/5
        //Gets results for delete using a lamda query.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _context.Result
                .Include(r => r.Course)
                .Include(r => r.Student)
                .FirstOrDefault(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        //Deletes the results uses a linq query to get the result.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = (from results in _context.Result
                          where results.Id == id
                          select results).FirstOrDefault();
            _context.Result.Remove(result);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the results using a lamda query.
        private bool ResultExists(int id)
        {
            return _context.Result.Any(e => e.Id == id);
        }
    }
}

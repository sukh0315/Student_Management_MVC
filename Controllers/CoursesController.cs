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
    //Authorised Course controller 
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly Student_Management_MVCContext _context;

        public CoursesController(Student_Management_MVCContext context)
        {
            _context = context;
        }

        // GET: Courses
        //Get All courses using a linq query.
        public IActionResult Index()
        {
            return View((from courses in _context.Course select courses).ToList());
        }

        // GET: Courses/Details/5
        //Get Details of  a course using a lamda query
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course =  _context.Course
                .FirstOrDefault(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        //Gets the  create form.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds  a course to database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CourseName,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        //Gets the course details for edit using a linq query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = (from courses in _context.Course
                          where courses.Id == id
                          select courses).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Update the course 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CourseName,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        //Gets the course to delete uses a lamda query to fetch the course.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course =  _context.Course
                .FirstOrDefault(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        //Removes the course uses a linq query to select the course.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = (from courses in _context.Course
                          where courses.Id == id
                          select courses).FirstOrDefault();
            _context.Course.Remove(course);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the course using a lamda query.
        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}

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
    //Authorised Enrollements controller
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly Student_Management_MVCContext _context;

        public EnrollmentsController(Student_Management_MVCContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        //Gets all the entrollments using a lamda query.
        public IActionResult Index()
        {
            var student_Management_MVCContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View( student_Management_MVCContext.ToList());
        }

        // GET: Enrollments/Details/5
        //Gets the details of the enrollment using a lamda query.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment =  _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefault(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        //Gets the Add enrollment form.
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseSerialId");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentSerialId");
            ViewData["Semester"] = new SelectList(Enum.GetValues(typeof(Semester)));
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds the enrollment  to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CourseId,StudentId,Semester")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName", enrollment.StudentId);
           
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        //Gets the entrollment for update using a linq query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = (from enrollments in _context.Enrollment
                              where enrollments.Id == id
                              select enrollments).FirstOrDefault();
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseSerialId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentSerialId", enrollment.StudentId);
            ViewData["Semester"] = new SelectList(Enum.GetValues(typeof(Semester)), enrollment.Semester);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the enrollment.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CourseId,StudentId,Semester")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "StudentName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        //Gets the entrollment for delete using a lamda query.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment =  _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefault(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        //Deletes the enrollment uses a linq query to get the enrollment.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = (from enrollments in _context.Enrollment
                              where enrollments.Id == id
                              select enrollments).FirstOrDefault();
            _context.Enrollment.Remove(enrollment);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the enrollment using a lamda query.
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}

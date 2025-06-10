using Microsoft.AspNetCore.Mvc;
using StudentManagerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagerWeb.Controllers
{
    public class StudentController : Controller
    {
        public readonly AppDbContext _db;//add class AppdbContext
        public StudentController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var StudentList = _db.Students.ToList();//hiển thị danh sách SV

            return View(StudentList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();

                TempData["success"] = "Thêm thành công";
                return RedirectToAction("index");
            }
            return View(student);
        }
        public IActionResult Update(int id)
        {
            var student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                var existsStudent = _db.Students.Find(student.Id);
                if (existsStudent == null)
                {
                    return NotFound();
                }
                existsStudent.FullName = student.FullName;
                existsStudent.Age = student.Age;
                existsStudent.Mail = student.Mail;
                _db.SaveChanges();

                TempData["success"] = "Updated success";
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public IActionResult Delete(int id)
        {
            var student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var existsStudent = _db.Students.Find(id);
                if (existsStudent == null)
                {
                    return NotFound();
                }
                _db.Students.Remove(existsStudent);
                _db.SaveChanges();

                TempData["success"] = "Deleted success";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}

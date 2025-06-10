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
        // Kiểm tra email có tồn tại không
        private bool IsEmailExists(string email, int? excludeId = null)
        {
            return _db.Students.Any(s => s.Mail.ToLower() == email.ToLower() &&
                                   (excludeId == null || s.Id != excludeId));
        }
        public IActionResult Index()
        {
            var StudentList = _db.Students.ToList();//hiển thị danh sách SV

            // Tính tổng số sinh viên và tổng số tuổi
            ViewBag.TotalStudents = StudentList.Count();
            ViewBag.AverageAge = StudentList.Count() > 0 ? Math.Round(StudentList.Average(s => s.Age), 0) : 0;

            return View(StudentList);
        }
        // Action riêng để hiển thị thống kê
        public IActionResult Statistics()
        {
            var students = _db.Students.ToList();

            var stats = new
            {
                AverageAge = students.Count() > 0 ? students.Average(s => s.Age) : 0,
                MinAge = students.Count() > 0 ? students.Min(s => s.Age) : 0,
                MaxAge = students.Count() > 0 ? students.Max(s => s.Age) : 0
            };

            return View(stats);
        }

        // API endpoint để lấy thống kê dạng JSON
        [HttpGet]
        public IActionResult GetStatistics()
        {
            var students = _db.Students.ToList();

            var stats = new
            {
                TotalStudents = students.Count(),
                MinAge = students.Count() > 0 ? students.Min(s => s.Age) : 0,
                MaxAge = students.Count() > 0 ? students.Max(s => s.Age) : 0
            };

            return Json(stats);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (IsEmailExists(student.Mail))
            {
                ModelState.AddModelError("Mail", "Email này đã tồn tại. Vui lòng sử dụng email khác.");
            }
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

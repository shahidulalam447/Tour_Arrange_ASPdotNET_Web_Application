using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Arrange.Models.ViewModels;
using Tour_Arrange.Models;

namespace Tour_Arrange.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TourArrangeDbContext db = new TourArrangeDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(c => c.ManagementEntries.Select(b => b.Department)).OrderByDescending(x => x.EmployeeId).ToList();
            return View(employees);
        }
        public ActionResult AddNewDepartment(int? id)
        {
            ViewBag.department = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewDepartment");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeVM employeeVM, int[] departmentId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    EmployeeName = employeeVM.EmployeeName,
                    BirthDate = employeeVM.BirthDate,
                    JoinDate = employeeVM.JoinDate,
                    Email = employeeVM.Email
                };
                //for Image
                HttpPostedFileBase file = employeeVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    employee.EmployeePicture = filePath;
                }

                //for spot
                foreach (var item in departmentId)
                {
                    ManagementEntry managementEntry = new ManagementEntry()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        DepartmentId = item,
                    };
                    db.ManagementEntries.Add(managementEntry);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Employee employee = db.Employees.First(x => x.EmployeeId == id);
            var employeeDepart = db.ManagementEntries.Where(x => x.EmployeeId == id).ToList();

            EmployeeVM employeeVM = new EmployeeVM()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                BirthDate = employee.BirthDate,
                JoinDate = employee.JoinDate,
                Email = employee.Email,
                EmployeePicture = employee.EmployeePicture,
                //PicturePath=employee.PicturePath                
            };
            if (employeeDepart.Count() > 0)
            {
                foreach (var item in employeeDepart)
                {
                    employeeVM.DepartmentList.Add(item.DepartmentId);
                }
            }
            return View(employeeVM);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeVM employeeVM, int[] departmentId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = employeeVM.EmployeeId,
                    EmployeeName = employeeVM.EmployeeName,
                    BirthDate = employeeVM.BirthDate,
                    JoinDate = employeeVM.JoinDate,
                    Email = employeeVM.Email
                };
                //for Image
                HttpPostedFileBase file = employeeVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    employee.EmployeePicture = filePath;
                }
                else
                {
                    employee.EmployeePicture = employeeVM.EmployeePicture;
                }
                //Department
                var existsDepartmentEntry = db.ManagementEntries.Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                //Delete
                foreach (var managementEntry in existsDepartmentEntry)
                {
                    db.ManagementEntries.Remove(managementEntry);
                }
                //save All Spot
                foreach (var item in departmentId)
                {
                    ManagementEntry managementEntry = new ManagementEntry()
                    {
                        EmployeeId = employee.EmployeeId,
                        DepartmentId = item
                    };
                    db.ManagementEntries.Add(managementEntry);
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            var employee = db.Employees.Find(id);
            var existsDepartmentEntry = db.ManagementEntries.Where(x => x.EmployeeId == id).ToList();

            foreach (var managementEntry in existsDepartmentEntry)
            {
                db.ManagementEntries.Remove(managementEntry);
            }
            db.Entry(employee).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
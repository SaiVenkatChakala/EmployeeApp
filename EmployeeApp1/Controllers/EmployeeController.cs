using EmployeeApp1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApp1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());

        }
        IEnumerable<EmployeeTB> GetAllEmployee()
        {
            using (DBModel db = new DBModel())
            {
                return db.EmployeeTBs.ToList<EmployeeTB>();
            }

        }
        public ActionResult AddOrEdit(int id = 0)
        {
            EmployeeTB emp = new EmployeeTB();
            if (id != 0)
            {
                using (DBModel db = new DBModel())
                {
                    emp = db.EmployeeTBs.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeTB>();
                }
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult AddOrEdit(EmployeeTB emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.EmployeeID == 0)
                {
                    db.EmployeeTBs.Add(emp);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                EmployeeTB emp = db.EmployeeTBs.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeTB>();
                db.EmployeeTBs.Remove(emp);
                db.SaveChanges();
            }
            return RedirectToAction("ViewAll");
        }
    }
}

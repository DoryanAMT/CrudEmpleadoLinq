using CrudEmpleadoLinq.Models;
using CrudEmpleadoLinq.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace CrudEmpleadoLinq.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryEmpleados repo;
        public EmpleadosController()
        {
            this.repo = new RepositoryEmpleados();
        }
        public IActionResult Index()
        {
            List <Empleado> empleados = this.repo.GetEmpleados();
            ViewData["OFICIOS"] = this.repo.GetOficios();
            return View(empleados);
        }
        [HttpPost]
        public IActionResult Index
            (string oficio)
        {
            List<Empleado> empleados = this.repo.GetEmpleadosOficios(oficio);
            ViewData["OFICIOS"] = this.repo.GetOficios();
            return View(empleados);
        }
        public IActionResult Details
            (int empNo)
        {
            Empleado empleado = this.repo.FindEmpleado(empNo);
            return View(empleado);
        }
        [HttpGet]
        public IActionResult Update
            (int empNo)
        {
            Empleado empleado = this.repo.FindEmpleado(empNo);
            return View(empleado);
        }
        [HttpPost]
        public IActionResult Update
            (Empleado empleado)
        {
            this.repo.UpdateEmpleado(empleado);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete
            (int empNo)
        {
            this.repo.DeleteEmpleado(empNo);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DEPARTAMENTOS"] = this.repo.GetDepartamentos();
            return View();
        }
        [HttpPost]
        public IActionResult Create
             (Empleado empleado)
        {
            this.repo.CreateEmpleado(empleado);
            return RedirectToAction("Index");
        }
    }
}

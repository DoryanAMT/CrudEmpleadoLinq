using CrudEmpleadoLinq.Models;
using CrudEmpleadoLinq.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudEmpleadoLinq.Controllers
{
    public class DepartamentosController : Controller
    {
        RepositoryDepartamento repo;
        public DepartamentosController()
        {
            this.repo = new RepositoryDepartamento();
        }
        public IActionResult Index()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }
        public IActionResult Details
            (int deptNo)
        {
            Departamento departamento = this.repo.FindDepartamento(deptNo);
            return View(departamento);
        }
        public IActionResult Update
            (int deptNo)
        {
            Departamento departamento = this.repo.FindDepartamento(deptNo);
            return View(departamento);
        }
        [HttpPost]
        public IActionResult Update
            (Departamento departamento)
        {
            this.repo.UpdateDepartamento(departamento);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create
            (Departamento departamento)
        {
            this.repo.CreateDepartamento(departamento);
            return RedirectToAction("Index");
        }
        public IActionResult Delete
            (int deptNo)
        {
            this.repo.DeleteDepartamento(deptNo);
            return RedirectToAction("Index");
        }
    }
}

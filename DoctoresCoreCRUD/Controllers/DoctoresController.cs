using DoctoresCoreCRUD.Data;
using DoctoresCoreCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctoresCoreCRUD.Controllers
{
    public class DoctoresController : Controller
    {
        DoctoresContext context;
        public DoctoresController(DoctoresContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Doctor> listadoctores = this.context.GetDoctores();
            return View(listadoctores);
        }
        public IActionResult Details(int iddoctor)
        {
            Doctor doctor = this.context.FindDoctor(iddoctor);
            return View(doctor);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int iddoctor, String apellido, String especialidad, int salario)
        {
            this.context.InsertDoctor(iddoctor, apellido, especialidad, salario);
            //para que nos envie a otra vista
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddoctor)
        {
            Doctor doctor = this.context.FindDoctor(iddoctor);
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Edit(int iddoctor, String apellido, String especialidad, int salario)
        {
            this.context.UpdateDoctor(iddoctor, apellido, especialidad, salario);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddoctor)
        {
            this.context.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }
    }
}

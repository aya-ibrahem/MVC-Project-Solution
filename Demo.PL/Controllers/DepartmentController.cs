using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            //ViewData["Message"] = "Hello View Data";
            //ViewBag.Message = "Hello View Bag";
            var department =await _departmentRepository.GetAll();    
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.Add(department);
                TempData["Message"] = "Department is added Successfully";
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public async Task<IActionResult> Details (int? id ,string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var department =await _departmentRepository.Get(id.Value);
            if(department == null)
                return NotFound();
            return View(ViewName, department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
            //if (id == null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null)
            //    return BadRequest();
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                   await _departmentRepository.Update(department);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            //if (id == null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null)
            //    return BadRequest();
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                   await _departmentRepository.Delete(department);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);

        }


    }
    }


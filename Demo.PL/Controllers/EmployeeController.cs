using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            var Employees =Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchValue))
                 Employees =await _unitOfWork.EmployeeRepository.GetAll();
            else
                 Employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);
            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(mappedEmps);
        }
        public async  Task<IActionResult> Create()
        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                //var mappedEmp = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Salary = employeeVM.Salary,
                //    Address = employeeVM.Address,
                //    Age = employeeVM.Age,
                //    IsActive = employeeVM.IsActive,
                //    Email = employeeVM.Email,
                //    HireDate = employeeVM.HireDate,
                //    PhoeNumber = employeeVM.PhoeNumber,
                //    DepartmentId = employeeVM.DepartmentId,

                //};
                employeeVM.ImageName = DocumentSettings.upload(employeeVM.Image, "images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.Add(mappedEmp);
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var employee =await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee == null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);    
            return View(ViewName, MappedEmployee);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
            //if (id == null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee == null)
            //    return BadRequest();
            //return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                   await _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
            //if (id == null)
            //    return BadRequest();
            //var employee = _employeeRepository.Get(id.Value);
            //if (employee == null)
            //    return BadRequest();
            //return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                   int count =await _unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                    if (count >0)
                    {
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);

        }

    }
}

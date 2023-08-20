using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
        public async Task<IActionResult> Index(string SearchValue)
        {
            var users = Enumerable.Empty<ApplicationUser>().ToList();
            if (string.IsNullOrEmpty(SearchValue))
                users.AddRange( _userManager.Users);
            else
               users.Add(await _userManager.FindByEmailAsync(SearchValue)); 
            return View(users);
        }
        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var mappedEmp = new Employee()
        //        //{
        //        //    Name = employeeVM.Name,
        //        //    Salary = employeeVM.Salary,
        //        //    Address = employeeVM.Address,
        //        //    Age = employeeVM.Age,
        //        //    IsActive = employeeVM.IsActive,
        //        //    Email = employeeVM.Email,
        //        //    HireDate = employeeVM.HireDate,
        //        //    PhoeNumber = employeeVM.PhoeNumber,
        //        //    DepartmentId = employeeVM.DepartmentId,

        //        //};
        //        employeeVM.ImageName = DocumentSettings.upload(employeeVM.Image, "images");
        //        var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
        //        await _unitOfWork.EmployeeRepository.Add(mappedEmp);
        //        return RedirectToAction("Index");
        //    }
        //    return View(employeeVM);
        //}
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);    
            if (user == null)
                return NotFound();
            return View(ViewName, user);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser UpdatedUser)
        {
            if (id != UpdatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                  var user = await _userManager.FindByIdAsync(id);
                  user.UserName = UpdatedUser.UserName;
                  user.PhoneNumber = UpdatedUser.PhoneNumber;
                  await _userManager.UpdateAsync(user);
                  return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(UpdatedUser);

        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
        {
            if (id != deletedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(deletedUser);

        }
    }
}

using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationRepository _repoAllocation;
        private readonly ILeaveTypeRepository _repoType;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public LeaveAllocationController(ILeaveTypeRepository repoType,
            ILeaveAllocationRepository repoAllocation, IMapper mapper, UserManager<Employee> userManager)
        {
            _repoType = repoType;
            _repoAllocation = repoAllocation;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: LeaveAllocationController
        public async Task<ActionResult> Index(int numUpdated=0)
        {
            var types = await _repoType.FindAll();           
            var mapTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(types.ToList());
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mapTypes,
                NumberUpdated = numUpdated
            };
            return View(model);
        }

        public async Task< ActionResult> SetLeave(int id)
        {
            var leaveTyppe = await _repoType.FindById(id);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            int NumberUpdated = 0;
            foreach (Employee emp in employees)
            {
                if (await _repoAllocation.CheckAllocation(id, emp.Id))
                    continue;
                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leaveTyppe.DefaultDays,
                    Period = DateTime.Now.Year
                };
                //mapping to save
                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                await _repoAllocation.Create(leaveAllocation);
                NumberUpdated++;
            }
            return RedirectToAction(nameof(Index),new { numUpdated = NumberUpdated });
        }

        public async Task<ActionResult> ListEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }
                
        public async Task<ActionResult> Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(await _userManager.FindByIdAsync(id));
            var period = DateTime.Now.Year;
            var allocations = _mapper.Map<List<LeaveAllocationVM>>(await _repoAllocation.GetLeaveAllocationsByEmployee(id));
            return View(new ViewLeaveAllocationVM { Employee = employee, 
                EmployeeId = id, LeaveAllocations = allocations } );
        }
    

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public async Task<ActionResult> Edit (int id)
        {
            var leaveAllocation = _mapper.Map<EditLeaveAllocationVM>(await _repoAllocation.FindById(id));
            return View(leaveAllocation);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVM model )
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var previous = _mapper.Map<LeaveAllocation>(await _repoAllocation.FindById(model.Id));
                var allocation = _mapper.Map<LeaveAllocation>(model);
                previous.NumberOfDays = allocation.NumberOfDays;
                var success = await _repoAllocation.Update(previous);
                if (!success)
                {
                    ModelState.AddModelError("", "Error on database");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new {id = model.EmployeeId });
            }
           catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return View(model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LeaveTypesController : Controller
    {

        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        // GET: LeaveTypesController
        public ActionResult Index()
        {
            var types = _repo.FindAll();
            var models = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(types.ToList());
            return View(models);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leavetype = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leavetype);
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var success = _repo.Create(leaveType);
                if (!success)
                {
                    ModelState.AddModelError("", "Error on database");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return View(model);                
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                var success = _repo.Update(leaveType);
                if (!success)
                {
                    ModelState.AddModelError("", "Error on database");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var leaveType = _repo.FindById(id);
                var success = _repo.Delete(leaveType);
                if (leaveType == null)
                {
                    return NotFound();
                }
                if (!success)
                {

                    return BadRequest();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return BadRequest();
            }

            //if (!_repo.IsExists(id))
            //{
            //    return NotFound();
            //}
            //var leaveType = _repo.FindById(id);
            //var model = _mapper.Map<LeaveTypeVM>(leaveType);
            //return View(model);
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int id)
        {
            try
            {
                var leaveType = _repo.FindById(id);
                var success = _repo.Delete(leaveType);
                if(leaveType==null)
                {
                    return NotFound();
                }
                if (!success)
                {
                   
                    return View(leaveType);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return View();
            }
        }
    }
}

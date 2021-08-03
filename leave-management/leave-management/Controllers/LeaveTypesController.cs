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

        //private readonly ILeaveTypeRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveTypesController(IUnitOfWork unitWork, IMapper mapper)
        {
            //_repo = repo;
            _unitOfWork = unitWork;
            _mapper = mapper;
        }
        
        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            //var types = await _repo.FindAll();
            var types = await _unitOfWork.LeaveTypeRepo.FindAll();
            var models = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(types.ToList());
            return View(models);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (! await _unitOfWork.LeaveTypeRepo.IsExists(x => x.Id==id)) //_repo.IsExists(id)
            {
                return NotFound();
            }
            var leavetype = await _unitOfWork.LeaveTypeRepo.Find(x => x.Id == id); // _repo.FindById(id)
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
        public async Task<ActionResult> Create(LeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                 await _unitOfWork.LeaveTypeRepo.Create(leaveType); //_repo.Create(leaveType)
                var success = await _unitOfWork.Save();
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
        public async Task<ActionResult> Edit(int id)
        {
            if (! await _unitOfWork.LeaveTypeRepo.IsExists(x => x.Id == id)) //_repo.IsExists(id)
            {
                return NotFound();
            }
            var leaveType = await _unitOfWork.LeaveTypeRepo.Find(x => x.Id == id); //_repo.FindById(id)
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                await _unitOfWork.LeaveTypeRepo.Update(leaveType); //_repo.Update(leaveType)
                var success = await _unitOfWork.Save();
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var leaveType = await _unitOfWork.LeaveTypeRepo.Find(expression: x => x.Id == id); //_repo.FindById(id);
                 await _unitOfWork.LeaveTypeRepo.Delete(leaveType); //_repo.Delete(leaveType);
                var success = await _unitOfWork.Save();
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
                ModelState.AddModelError("", "Error on the elimination:" + ex.Message);
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
        public async Task<ActionResult> DeletePOST(int id)
        {
            try
            {
                var leaveType = await _unitOfWork.LeaveTypeRepo.Find(x => x.Id == id); //_repo.FindById(id)
                 await _unitOfWork.LeaveTypeRepo.Delete(leaveType); //_repo.Delete(leaveType);
                var success = await _unitOfWork.Save(); 
                if (leaveType==null)
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

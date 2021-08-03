using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveHistoryController : Controller
    {

        //private readonly ILeaveHistoryRepository _repoHistory;
        //private readonly ILeaveTypeRepository _repoType;
        //private readonly ILeaveAllocationRepository _repoAllocation;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        
        public LeaveHistoryController(IUnitOfWork unitWork, IMapper mapper, UserManager<Employee> userManager)
        {
            //_repoHistory = repoHistory;
            //_repoType = repoType;
            // _repoAllocation = repoAllocation;
            _unitOfWork = unitWork;
              _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveHistoryController
        public async Task<ActionResult> Index()
        {
            var requests = await _unitOfWork.LeaveHistoryRepo.FindAll(includes: q => q.Include(x => x.RequestingEmployee).
                                                                                       Include(x => x.LeaveType)); //_repoHistory.FindAll()
            var requestsModel = _mapper.Map<List<LeaveHistoryVM>>(requests);
            var model = new AdminLeaveHistoryVM
            {
                TotalRequest = requestsModel.Count,
                ApprovedRequest = requestsModel.Where(x => x.Approved==true).Count(),
                RejectedRequest = requestsModel.Count(x => x.Approved == false),
                PendingRequest = requestsModel.Count(x => x.Approved == null && x.Cancelled !=true),
                LeaveRequests = requestsModel
            };
            model.LeaveRequests = model.LeaveRequests.Select(x => { x.DaysRequested = (int)(x.EndDate-x.StartDate).TotalDays; return x; }).ToList();
            return View(model);
        }

        // GET: LeaveHistoryController/Details/5
        public async Task<ActionResult> Details(int id, int? error =null, string message=null)
        {
            var leaveRequest = _mapper.Map<LeaveHistoryVM>( await _unitOfWork.LeaveHistoryRepo.Find(
                                                                         expression:x=> x.Id==id, 
                                                                         includes: q => q.Include(x => x.RequestingEmployee).
                                                                                      Include(x => x.LeaveType).
                                                                                     Include(x => x.ApprovedBy))); //_repoHistory.FindById(id)
            if (error == 1)
            {
                ViewBag.errorAcept = true;
                ViewBag.MessageError = message;
            }
            else
                ViewBag.errorAcept = false;
            return View(leaveRequest);
        }

        //All the Employee Leaves
        public async Task<ActionResult> MyLeaves()
        {           
            var employee = await _userManager.GetUserAsync(User);
            //_repoAllocation.GetLeaveAllocationsByEmployee(employee.Id)
            var employeeAllocations = await _unitOfWork.LeaveAllocationRepo.FindAll(x => x.EmployeeId == employee.Id, 
                                                                                   includes: q => q.Include(x => x.LeaveType));
            //_repoHistory.GetLeaveRequestsByEmployee(employee.Id)
            var employeeRequests = await _unitOfWork.LeaveHistoryRepo.FindAll(x => x.RequestingEmployeeId == employee.Id, 
                                                                              includes: q => q.Include(x => x.RequestingEmployee).
                                                                                      Include(x => x.LeaveType).
                                                                                      Include(x => x.ApprovedBy));

            var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestsModel = _mapper.Map<List<LeaveHistoryVM>>(employeeRequests);
            employeeRequestsModel = employeeRequestsModel.Select(x => { x.DaysRequested = (int)(x.EndDate - x.StartDate).TotalDays; return x; }).ToList();
            var model = new EmployeeLeaveRequestsVM
            {
                LeaveAllocations = employeeAllocationsModel,
                LeaveRequests = employeeRequestsModel
            };
            return View(model);
        }

        // GET: LeaveHistoryController/Create
        public async Task <ActionResult> Create()
        {
            var leaveTypes = await _unitOfWork.LeaveTypeRepo.FindAll(); //_repoType.FindAll()
            //select
            var leaveTypesItems = leaveTypes.Select(x => new SelectListItem {
            Text = x.Name,
            Value = x.Id.ToString()});
            var model = new CreateLeaveRequestVM
            {
                StartDate = DateTime.Now.ToString(),
                EndDate = DateTime.Now.ToString(),
                LeaveTypes = leaveTypesItems
            };
            return View(model);
        }

        // POST: LeaveHistoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            var leaveTypes = await _unitOfWork.LeaveTypeRepo.FindAll(); //_repoType.FindAll()
            //select
            var leaveTypesItems = leaveTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            model.LeaveTypes = leaveTypesItems;
            
            try
            {
                //validations
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var dateStart = Convert.ToDateTime(model.StartDate);
                var dateEnd= Convert.ToDateTime(model.EndDate);
                var period = DateTime.Now.Year;
                if (DateTime.Compare(dateStart, dateEnd) >0 )
                {
                    ModelState.AddModelError("", "Error , the start date cannot be further in the future than the end date");
                    return View(model);
                }
                if (dateStart.Year != period || dateEnd.Year != period)
                {
                    ModelState.AddModelError("", "Error , you have selected at least one date out of the period");
                    return View(model);
                }
                if (dateStart.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "You can't request a leave to start in a date before today");
                    return View(model);
                }
                //user logged
                var employee = await _userManager.GetUserAsync(User);
                //var allocation = await _repoAllocation.GetLeaveAllocationByEmployeeAndType(employee.Id,model.LeaveTypeId);
                var allocation = await _unitOfWork.LeaveAllocationRepo.Find(x => x.EmployeeId == employee.Id
                                                                          && x.Period == DateTime.Now.Year
                                                                          && x.LeaveTypeId == model.LeaveTypeId,
                                                                          includes: q => q.Include(x => x.LeaveType).
                                                                                           Include(x => x.Employee));
                int daysRequested = (int)(dateEnd - dateStart).TotalDays;

                if (allocation ==null)
                {
                    ModelState.AddModelError("", "You Do Not Have Days Allocated for this year");
                    return View(model);
                }
                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Have Sufficient Days For This Request");
                    return View(model);
                }

                //mapping
                var leaveHistoryVM = new LeaveHistoryVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = dateStart,
                    EndDate = dateEnd,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = null,
                    LeaveTypeId = model.LeaveTypeId,
                    RequestComments = model.RequestComments
                };
                var leaveRequest = _mapper.Map<LeaveHistory>(leaveHistoryVM);
                //Save on dataBase
                await _unitOfWork.LeaveHistoryRepo.Create(leaveRequest); //_repoHistory.Create(leaveRequest)
                var isSuccess = await _unitOfWork.Save();
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error when saving on DataBase");
                    return View(model);
                }
                return RedirectToAction(nameof(MyLeaves));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the creation:" + ex.Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Approve(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveHistoryRepo.Find(expression: x => x.Id == id,
                                                                      includes: q => q.Include(x => x.RequestingEmployee).
                                                                                      Include(x => x.LeaveType).
                                                                                      Include(x => x.ApprovedBy)); //_repoHistory.FindById(id)
            var model = _mapper.Map<LeaveHistoryVM>(leaveRequest);            
            try
            {
                leaveRequest.Approved = true;
                 var user = await _userManager.GetUserAsync(User);
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;
                //_repoAllocation.GetLeaveAllocationByEmployeeAndType(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId)
                var allocation = await _unitOfWork.LeaveAllocationRepo.Find(x => x.EmployeeId == leaveRequest.RequestingEmployeeId 
                                                                           && x.Period == DateTime.Now.Year 
                                                                           && x.LeaveTypeId == leaveRequest.LeaveTypeId, 
                                                                           includes: q => q.Include(x => x.LeaveType).
                                                                                           Include(x => x.Employee));
                allocation.NumberOfDays -= (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                  
                if (allocation.NumberOfDays<=0)
                {
                    ModelState.AddModelError("", "Error the Employee doesn't have enough days to complete this request");
                    return RedirectToAction(nameof(Details), new { id = leaveRequest.Id, error = 1,message = "Error the Employee doesn't have enough days to complete this request" }); ;
                    }
                await _unitOfWork.LeaveHistoryRepo.Update(leaveRequest); //_repoHistory.Update(leaveRequest)               
                await _unitOfWork.LeaveAllocationRepo.Update(allocation); //_repoAllocation.Update(allocation)
                var success = await _unitOfWork.Save();
                if (!success)
                {
                    ModelState.AddModelError("", "Error when saving on DataBase");
                    return RedirectToAction(nameof(Details), new { id = leaveRequest.Id, error = 1, message = "Error when saving on DataBase" }); ;
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the aprobation of request:" + ex.Message);
                return View(model);
            }
        }
            public async Task<ActionResult> Reject(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveHistoryRepo.Find(expression: x => x.Id == id,
                                                                      includes: q=> q.Include(x => x.RequestingEmployee).
                                                                                      Include(x => x.LeaveType).
                                                                                      Include(x => x.ApprovedBy));  //_repoHistory.FindById(id)
            var model = _mapper.Map<LeaveHistoryVM>(leaveRequest);
            try
            {
                leaveRequest.Approved = false;
                var user = await _userManager.GetUserAsync(User);
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;
                 await _unitOfWork.LeaveHistoryRepo.Update(leaveRequest); //_repoHistory.Update(leaveRequest)
                var success = await _unitOfWork.Save();
                if (!success)
                {
                    ModelState.AddModelError("", "Error when saving on DataBase");
                    return RedirectToAction(nameof(Details),new { id = leaveRequest.Id, error = 1, message = "Error when saving on DataBase" });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the rejection of request:" + ex.Message);
                return View(model);
            }
        }


        public async Task<ActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveHistoryRepo.Find(expression: x => x.Id == id,
                                                                      includes: q => q.Include(x => x.RequestingEmployee).
                                                                                      Include(x => x.LeaveType).
                                                                                      Include(x => x.ApprovedBy));  //_repoHistory.FindById(id)
            leaveRequest.Cancelled = true;
            await _unitOfWork.LeaveHistoryRepo.Update(leaveRequest);
            await _unitOfWork.Save();
            return RedirectToAction("MyLeaves");
        }

        // GET: LeaveHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveHistoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LeaveHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveHistoryController/Delete/5
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

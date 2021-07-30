using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveHistoryController : Controller
    {

        private readonly ILeaveHistoryRepository _repoHistory;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository  _repoType;
        private readonly ILeaveAllocationRepository _repoAllocation;
        public LeaveHistoryController(ILeaveTypeRepository repoType,
      ILeaveHistoryRepository repoHistory, ILeaveAllocationRepository repoAllocation, IMapper mapper, UserManager<Employee> userManager)
        {
            _repoHistory = repoHistory;
            _repoType = repoType;
            _repoAllocation = repoAllocation;
             _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveHistoryController
        public ActionResult Index()
        {
            var requests = _repoHistory.FindAll();
            var requestsModel = _mapper.Map<List<LeaveHistoryVM>>(requests);
            var model = new AdminLeaveHistoryVM
            {
                TotalRequest = requestsModel.Count,
                ApprovedRequest = requestsModel.Where(x => x.Approved==true).Count(),
                RejectedRequest = requestsModel.Count(x => x.Approved == false),
                PendingRequest = requestsModel.Count(x => x.Approved == null),
                LeaveRequests = requestsModel
            };
            model.LeaveRequests = model.LeaveRequests.Select(x => { x.DaysRequested = (int)(x.EndDate-x.StartDate).TotalDays; return x; }).ToList();
            return View(model);
        }

        // GET: LeaveHistoryController/Details/5
        public ActionResult Details(int id)
        {
            var leaveRequest = _mapper.Map<LeaveHistoryVM>(_repoHistory.FindById(id));

            return View(leaveRequest);
        }

        public ActionResult MyLeaves()
        {
           
            var employee = _userManager.GetUserAsync(User).Result;            
            var employeeAllocations = _repoAllocation.GetLeaveAllocationsByEmployee(employee.Id);
            var employeeRequests = _repoHistory.GetLeaveRequestsByEmployee(employee.Id);

            var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestsModel = _mapper.Map<List<LeaveHistoryVM>>(employeeRequests);

            var model = new EmployeeLeaveRequestsVM
            {
                LeaveAllocations = employeeAllocationsModel,
                LeaveRequests = employeeRequestsModel
            };

            return View(model);

        }

        // GET: LeaveHistoryController/Create
        public ActionResult Create()
        {

            var leaveTypes =_repoType.FindAll();
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
        public ActionResult Create(CreateLeaveRequestVM model)
        {
            var leaveTypes = _repoType.FindAll();
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
                var employee = _userManager.GetUserAsync(User).Result;
                var allocation = _repoAllocation.GetLeaveAllocationByEmployeeAndType(employee.Id,model.LeaveTypeId);
                int daysRequested = (int)(dateEnd - dateStart).TotalDays;

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
                var isSuccess = _repoHistory.Create(leaveRequest);

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

        public ActionResult Approve(int id)
        {
            var leaveRequest = _repoHistory.FindById(id);
            var model = _mapper.Map<LeaveHistoryVM>(_repoHistory.FindById(id));
            
            try
            {
                leaveRequest.Approved = true;
                 var user = _userManager.GetUserAsync(User).Result;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;
                var allocation = _repoAllocation.GetLeaveAllocationByEmployeeAndType(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDays -= (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

               
                var success = _repoHistory.Update(leaveRequest);
                _repoAllocation.Update(allocation);
                if (!success)
                {
                    ModelState.AddModelError("", "Error when saving on DataBase");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the aprobation of request:" + ex.Message);
                return View(model);
            }
        }
            public ActionResult Reject(int id)
        {
            var leaveRequest = _repoHistory.FindById(id);
            var model = _mapper.Map<LeaveHistoryVM>(_repoHistory.FindById(id));
            try
            {
                leaveRequest.Approved = false;
                var user = _userManager.GetUserAsync(User).Result;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;
                var success = _repoHistory.Update(leaveRequest);
                if (!success)
                {
                    ModelState.AddModelError("", "Error when saving on DataBase");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error on the rejection of request:" + ex.Message);
                return View(model);
            }
        }


        public ActionResult CancelRequest(int id)
        {
            var leaveRequest = _repoHistory.FindById(id);
            leaveRequest.Cancelled = true;
            _repoHistory.Update(leaveRequest);
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

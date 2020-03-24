using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thryvo.Contracts;
using Thryvo.Models;

namespace Thryvo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        
        private readonly ILeaveTypeRepository _typerepo;
        private readonly ILeaveAllocationRepository _allowrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager; 

        public LeaveAllocationController(
            ILeaveTypeRepository typerepo,
            UserManager<Employee> userManager, 
            IMapper mapper, 
            ILeaveAllocationRepository allowrepo)
        {
            _typerepo = typerepo;
            _userManager = userManager; 
            _allowrepo = allowrepo; 
            _mapper = mapper;
        }

        // GET: LeaveAllocation
        public ActionResult Index()
        {
            var leavetypes = _typerepo.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };
            return View(model);
        }

        public ActionResult SetLeave(int id)
        {
            var leavetype = _typerepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employees)
            {
                if (_allowrepo.CheckAllocation(id, emp.Id))
                    continue; 

                var allocation = new LeaveAllocationVM
                {
                   DateCreated = DateTime.Now,
                   EmployeeId = emp.Id,
                   LeaveTypeId = id,
                   NumberOfDays = leavetype.DefaultDays,
                   Period = DateTime.Now.Year
                };
                var leaveallocation = _mapper.Map<LeaveAllocation>(allocation);
                _allowrepo.Create(leaveallocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }

        // GET: LeaveAllocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveAllocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
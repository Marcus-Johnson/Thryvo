using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thryvo.Models;

namespace Thryvo.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(int leaveTypeId, string employeeId);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id); 
    }
}

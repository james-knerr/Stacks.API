using Stacks.API.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Stacks.API
{
    public interface IStacksRepository
    {
        bool SaveAll(ClaimsPrincipal userName);
        bool SaveAll(string email);
        ICollection<Stack> GetStacks(bool includeDeleted = false);
        void AddStack(Stack newStack);
        Stack GetStackById(Guid stackId);
        void AddRecord(Record newRecord, Guid stackId);
    }
}

using Microsoft.EntityFrameworkCore;
using Stacks.API.Helpers;
using Stacks.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Stacks.API
{
    public class StacksRepository : IStacksRepository
    {
        private StacksContext _context;
        public StacksRepository(StacksContext context)
        {
            _context = context;
        }
        public bool SaveAll(ClaimsPrincipal user)
        {
            var email = "";//UserClaimHelper.Email(user.Identity);
            return SaveAll(email);
        }

        public bool SaveAll(string email)
        {
            /*foreach (var history in _context.ChangeTracker.Entries()
                            .Where(e => e.Entity is IModificationHistory &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified))
                            .Select(e => e.Entity as IModificationHistory))
            {
                history.ModifiedByUser = email;
                if (string.IsNullOrEmpty(history.CreatedByUser))
                {
                    history.CreatedByUser = email;
                }

                history.DateModified = DateTime.UtcNow;
                if (history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.UtcNow;
                }
            }*/

            return _context.SaveChanges() > 0;
        }
        public ICollection<Stack> GetStacks(bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return _context.Stacks.ToList();
            } else
            {
                return _context.Stacks.Where(k => k.IsDeleted == false).ToList();
            }

        }
        public void AddStack(Stack newStack)
        {
            _context.Add(newStack);
        }
        public void AddRecord(Record newRecord, Guid stackId)
        {
            var stack = _context.Stacks.Include(k => k.Records).FirstOrDefault(k => k.Id == stackId);
            stack.Records.Add(newRecord);
        }
        public Stack GetStackById(Guid stackId)
        {
            return _context.Stacks.Include(k => k.Records).FirstOrDefault(k => k.Id == stackId);
        }
    }
}
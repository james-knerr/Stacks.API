using Stacks.API.Helpers;
using Stacks.API.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stacks.API.Models
{
    public class Stack
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Record> Records { get; set; }
        public bool IsDeleted { get; set; }
        public StackViewModel ToViewModel()
        {
            var vm = new StackViewModel()
            {
                Id = GuidMappings.Map(this.Id),
                Title = this.Title,
                Records = this.Records != null ? new List<RecordViewModel>(this.Records.Count) : new List<RecordViewModel>(),
                IsDeleted = this.IsDeleted
            };
            if (this.Records != null)
            {
                foreach (var record in this.Records)
                {
                    vm.Records.Add(record.ToViewModel());
                }
            }
            return vm;
        }
        public StackListItemViewModel ToListItemViewModel()
        {
            var vm = new StackListItemViewModel()
            {
                Id = GuidMappings.Map(this.Id),
                Title = this.Title,
                IsDeleted = this.IsDeleted
            };
            return vm;
        }
    }
}

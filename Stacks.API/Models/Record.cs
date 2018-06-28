using Stacks.API.Helpers;
using Stacks.API.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stacks.API.Models
{
    public class Record
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string SourceUrl { get; set; }
        public bool IsDeleted { get; set; }

        public RecordViewModel ToViewModel()
        {
            var vm = new RecordViewModel()
            {
                Id = GuidMappings.Map(this.Id),
                Title = this.Title,
                ImageUrl = this.ImageUrl,
                SourceUrl = this.SourceUrl,
                IsDeleted = this.IsDeleted
            };
            return vm;
        }
    }
}

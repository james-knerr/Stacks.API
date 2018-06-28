using Stacks.API.Models;
using System.Collections.Generic;

namespace Stacks.API.ViewModels
{
    public class StackViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public ICollection<RecordViewModel> Records { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class StackListItemViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public Stack ToModel()
        {
            var model = new Stack()
            {
                Title = this.Title
            };
            return model;
        }
    }
}

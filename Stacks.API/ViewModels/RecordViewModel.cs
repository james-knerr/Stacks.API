using Stacks.API.Models;

namespace Stacks.API.ViewModels
{
    public class RecordViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string SourceUrl { get; set; }
        public bool IsDeleted { get; set; }
        public Record ToModel()
        {
            var model = new Record()
            {
                Title = this.Title,
                ImageUrl = this.ImageUrl
            };
            return model;
        }
    }
}

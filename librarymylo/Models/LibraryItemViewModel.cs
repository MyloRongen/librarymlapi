using System.ComponentModel.DataAnnotations.Schema;

namespace librarymylo.WebApi.Models
{
    public class LibraryItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string? ImageSrc { get; set; }
    }
}

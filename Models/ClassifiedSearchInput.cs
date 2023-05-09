
using System.ComponentModel.DataAnnotations;

namespace MyAds.Models
{
    public class ClassifiedSearchInput
    {
        [Required(ErrorMessage = "Search filter is required!")]
        [MinLength(3, ErrorMessage = "Search filter minimun 3 characters!")]
        public string Filter { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CategoryId { get; set; }
    }
}

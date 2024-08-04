using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BookVM
    {

        public required string Name { get; set; }

        public required string Genre { get; set; }
        public long SetId { get; set; }

        public required double Width { get; set; }

        public required double Height { get; set; }
    }
}

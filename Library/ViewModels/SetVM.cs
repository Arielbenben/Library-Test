using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class SetVM
    {
        public required string Name { get; set; }
        public long ShelfId { get; set; }

    }
}

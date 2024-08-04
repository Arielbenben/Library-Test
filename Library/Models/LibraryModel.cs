using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
		[Index(nameof(Genre), IsUnique =true)]
	public class LibraryModel
	{
		public long Id { get; set; }

		[Required, StringLength(100, MinimumLength = 3)]
		public required string Genre { get; set; }
		public List<ShelfModel> Shelves { get; set; } = [];

		
	}
}

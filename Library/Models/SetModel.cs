using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class SetModel
	{
		public long Id { get; set; }
		public long ShelfId { get; set; }
		public ShelfModel? Shelf { get; set; }

		[Required, StringLength(100, MinimumLength = 2)]
		public required string Name { get; set; }
		
		public List<BookModel> Books { get; set; } = [];
	}
}
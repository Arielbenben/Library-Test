using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class ShelfModel
	{
		public long Id { get; set; }

		[Required]
		public required double Width { get; set; }

		[Required]
		public required double Height { get; set; }
		public long LibraryId { get; set; }

		[Required]
		public LibraryModel? Library { get; set; }
		public List<SetModel> Sets { get; set; } = [];

	}
}

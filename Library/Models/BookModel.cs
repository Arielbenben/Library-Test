using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class BookModel
	{
		public long Id { get; set; }

		[Required, StringLength(100, MinimumLength = 3)]
		public required string Name { get; set; }

		[Required, StringLength(100, MinimumLength = 3)]
		public required string Genre { get; set; }
		public long SetId { get; set; }

		[Required]
		public required double Width { get; set; }

		[Required]
		public required double Height { get; set; }
		public SetModel? Set {  get; set; }


	}
}
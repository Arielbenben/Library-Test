using Library.Models;

namespace Library.ViewModels
{
    public class ShelfVM
    {
        
        public required double Width { get; set; }

        public required double Height { get; set; }
        public long LibraryId { get; set; }

    }
}

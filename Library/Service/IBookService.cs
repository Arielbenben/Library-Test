using Library.Models;
using Library.ViewModels;

namespace Library.Service
{
	public interface IBookService
	{
		Task<List<BookModel>> GetAllBooksBySetId(long id);
		Task<BookModel> InsertNewBook(BookVM bookVM);
		double? SumWidthAllBooksInShelf(long shelfId);

        bool HasEnoghWidthSpace(double width, long shelfId);

		bool HasEnoughHightSpace(double hight, long shelfId);
		bool TenSantimeterMore(BookVM bookVM);
		string? FindGenreBySetId(long shelfId);
    }
}

using Library.Models;
using Library.ViewModel;

namespace Library.Service
{
	public interface IlibraryService
	{
		Task<List<LibraryModel>> GetAllLibraries();
		Task<bool> CreateNewLibrary(LibraryVM libraryVM);
		Task<bool> IsLibraryExists(LibraryVM libraryVM);
	}
}

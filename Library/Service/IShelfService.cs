using Library.Models;
using Library.ViewModels;

namespace Library.Service

{
	public interface IShelfService
	{
		Task<List<ShelfModel>> GetAllShelves(long id );
		Task<ShelfModel> CreateNewShelf(ShelfVM shelfVM);
        


    }
}

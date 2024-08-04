using Library.Data;
using Library.Models;
using Library.ViewModel;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
	public class ShelfService : IShelfService
	{
		private readonly ApplicationDBContext _context;

		public ShelfService(ApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<List<ShelfModel>> GetAllShelves(long id)
		{
            List<ShelfModel> shelves = await _context.Shelves
				.Where(s => s.LibraryId == id)
				.ToListAsync();
			return shelves;
        }





        public async Task<ShelfModel> CreateNewShelf(ShelfVM shelfVM)
		{
			LibraryModel? library = await _context.Library
			.FirstOrDefaultAsync(l => l.Id == shelfVM.LibraryId);

                ShelfModel shelfModel = new()
				{
					Height = shelfVM.Height,
					Width = shelfVM.Width,
					LibraryId = shelfVM.LibraryId, 
					Library = library
				};
				await _context.Shelves.AddAsync(shelfModel);
				await _context.SaveChangesAsync();
				library?.Shelves.Add(shelfModel);
				return shelfModel;

		}
	}
}

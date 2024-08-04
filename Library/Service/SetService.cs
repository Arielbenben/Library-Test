using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class SetService : ISetService
    {
        private readonly ApplicationDBContext _context;

        public SetService(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<SetModel> CreateNewSet(SetVM setVM)
        {
            ShelfModel? shelf = await _context.Shelves.FirstOrDefaultAsync(s => s.Id == setVM.ShelfId);
            SetModel set = new() {
                Name = setVM.Name,
                ShelfId = setVM.ShelfId,
                Shelf = shelf
            };
            await _context.Sets.AddAsync(set);
            await _context.SaveChangesAsync();
            shelf?.Sets.Add(set);
            return set;
            
        }

        public async Task<List<SetModel>> GetAllSetsByShelfId(long id)
        {
            List<SetModel> sets = await _context.Sets
                .Where(s => s.ShelfId == id)
                .ToListAsync();
            return sets;
        }

        
    }
}

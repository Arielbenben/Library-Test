using Azure;
using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISetService _setService;

        public BookService(ApplicationDBContext context,
            ISetService setService)
        {
            _context = context;
            _setService = setService;
        }


        public async Task<List<BookModel>> GetAllBooksBySetId(long id)
        {
            List<BookModel> books = await _context.books
                .Where(b => b.SetId == id)
                .ToListAsync();
            return books;
        }


        public async Task<BookModel> InsertNewBook(BookVM bookVM)
        {
            
            SetModel? set = await _context.Sets
                .FirstOrDefaultAsync(s => s.Id == bookVM.SetId);
            if (set == null) throw new Exception("Set not found");

            ShelfModel? shelf = FindShelfByShelfId(set.ShelfId);
            if (shelf == null) throw new Exception("Shelf not found");

            LibraryModel? library = await _context.Library
                .FirstOrDefaultAsync(l => l.Id == shelf.LibraryId);
            if (library == null) throw new Exception("Library not found");


            if (!HasEnoghWidthSpace(bookVM.Width, set.ShelfId)) 
                throw new Exception(
                "There is not enough space in this shelf, " +
                "the width is fully occupied, " +
                "please try in another shelf or create a new one");


            if (!HasEnoughHightSpace(bookVM.Height, set.ShelfId))
                throw new Exception("This book is too hight for this shelf, " +
                    "please try in another shelf or create a new one");

            


            BookModel? newBook = new()
            {
                Name = bookVM.Name,
                Genre = bookVM.Genre,
                Width = bookVM.Width,
                Height = bookVM.Height,
                SetId = bookVM.SetId,
                Set = set
            };

            await _context.books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            set.Books.Add(newBook);
            return newBook;
           
        }

        

        public double? SumWidthAllBooksInShelf(long shelfId)
        {
            ShelfModel? shelf = FindShelfByShelfId(shelfId);


            double? sumAllBooks = shelf?.Sets
                .Select(s => s.Books)
                .ToList().
                Select(bl => bl
                .Select(b => b.Width)
                .ToList())
                .ToList()
                .Aggregate(0.0, (current, next) => current + next
                .Aggregate(0.0, (current, next)=> current + next))
                ;

            return sumAllBooks;
            
                
        }

        public string? FindGenreBySetId(long setId)
        {
            SetModel? set = _context.Sets
                .FirstOrDefault(s => s.Id == setId);
            if (set == null) throw new Exception("Set not found");

            ShelfModel? shelf = _context.Shelves
                .FirstOrDefault(s => s.Id == set.ShelfId);
            if (shelf == null) throw new Exception("Shelf is not found");

            string? genre = _context.Library.
                FirstOrDefault(l => l.Id == shelf.LibraryId)?.Genre;
            return genre;
        }

        public bool HasEnoghWidthSpace(double width, long shelfId)
        {
            var sumAllBooks = SumWidthAllBooksInShelf(shelfId);
            ShelfModel? shelf = FindShelfByShelfId(shelfId);
            var res = shelf?.Width - sumAllBooks;
            return res > width;
        }

        public bool HasEnoughHightSpace(double hight, long shelfId)
        {
            ShelfModel? shelf = FindShelfByShelfId(shelfId);
            if (shelf == null) throw new Exception("Shelf is not found");
            return shelf.Height > hight;
        }

        public ShelfModel? FindShelfByShelfId(long shelfId) =>
            _context.Shelves.FirstOrDefault(s => s.Id == shelfId);


        public bool TenSantimeterMore(BookVM bookVM)
        {
            SetModel? set = _context.Sets
                .FirstOrDefault(s => s.Id == bookVM.SetId);
            if (set == null) throw new Exception("Set not found");

            ShelfModel? shelf = FindShelfByShelfId(set.ShelfId);
            return (shelf?.Height - bookVM.Height) >= 10;
        }


        
        
    }
}

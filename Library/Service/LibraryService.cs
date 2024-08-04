using Library.ViewModel;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Service
{
	public class LibraryService : IlibraryService
	{
		private readonly ApplicationDBContext _Context;

		public LibraryService(ApplicationDBContext dbContext)
		{
			_Context = dbContext;
		}

		public async Task<List<LibraryModel>> GetAllLibraries()
		{
			return await _Context.Library
				.Include(l => l.Shelves)
				.ToListAsync();
		}

		public async Task<bool> CreateNewLibrary (LibraryVM libraryVM)
		{
			
			try
			{
				LibraryModel newLibrary = new() { Genre = libraryVM.Genre };
				await _Context.Library.AddAsync(newLibrary);
				await _Context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex) {
				return false;
			}

		}

		public async Task<bool> IsLibraryExists(LibraryVM libraryVM)
		{ 
			var libraries = await GetAllLibraries();
			return libraries.Exists(l => l.Genre == libraryVM.Genre);
		}

		
	}
}

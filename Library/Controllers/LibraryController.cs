using Library.Service;
using Microsoft.AspNetCore.Mvc;
using Library.ViewModel;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library.Controllers
{
	public class LibraryController : Controller
	{
		private readonly IlibraryService _libraryService;
		private readonly ApplicationDBContext _Context;


		public LibraryController(ApplicationDBContext dbContext, IlibraryService libraryService)
		{
			_Context = dbContext;
			_libraryService = libraryService;
		}


		public async Task<IActionResult> Index() =>
			View(await _libraryService.GetAllLibraries());


		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(LibraryVM libraryVM)
		{

			if (!ModelState.IsValid)
			{
				return View("ErorLibrary");
			}

			if (await _libraryService.IsLibraryExists(libraryVM))
			{
				return View("LibraryExists");
			}

			bool success = await _libraryService.CreateNewLibrary(libraryVM);
			if (success) return RedirectToAction("Index");
			return View("ErorLibrary");

		}
	}
}

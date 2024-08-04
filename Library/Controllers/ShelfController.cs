using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.ViewModel;
using Library.Service;
using Library.ViewModels;

namespace Library.Controllers
{
	public class ShelfController : Controller
	{
		private readonly ApplicationDBContext _Context;
		private readonly IShelfService _ishelfService;



		public ShelfController(ApplicationDBContext context, IShelfService shelfService)
		{
			_Context = context;
			_ishelfService = shelfService;
		}


		public async Task<IActionResult> Index(long id)
		{

			ViewBag.LibraryId = id;
			return View(await _ishelfService.GetAllShelves(id));
		}


		public IActionResult Create(long libraryId)
		{
			ViewBag.LibraryId = libraryId;
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ShelfVM shelfVM)
		{
			try
			{
				await _ishelfService.CreateNewShelf(shelfVM);
			}
			catch (Exception ex)
			{
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }

            return RedirectToAction("Index", new { id = shelfVM.LibraryId });
		}
	}
}

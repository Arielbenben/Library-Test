using Library.Data;
using Library.Models;
using Library.Service;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDBContext _context;

        public BookController(IBookService bookService,
            ApplicationDBContext applicationDBContext)
        {
            _bookService = bookService;
            _context = applicationDBContext;
        }

        public async Task<IActionResult> Index(long setId)
        {
            ViewBag.SetId = setId;
            return View(await _bookService.GetAllBooksBySetId(setId));
        }


        public IActionResult Create(long setId) 
        {
            ViewBag.SetId = setId;
            ViewBag.Genre = _bookService.FindGenreBySetId(setId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM bookVM)
        {
            try
            {
                BookModel newBook = await _bookService.InsertNewBook(bookVM);
                if (newBook == null) throw new Exception("Something was wrong");

                ViewBag.TenMore = null;
                if (_bookService.TenSantimeterMore(bookVM))
                {
                    ViewBag.TenMore = 1;
                }
                
                return RedirectToAction("Index", new { setId = bookVM.SetId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }
        }
    }
}

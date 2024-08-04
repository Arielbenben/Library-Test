using Humanizer.Localisation;
using Library.Service;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class SetController : Controller
    {
        private readonly ISetService _setService;

        public SetController(ISetService setService)
        {
            _setService = setService;
        }

        public async Task<IActionResult> Index(long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            return View( await _setService.GetAllSetsByShelfId(shelfId));
        }

        public IActionResult Create(long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetVM setVM) 
        {
            try
            {
                await _setService.CreateNewSet(setVM);
                return RedirectToAction("Index", new { shelfId = setVM.ShelfId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }

        }




    }
}

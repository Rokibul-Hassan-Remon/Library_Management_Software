
using LibraryManagementSoftwareModels.Entities;
using LibraryManagementSoftwareServices.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSoftware.Controllers
{
	public class BookController(IBookService bookService) : Controller
	{
        private IBookService _bookService = bookService;

		public async Task<IActionResult> Index()
        {
            var list = await _bookService.GetAllAsync();
			return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Create(Book entity)
		{
			if (entity == null)
			{
				return View();
			}
			else
			{
				 await _bookService.AddAsync(entity);
				return RedirectToAction("Index");
			}
		}

		public async Task<IActionResult> Update(int id)
		{
			var obj = await _bookService.GetByIdAsync(id);
			return View(obj);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Book entity)
		{
			await _bookService.UpdateAsync(entity);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _bookService.DeleteAsync(id);
			return RedirectToAction("Index");
		}

		
		public async Task<IActionResult> Details(int id)
		{
			var details = await _bookService.GetByIdAsync(id);
			return View(details);
		}
	}
}

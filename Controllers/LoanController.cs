using Microsoft.AspNetCore.Mvc;
using LibraryManagementSoftwareServices.IServices;
using LibraryManagementSoftwareModels.Entities;

namespace LibraryManagementSoftware.Controllers
{
	public class LoanController( ILoanService loanService ) : Controller
	{
		private ILoanService _studentService = loanService;

		public async Task<IActionResult> Index()
		{
			var obj = await _studentService.GetAllAsync();
			return View(obj);
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult>Create(Loan entity)
		{
			 await _studentService.AddAsync(entity);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(int id)
		{
			if (id > 0)
			{
				var obj = await _studentService.GetByIdAsync(id);
				return View(obj);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Update(Loan entity)
		{
			await _studentService.UpdateAsync(entity);
			return RedirectToAction("Details");
		}

		public async Task<IActionResult> Details(int id)
		{
			if (id > 0)
			{
				var obj = await _studentService.GetByIdAsync(id);
				return View(obj);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			if(id > 0)
			await _studentService.DeleteAsync(id);

			return RedirectToAction("Index");
		}

	}
}

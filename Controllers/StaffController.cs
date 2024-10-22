
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSoftwareModels.Entities;
using LibraryManagementSoftwareServices.IServices;

namespace LibraryManagementSoftware.Controllers
{
	public class StaffController( IStaffService staffServie) : Controller
	{
		private IStaffService _staffService = staffServie;

		public async Task<IActionResult> Index()
		{
			var list = await _staffService.GetAllAsync();
			return View(list);
		}

		public async Task<IActionResult> Details(int id)
		{
			if (id > 0)
			{
				var obj = await _staffService.GetByIdAsync(id);
				return View(obj);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id > 0)
			{
				await _staffService.DeleteAsync(id);
				
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update( int id)
		{
			if (id > 0)
			{

				var Obj = await _staffService.GetByIdAsync(id);
				return View(Obj);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Update(Staff entity)
		{
			if(entity != null)
			{
				await _staffService.UpdateAsync(entity);
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index");
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Create(Staff entity)
		{
			if(entity == null)
			{
				return View();
			}

			else
			{
				await _staffService.AddAsync(entity);
				return RedirectToAction("Index");
			}
		}


	}
}

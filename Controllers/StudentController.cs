using Microsoft.EntityFrameworkCore;
using LibraryManagementSoftwareModels.Entities;
using LibraryManagementSoftwareServices.IServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementSoftwareModels.Business_Model;

namespace LibraryManagementSoftware.Controllers
{
    [Authorize]
    public class StudentController(IStudentService studentService) : Controller
	{
        
        private IStudentService _studentService = studentService;

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
		public async Task<IActionResult> Create(StudentViewModel entity)
		{
			if(entity == null)
			{
				return View();
			}

			else
			{
				await _studentService.AddAsync(entity);
				return RedirectToAction("Index");
			}
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
		public async Task<IActionResult> Update(StudentViewModel entity)
		{
			if(entity != null)
			{
				await _studentService.UpdateAsync(entity);
				return RedirectToAction("Details");
			}
			return View();
		}

		
		public async Task<IActionResult>Details(int id)
		{

            if (id > 0)
            {
                var obj = await _studentService.GetByIdAsync(id);
                return View(obj);
            }

            return RedirectToAction("Index");

        }

	}
}

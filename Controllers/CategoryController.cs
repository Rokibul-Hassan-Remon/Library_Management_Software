using LibraryManagementSoftwareModels.Entities;
using LibraryManagementSoftwareServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSoftware.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {        
        private ICategoryService _categoryservice;

        public CategoryController(ICategoryService categoryservice)
        {
            _categoryservice = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            var list =  await _categoryservice.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category entity)
        {
            await _categoryservice.AddAsync(entity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            await _categoryservice.GetByIdAsync(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category entity)
        {
            await _categoryservice.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryservice.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

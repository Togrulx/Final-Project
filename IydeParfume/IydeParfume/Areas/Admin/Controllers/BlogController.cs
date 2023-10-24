using IydeParfume.Areas.Admin.ViewModels.Blog;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin")]

    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public readonly ILogger<BlogController> _logger;

        public BlogController(DataContext dataContext, IFileService fileService, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List'

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> List()
        {

            var model = await _dataContext.Blogs.OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(p.Id, p.Title, p.Content, p.CreatedAt
                )).ToListAsync();


            return View(model);
        }

        #endregion

        #region Add' 

        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add()
        {
          
            return View();
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");


            async void AddProduct()
            {
                var product = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                };

                await _dataContext.Blogs.AddAsync(product);

            }
        } 
        #endregion


        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null) return NotFound();
           

            _dataContext.Blogs.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");
        }


        #endregion
    }
}

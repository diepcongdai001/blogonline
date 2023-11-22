using BlogOnline.BackEnd.Entity;
using BlogOnline.BackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Reflection;
using BlogOnline.BackEnd.Helper;

namespace BlogOnline.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IReadHelper _readResultHelper;
        public BlogsController (ApplicationDbContext context, IReadHelper readResultHelper)
        {
            _context = context;
            _readResultHelper = readResultHelper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogViewModel>> GetBlogById(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            var createdBy = await _context.Users.FindAsync(blog.CreatedBy);
            var fileUpload = await _context.FileUploads.Where(c => blog.FileIds.Contains(c.Id)).ToListAsync();
            var blogVM = new BlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description, 
                Author = createdBy?.FullName,
                CategoryId = blog.CategoryId,
                CategoryName = blog.Category.Name, 
                CreateTime = blog.CreatedAt,
                Priority = (int)blog.Priority,
                // FileUploads = fileUpload
            };

            return blogVM;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BlogViewModel>>> GetBlogs(Guid? categoryId)
        {
            // lambda
            //var blogs = await _context.Blogs.Where(c => categoryId.HasValue ? c.CategoryId == categoryId : true).Select(x => new BlogViewModel
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Description = x.Description,
            //        Author = x.User.FullName,
            //        CategoryId = x.CategoryId,
            //        CategoryName = x.Category.Name,
            //        CreateTime = x.CreatedAt,
            //        Priority = (int)x.Priority
            //    }).ToListAsync();

            // linq
            //var blogs = await (from blog in _context.Blogs
            //                join cat in _context.Categories on blog.CategoryId equals cat.Id
            //                where categoryId.HasValue ? blog.CategoryId == categoryId : true
            //                select new BlogViewModel
            //                {
            //                    Id = blog.Id,
            //                    Title = blog.Title,
            //                    Description = blog.Description,
            //                    CategoryId = cat.Id,
            //                    CategoryName = cat.Name,
            //                    CreateTime = blog.CreatedAt,
            //                    Priority = (int)blog.Priority
            //                }).ToListAsync();

            // Dapper
            var sql = $"select bl.Id, bl.Title, bl.Description, ' ' 'Author', bl.CreatedAt 'CreateTime', bl.Priority, cat.Id 'CategoryId', cat.Name 'CategoryName'  from Blogs bl join Categories cat on bl.CategoryId = CategoryId";
            var blogs = _readResultHelper.ExecuteResultFromQuery<BlogViewModel>(_context, sql);

            return blogs;
        }
    }
}

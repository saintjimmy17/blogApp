using backend.Data;
using backend.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;

        public CategoryController(IRepository<Category> repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            var categoryList = await _repository.GetAll();
            return Ok(categoryList);
        }
    }
}

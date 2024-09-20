﻿using backend.Data;
using backend.Dto;
using backend.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IRepository<Blog> _blogRepository;

        public BlogsController(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetBlogList()
        {
            var blogs = await _blogRepository.GetAll();
            return Ok(blogs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlog([FromRoute] int id)
        {
            var blog = await _blogRepository.GetById(id);
            return Ok(blog);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddBlog([FromBody] BlogDto model)
        {
            var blog = new Blog()
            {
                CategoryId = model.CategoryId,
                IsFeatured = model.IsFeatured,
                Content = model.Content,
                Title = model.Title,
                Description = model.Description,
                Image = model.Image,
            };
            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return Ok(model);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlog([FromRoute] int id, [FromBody] BlogDto model)
        {
            var blog = await _blogRepository.GetById(id);
            blog.Description = model.Description;
            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.IsFeatured = model.IsFeatured;
            blog.Image = model.Image;
            _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
            return Ok(model);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlog([FromRoute,] int id)
        {
            await _blogRepository.DeleteAsync(id);
            await _blogRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("featured")]
        public async Task<ActionResult> GetBlogsFeatureList()
        {
            var blogs = await _blogRepository.GetAll(x=>x.IsFeatured==true);
            return Ok(blogs);
        }
    }
}

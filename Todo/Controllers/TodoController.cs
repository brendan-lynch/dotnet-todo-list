﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        // Dependency Injection is giving us context, which is a container for each of our databases/tables.
        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }

        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            _context.TodoItems.Add(new TodoItem { Name = "Item1"});
            List<TodoItem> list = _context.TodoItems.ToList();
            _context.SaveChanges();
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(item == null)
            {
                return NotFound(); // 404
            }

            return new ObjectResult(item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doit.Data;
using Doit.Models;
using Microsoft.AspNetCore.Authorization;

namespace Doit.Controllers
{
    [Authorize]
    public class TodosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAllTodos()
        {
            var todos = _context.Todos.ToList();
            return View(todos);
        }

        public IActionResult GetById(int id)
        {
            var todo = _context.Todos.SingleOrDefault(x => x.Id == id);

            var tododescriptions = _context.TodoDescriptions
                .Where(x => x.TodoId == id).ToList();

            var model = new Todo
            {
                Id = todo.Id,
                Date = todo.Date,
                Title = todo.Title,
                TodoDescriptions = tododescriptions
            };

            return View(model); 
        }
            

       [HttpGet]
        public IActionResult CreateTodo()
        {
           return View();
        } 
        
        [HttpPost]
        public IActionResult CreateTodo(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
            return RedirectToAction("GetAllTodos");
        }

        public IActionResult CreateTodoDescription(Todo desc)
        {
            var todesc = desc.TodoDescription;
            _context.Add(todesc);
            _context.SaveChanges();
            return RedirectToAction("GetById", new { id = todesc.TodoId }); 

        }

        // GET: Todo/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }
           
            public IActionResult DeleteConfirmed(int id)
            {
                var item = _context.Todos.SingleOrDefault(x => x.Id == id);

                _context.Remove(item);
                _context.SaveChanges();

                return RedirectToAction("GetAllTodos");
            }
          
          
          

        public IActionResult Edit(int id)
        {
            var item = _context.Todos.SingleOrDefault(x => x.Id == id);

            return View(item);
        }


        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            //Fetch Todo record from db
            var todoInDb = _context.Todos.AsNoTracking().SingleOrDefault(x => x.Id == todo.Id);
            //Change record from db to user's input
            todoInDb = todo;
            //Update db
            _context.Update(todoInDb).Property(x => x.Id).IsModified = false;            
            
            var todoDescInDb = _context.TodoDescriptions.AsNoTracking().Where(x => x.TodoId == todo.Id).ToList();

            foreach (var item in todoDescInDb)
            {
                item.Title = todo.Title;
                _context.Update(item).Property(x => x.TodoId).IsModified = false;

            }

            _context.SaveChanges();
          
            
            return RedirectToAction("GetAllTodos");
        }


        public IActionResult EditTodoDescription(int id)
        {
            var item = _context.TodoDescriptions.SingleOrDefault(x => x.Id == id);
          
            return View(item);

        }

        [HttpPost]
        public IActionResult EditTodoDescription(TodoDescription desc)
        {
            var descInDb = _context.TodoDescriptions. AsNoTracking().SingleOrDefault(x => x.Id == desc.Id);
         
            descInDb = desc;
          
            _context.Update(descInDb).Property(X =>X.Id).IsModified = false;
          
            _context.SaveChanges();
          
            return RedirectToAction("GetById", new { id = descInDb.TodoId });
        }

        public IActionResult DeleteTodoDescription(int id)
        {
            var item = _context.TodoDescriptions.SingleOrDefault(x => x.Id == id);

            _context.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("GetById", new { id = item.TodoId });
        }


    }
}

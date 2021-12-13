using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppInASP.Infrastructure;
using ToDoAppInASP.Models;

namespace ToDoAppInASP.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;

        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }

        // GET
        public async Task<ActionResult> Index()
        {
            IQueryable<ToDoList> items = from i in context.ToDoList orderby i.Id select i;

            List<ToDoList> toDoLists = await items.ToListAsync();

            return View(toDoLists);
        }

        // GET /todo/create
        public IActionResult Create() => View();

        // POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been addes!";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET /todo/edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            ToDoList item = await context.ToDoList.FindAsync(Id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /todo/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET /todo/delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            ToDoList item = await context.ToDoList.FindAsync(Id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}

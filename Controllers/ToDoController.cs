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
    }
}

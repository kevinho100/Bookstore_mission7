using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBooksRepository repo;

        public HomeController (IBooksRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string Category, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                //pagination set up

                Books = repo.Books
                .Where(p => p.Category == Category || Category == null)
                .OrderBy(p => p.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (Category == null
                        ?  repo.Books.Count()
                        : repo.Books.Where(x => x.Category == Category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

    }
}

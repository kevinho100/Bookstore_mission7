using System;
using System.Linq;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBooksRepository repo { get; set; }

        public TypesViewComponent(IBooksRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["Category"];

            var types = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}

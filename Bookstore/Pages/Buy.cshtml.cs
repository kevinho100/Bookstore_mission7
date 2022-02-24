using System;
using System.Linq;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class BuyModel : PageModel
    {
        private IBooksRepository repo { get; set; }

        public BuyModel(IBooksRepository temp)
        {
            repo = temp;
        }

        public Basket basket { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Books p = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(p, 1);

            HttpContext.Session.SetJeson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}

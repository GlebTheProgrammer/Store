using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookRepository bookRepository;

        public CartController(IBookRepository bookRepository) // Constructor Injection
        {
            this.bookRepository = bookRepository;
        }

        public IActionResult Add(int id)
        {
            var book = bookRepository.GetById(id);
            Cart cart;

            if (!HttpContext.Session.TryGetCart(out cart))
                cart = new Cart();

            if(cart.Items.ContainsKey(id))
                cart.Items[id]++; // Если одна книга встречается в корзине больше одного раза
            else
                cart.Items[id] = 1; // В противном случае просто добавляем одну книгу

            cart.Amount += book.Price;

            HttpContext.Session.Set(cart); // Сохраняем нашу сессию

            return RedirectToAction("Index", "Book", new { id = id });
        }
    }
}

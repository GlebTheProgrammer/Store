using Microsoft.AspNetCore.Mvc;
using Store.Web.App;

namespace Store.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly BookService bookService;

        public SearchController(BookService bookService)
        {
            this.bookService = bookService;
        }

        // /search/index?query=title
        // IActionResult - результат какого-то действия. Это либо какая-то готовая страница(View), набор заголовков в протоколе HTTP или же статус ошибки и тп.
        public IActionResult Index(string query)
        {
            var books = bookService.GetAllByQuery(query);

            return View("Index",books); // Указываем имя шаблона в папке View, который будет вызываться, и передаём массив книг, то есть модель
        }
    }
}

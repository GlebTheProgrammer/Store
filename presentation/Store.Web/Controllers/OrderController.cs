﻿using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository) // Constructor Injection
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            // Если заполнена корзина -> Загружаем order, подгружаем книги и формируем модельку, которую передаём во View
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = orderRepository.GetById(cart.OrderId);
                OrderModel model = Map(order);

                return View(model);
            }
            return View("Empty");
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            var books = bookRepository.GetAllByIds(bookIds);
            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Price = item.Price,
                                 Count = item.Count,
                             };

            return new OrderModel
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
            };
        }

        public IActionResult AddBook(int id)
        {
            Order order;
            Cart cart;

            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            var book = bookRepository.GetById(id);
            order.AddBook(book);
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart); // Сохраняем нашу сессию

            return RedirectToAction("Index", "Book", new { id = id });
        }

        public IActionResult RemoveBook(int id)
        {
            Order order;
            Cart cart;

            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            var book = bookRepository.GetById(id);
            order.RemoveBook(book);
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart); // Сохраняем нашу сессию

            return RedirectToAction("Index", "Book", new { id = id });
        }

        public IActionResult RemoveItem(int id)
        {
            Order order;
            Cart cart;

            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                throw new Exception("Cart not found");
            }

            var book = bookRepository.GetById(id);
            order.RemoveItem(book);
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Book", new { id });
        }
    }
}

using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers {
    public class OrderController : Controller {
        IOrderRepository orderRepository = null;
        
        public OrderController() => orderRepository = new OrderRepository();

        // GET: ordersController
        public IActionResult Index() {
            var orderList = orderRepository.GetOrders();
            return View(orderList);
        }

        // GET: ordersController/Details/2
        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if(order == null) {
                return NotFound();
            }

            return View(order);
        }

        // GET: ordersController/Create
        public IActionResult Create() => View();
        // POST: ordersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order) {
            try {
                if(ModelState.IsValid) {
                    orderRepository.AddOrder(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: ordersController/Edit/2
        public IActionResult Edit(int? id) {
            if (id == null) {
                NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null) {
                return NotFound();
            }

            return View(order);
        }
        // POST: ordersController/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order) {
            try {
                if (id != order.OrderId) {
                    return NotFound();
                }
                if(ModelState.IsValid) {
                    orderRepository.UpdateOrder(order);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ordersController/Delete/1
        public IActionResult Delete(int? id) {
            if(id == null) {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if(order == null) {
                return NotFound();
            }

            return View(order);
        }
        // POST: ordersController/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            try {
                orderRepository.RemoveOrder(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

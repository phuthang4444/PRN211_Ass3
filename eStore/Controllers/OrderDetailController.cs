using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers {
    public class OrderDetailController : Controller {
        IOrderDetailRepository orderDetailRepository = null;

        public OrderDetailController() => orderDetailRepository = new OrderDetailRepository();

        // GET: orderDetailsController
        public IActionResult Index() {
            var orderDetailList = orderDetailRepository.GetOrderDetails();
            return View(orderDetailList);
        }

        // GET: orderDetailsController/Details/2
        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: orderDetailsController/Create
        public IActionResult Create() => View();
        // POST: orderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderDetail orderDetail) {
            try {
                if (ModelState.IsValid) {
                    orderDetailRepository.AddOrderDetail(orderDetail);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View(orderDetail);
            }
        }

        // GET: orderDetailsController/Edit/2
        public IActionResult Edit(int? id) {
            if (id == null) {
                NotFound();
            }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) {
                return NotFound();
            }

            return View(orderDetail);
        }
        // POST: orderDetailsController/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderDetail orderDetail) {
            try {
                if (id != orderDetail.OrderId) {
                    return NotFound();
                }
                if (ModelState.IsValid) {
                    orderDetailRepository.UpdateOrderDetail(orderDetail);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: orderDetailsController/Delete/1
        public IActionResult Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) {
                return NotFound();
            }

            return View(orderDetail);
        }
        // POST: orderDetailsController/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            try {
                orderDetailRepository.RemoveOrderDetail(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eStore.Controllers
{
    public class MemerController : Controller
    {
        private readonly eStoreContext context;
        private readonly MemberDAO dao;
        public MemerController(eStoreContext context)
        {
            this.context = context;
        }

        // GET: MemerController
        public ActionResult Index()
        {
            var model = context.Members.ToList();
            return View(model);
        }

        // GET: MemerController/Details/5
        public ActionResult Details(string email)
        {
            if(email == null)
            {
                return NotFound();
            }
            var member = context.Members.FirstOrDefault(m => m.Email == email);
            if(member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: MemerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            
                if (ModelState.IsValid) { 
                    dao.AddNew(member);
                return RedirectToAction(nameof(Index));
                }
            
                return View(member);
            
        }

        // GET: MemerController/Edit/5
        public ActionResult Edit(string? email)
        {
            if(email == null) { return NotFound(); }
            var member = dao.GetMemberByEmail(email);
            if(member == null) { return NotFound(); }
            return View(member);
        }

        // POST: MemerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string? email, Member member)
        {
            if (email != member.Email) { return NotFound(); }
            if (ModelState.IsValid)
            {
                dao.Update(member);
                return RedirectToAction(nameof(Index));
            }
             return View(member);
            
        }

        // GET: MemerController/Delete/5
        public ActionResult Delete(string? email)
        {
            dao.Remove(email);
            return RedirectToAction(nameof(Index));
        }

        // POST: MemerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

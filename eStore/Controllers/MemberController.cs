using BusinessObject.Models;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        private readonly eStoreContext context;
        public MemberController(eStoreContext context)
        {
            this.context = context;
        }
        /*private readonly MemberDAO dao;*/
        private readonly MemberRepository dao = new MemberRepository();
        // GET: MemberController
        public ActionResult Index()
        {
            var model = context.Members.ToList();
            return View(model);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var member = context.Members.FirstOrDefault(m => m.MemberId == id);
            if(member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                dao.AddNewMember(member);
                return RedirectToAction(nameof(Index));
            }
                
            
                return View(member);
            
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(string? email)
        {
            if (email == null)
            {
                return NotFound();
            }
            var member = context.Members.Find(email);
            if(member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string? email, Member member)
        {
            if (email != member.Email)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                dao.UpdateMember(member);
                return RedirectToAction(nameof(Index));
            }
            
            
                return View(member);
            
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(string? email)
        {
            var member = dao.RemoveMember(email);
            return RedirectToAction(nameof(Index));
        }

        // POST: MemberController/Delete/5
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

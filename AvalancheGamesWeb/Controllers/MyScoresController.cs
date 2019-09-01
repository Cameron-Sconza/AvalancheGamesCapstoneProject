using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheGamesWeb.Controllers
{
    public class MyScoresController : Controller
    {
        public ActionResult Page(int? PageNumber, int? PageSize)
        {
            int PageN = (PageNumber.HasValue) ? PageNumber.Value : 0;
            int PageS = (PageSize.HasValue) ? PageSize.Value : ApplicationConfig.DefaultPageSize;
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<ScoreBLL> Model = new List<ScoreBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    UserBLL me = ctx.FindUserByUserName(User.Identity.Name);
                    ViewBag.TotalCount = ctx.ObtainUserScoreCount(me.UserID);
                    Model = ctx.GetScoresReltatedToUserID(me.UserID, PageN * PageS, PageS);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
        // GET: MyScores
        public ActionResult Index()
        {
            List<ScoreBLL> Model = new List<ScoreBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    var user = ctx.FindUserByUserName(User.Identity.Name);
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    int count = ctx.ObtainUserScoreCount(user.UserID);
                    ViewBag.TotalCount = ctx.ObtainUserScoreCount(user.UserID);
                    Model = ctx.GetScoresReltatedToUserID(user.UserID, 0, ViewBag.PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }

        // GET: MyScores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyScores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyScores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MyScores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyScores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MyScores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyScores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

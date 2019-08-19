using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvalancheGamesWeb.Controllers
{
    public class ScoreController : Controller
    {
        // GET: Score
        public ActionResult Index()
        {
            return View();
        }

        // GET: Score/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Score/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Score/Create
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

        // GET: Score/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Score/Edit/5
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

        // GET: Score/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Score/Delete/5
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

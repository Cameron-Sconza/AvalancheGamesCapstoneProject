using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace AvalancheGamesWeb.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Page (int? PageNumber, int? PageSize)
        {
            int PageN = (PageNumber.HasValue) ? PageNumber.Value : 0;
            int PageS = (PageSize.HasValue) ? PageSize.Value : ApplicationConfig.DefaultPageSize;
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<GameBLL> Model = new List<GameBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainGameCount();
                    Model = ctx.GetGames(PageN * PageS, PageS);

                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Errorr");
            }
        }
        // GET: Game
        public ActionResult Index()
        {
            List<GameBLL> Model = new List<GameBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalCount = ctx.ObtainGameCount();
                    Model = ctx.GetGames(0, ViewBag.PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);//model is list of roles, name of view is same as method name
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            GameBLL Game;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Game = ctx.FindGameByGameID(id);
                    if (null == Game)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Game);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            GameBLL defGame = new GameBLL();
            defGame.GameID = 0;
            return View(defGame);
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.GameBLL collection)
             {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateGame(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            GameBLL Game;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Game = ctx.FindGameByGameID(id);
                    if (null == Game)
                    {
                        return View("ItemNotFound");

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Game);
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.GameBLL collection)
        {
            try
            { // TODO: Add update logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateGame(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex;
                return View("Error");
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            GameBLL Game;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Game = ctx.FindGameByGameID(id);
                    if (null == Game)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Game);
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,BusinessLogicLayer.GameBLL collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteGame(id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
    }
}

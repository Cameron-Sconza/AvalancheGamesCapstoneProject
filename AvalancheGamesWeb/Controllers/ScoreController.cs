using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;


namespace AvalancheGamesWeb.Controllers
{
       public class ScoreController : Controller
    {
        List<SelectListItem> GetUserItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<UserBLL> users = ctx.GetUsers(0, 25);
                foreach (UserBLL user in users)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = user.UserID.ToString();
                    item.Text = user.Email;
                    ProposedReturnValue.Add(item);
                }
            }
            return ProposedReturnValue;
        }
        List<SelectListItem> GetGameItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<GameBLL> games = ctx.GetGames(0, 25);
                foreach (GameBLL game in games)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = game.GameID.ToString();
                    item.Text = game.GameName;
                    ProposedReturnValue.Add(item);
                }
            }
            return ProposedReturnValue;
        }
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
                    ViewBag.TotalCount = ctx.ObtainScoreCount();
                    Model = ctx.GetScores(PageN * PageS, PageS);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
        // GET: Score
        public ActionResult Index()
        {

            List<ScoreBLL> Model = new List<ScoreBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalCount = ctx.ObtainScoreCount();
                    Model = ctx.GetScores(0, ViewBag.PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }

        // GET: Score/Details/5
        public ActionResult Details(int id)
        {
            ScoreBLL Score;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Score = ctx.FindScoreByScoreID(id);
                    if (null == Score)
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
            return View(Score);
        }
        // GET: Score/Create
        public ActionResult Create()
        {
            ScoreBLL defScore = new ScoreBLL();
            defScore.ScoreID = 0;
            ViewBag.GameName = GetGameItems();
            ViewBag.Email = GetGameItems();
            return View(defScore);
        }

        // POST: Score/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.ScoreBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateScore(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Score/Edit/5
        public ActionResult Edit(int id)
        {
            ScoreBLL Score;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Score = ctx.FindScoreByScoreID(id);
                    if (null == Score)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Excption = ex;
                return View("Error");
            }
            ViewBag.Email = GetUserItems();
            ViewBag.GameName = GetGameItems();
            return View(Score);
        }

        // POST: Score/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.ScoreBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add update logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateScore(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Score/Delete/5
        public ActionResult Delete(int id)
        {
            ScoreBLL Score;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Score = ctx.FindScoreByScoreID(id);
                    if (null == Score)
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
            return View(Score);
        }
        // POST: Score/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.ScoreBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteScore(id);
                }
                // TODO: Add delete logic here

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

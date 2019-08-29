using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AvalancheGamesWeb.Controllers
{
    public class CommentController : Controller
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
            List<CommentBLL> Model = new List<CommentBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainCommentCount();
                    Model = ctx.GetComments(PageN * PageS, PageS);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
        // GET: Comment
        public ActionResult Index()
        {

            List<CommentBLL> Model = new List<CommentBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalCount = ctx.ObtainCommentCount();
                    Model = ctx.GetComments(0, ViewBag.PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            CommentBLL Comment;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Comment = ctx.FindCommentByCommentID(id);
                    if (null == Comment)
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
            return View(Comment);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            CommentBLL defComment = new CommentBLL();
            defComment.CommentID = 0;
            ViewBag.GameName = GetGameItems();
            ViewBag.Email = GetGameItems();
            return View(defComment);
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.CommentBLL collection)
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
                    ctx.CreateComment(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            CommentBLL Comment;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Comment = ctx.FindCommentByCommentID(id);
                    if (null == Comment)
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
            return View(Comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.CommentBLL collection)
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
                    ctx.UpdateComment(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            CommentBLL Comment;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Comment = ctx.FindCommentByCommentID(id);
                    if (null == Comment)
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
            return View(Comment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.CommentBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteComment(id);
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

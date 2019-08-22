using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;


namespace AvalancheGamesWeb.Controllers
{
    public class UserController : Controller
    {
        List<SelectListItem> GetRoleItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<RoleBLL> roles = ctx.GetRoles(0, 25);
                foreach (RoleBLL role in roles)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = role.RoleID.ToString();
                    item.Text = role.RoleName;
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
            List<UserBLL> Model = new List<UserBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(PageN * PageS, PageS);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
        // GET: User
        public ActionResult Index()
        {
            List<UserBLL> Model = new List<UserBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(0, ViewBag.PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);//model is list of roles, name of view is same as method  name
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if (null == User)
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
            return View(User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserBLL defUser = new UserBLL();
            defUser.UserID = 0;
            ViewBag.Role = GetRoleItems();
            return View(defUser);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.UserBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateUser(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if(null == User)
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
            ViewBag.Roles = GetRoleItems();
            return View(User);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.UserBLL collection)
        {
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateUser(collection);
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if (null == User)
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
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,BusinessLogicLayer.UserBLL collection)
        {
            try
            {  
                // TODO: Add delete logic here
            using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteUser(id);
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

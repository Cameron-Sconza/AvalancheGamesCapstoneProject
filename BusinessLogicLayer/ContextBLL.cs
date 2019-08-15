using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ContextBLL : IDisposable
    #region Context stuff

    {
        DataAccessLayer.ContextDAL _context = new DataAccessLayer.ContextDAL();
        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
        bool Log(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        public ContextBLL()
        {
            try
            {
                string connectionstring;
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                _context.ConnectionString = connectionstring;
            }
            catch (Exception ex) when (Log(ex))
            {
                //this exception does not have a reasonable handler, simply log it
            }
        }
        public void GenerateNotConnected()
        {
            _context.GenerateNotConnected();
        }
        public void GenerateStoredProcedureNotFound()
        {
            _context.GenerateStoredProcedureNotFound();
        }
        public void GenerateParameterNotIncluded()
        {
            _context.GenerateParametersNotIncluded();
        }
        #endregion
        #region UserBLL
        public int CreateUser(string FirstName, string LastName, string UserName, string Email, int PhoneNumber, string SALT, string HASH, DateTime DateOfBirth, int RoleID)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(FirstName, LastName, UserName, Email, PhoneNumber, SALT, HASH, DateOfBirth, RoleID);
            return proposedReturnValue;
        }
        public int CreateUser(UserBLL user)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, user.SALT, user.HASH, user.DateOfBirth, user.RoleID);
            return proposedReturnValue;
        }
        public void UpdateUser(int UserID, string FirstName, string LastName, string UserName, string Email, int PhoneNumber, string SALT, string HASH, DateTime DateOfBirth, int RoleID)
        {
            _context.UpdateUser(UserID, FirstName, LastName, UserName, Email, PhoneNumber, SALT, HASH, DateOfBirth, RoleID);
        }
        public void UpdateUser(UserBLL user)
        {
            _context.UpdateUser(user.UserID, user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, user.SALT, user.HASH, user.DateOfBirth, user.RoleID);
        }
        public void DeleteUser(int UserID)
        {
            _context.DeleteUser(UserID);
        }
        public void DeleteUser(UserBLL user)
        {
            _context.DeleteUser(user.UserID);
        }
        public UserBLL FindUserByUserID(int UserID)
        {
            UserBLL proposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByUserID(UserID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new UserBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }
        public UserBLL FindUserByUserEmail(string Email)
        {
            UserBLL proposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByUserEmail(Email);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new UserBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }
        public List<UserBLL> GetUsers(int skip, int take)
        {
            List<UserBLL> proposedReturnValue = new List<UserBLL>();
            List<UserDAL> ListOfDataLayerObjects = _context.GetUsers(skip, take);
            foreach (UserDAL User in ListOfDataLayerObjects)
            {
                UserBLL BusinessObject = new UserBLL(User);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        public List<UserBLL> GetUsersRelatedToRoleID(int RoleID, int skip, int take)
        {
            List<UserBLL> proposedReturnValue = new List<UserBLL>();
            List<UserDAL> ListOfDataLayerObjects = _context.GetUsersRelatedToRoleID(RoleID, skip, take);
            foreach (UserDAL User in ListOfDataLayerObjects)
            {
                UserBLL BusinessObject = new UserBLL(User);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        public int ObtainUserCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainUserCount();
            return proposedReturnValue;
        }
        #endregion
        #region RoleBLL
        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(RoleName);
            return proposedReturnValue;
        }
        public int CreateRole(RoleBLL role)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(role.RoleName);
            return proposedReturnValue;
        }
        public void UpdateRole(int RoleID, string RoleName)
        {
            _context.UpdateRole(RoleID, RoleName);
        }
        public void UpdateRole(RoleBLL role)
        {
            _context.UpdateRole(role.RoleID, role.RoleName);
        }
        public void DeleteRole(int RoleID)
        {
            _context.DeleteRole(RoleID);
        }
        public void DeleteRole(RoleBLL role)
        {
            _context.DeleteRole(role.RoleID);
        }
        public RoleBLL FindRoleByRoleID(int RoleID)
        {
            RoleBLL proposedReturnValue = null;
            RoleDAL DataLayerObject = _context.FindRoleByRoleID(RoleID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new RoleBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }
        public List<RoleBLL> GetRoles(int skip, int take)
        {
            List<RoleBLL> proposedReturnValue = new List<RoleBLL>();
            List<RoleDAL> ListOfDataLayerObjects = _context.GetRoles(skip, take);
            foreach (RoleDAL role in ListOfDataLayerObjects)
            {
                RoleBLL BusinessObject = new RoleBLL(role);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        public int ObtainRoleCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainRoleCount();
            return proposedReturnValue;
        }
        #endregion
        #region CommentBLL
        public int CreateComment(string GameComment, int UserID, int GameID, bool Liked)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateComment(GameComment, UserID, GameID, Liked);
            return proposedReturnValue;
        }
        public int CreateComment(CommentBLL comment)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateComment(comment.GameComment, comment.UserID, comment.GameID, comment.Liked);
            return proposedReturnValue;
        }
        public void UpdateComment(int CommentID, string GameComment, int UserID, int GameID, bool Liked)
        {
            _context.JustUpdateComment(CommentID, GameComment, UserID, GameID, Liked);
        }
        public void UpdateComment(CommentBLL comment)
        {
            _context.JustUpdateComment(comment.CommentID, comment.GameComment, comment.UserID, comment.GameID, comment.Liked);
        }
        public void DeleteComment(int CommentID)
        {
            _context.DeleteComment(CommentID);
        }
        public void DeleteComment(CommentBLL comment)
        {
            _context.DeleteComment(comment.CommentID);
        }
        public CommentBLL FindCommentByCommentID(int CommentID)
        {
            CommentBLL proposedReturnValue = null;
            CommentDAL DataLayerObject = _context.FindCommentByCommentID(CommentID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new CommentBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }
        public List<CommentBLL> GetComments(int skip, int take)
        {
            List<CommentBLL> proposedReturnValue = new List<CommentBLL>();
            List<CommentDAL> ListOfDataLayerObjects = _context.GetComments(skip, take);
            foreach(CommentDAL comment in ListOfDataLayerObjects)
            {
                CommentBLL BusinessObject = new CommentBLL(comment);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        public List<CommentBLL> GetCommentsRelatedToGameID(int GameID, int skip, int take)
        {
            List<CommentBLL> proposedReturnValue = new List<CommentBLL>();
            List<CommentDAL> ListOfDataLayerObjects = _context.GetCommentsRelatedToGameID(GameID, skip, take);
            foreach (CommentDAL comment in ListOfDataLayerObjects)
            {
                CommentBLL BusinessObject = new CommentBLL(comment);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        public List<CommentBLL> GetCommentsRelatedToUserID(int UserID, int skip, int take)
        {
            List<CommentBLL> proposedReturnValue = new List<CommentBLL>();
            List<CommentDAL> ListOfDataLayerObjects = _context.GetCommentsRelatedToUserID(UserID, skip, take);
            foreach (CommentDAL comment in ListOfDataLayerObjects)
            {
                CommentBLL BusinessObject = new CommentBLL(comment);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }
        #endregion
    }
}

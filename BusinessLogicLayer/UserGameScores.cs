
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccessLayer;

namespace BusinessLogicLayer

{
    public class UserGameScores
    {
        public List<SelectListItem> GetGameItems(ContextBLL ctx)
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            List<GameBLL> games = ctx.GetGames(0, 25);
            foreach (GameBLL game in games)
            {
                SelectListItem item = new SelectListItem();
                item.Value = game.GameID.ToString();
                item.Text = game.GameName;
                ProposedReturnValue.Add(item);
            }
            return ProposedReturnValue;
        }
    //    public UserBLL FindUserByUserID(int UserID)
    //    {
    //        UserBLL ProposedReturnValue = null;
    //        UserDAL DataLayerObject = _context.FindUserByUserID(UserID);
    //        if (null != DataLayerObject)
    //        {
    //            ProposedReturnValue = new UserBLL(DataLayerObject);
    //        }
    //        return ProposedReturnValue;
    //    }
    }
}

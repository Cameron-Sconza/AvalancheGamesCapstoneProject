using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    //will add meaningful calculation here

    public class ScoreBLL
    {
        public ScoreBLL()
        {

        }
        #region Direct Properties
        public int ScoreID { get; set; }
        public int Score { get; set; }
        public int UserID { get; set; }
        public int GameID { get; set; }
        public int AmountPlayed { get; set; }
        #endregion
        #region Indirect Properties
        public string Email { get; set; }
        public string GameName { get; set; }
        #endregion
        public ScoreBLL(ScoreDAL dal)
        {
            this.ScoreID = dal.ScoreID;
            this.Score = dal.Score;
            this.UserID = dal.UserID;
            this.GameID = dal.GameID;
            this.AmountPlayed = dal.AmountPlayed;
            this.Email = dal.Email;
            this.GameName = dal.GameName;
        }
        public override string ToString()
        {
            return $"ScoreID: {ScoreID} Score: {Score} UserID: {UserID} GameID: {GameID} AmountPlayed: {AmountPlayed} Email: {Email} GameName: {GameName}";
        }
    }
}

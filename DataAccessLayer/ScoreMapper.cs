using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ScoreMapper : Mapper
    {
        int OffsetToScoreID;
        int OffsetToScore;
        int OffsetToUserID;
        int OffsetToGameID;
        int OffsetToAmountPlayed;
        int OffsetToEmail;
        int OffsetToGameName;
        
        public ScoreMapper(System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToScoreID = reader.GetOrdinal("ScoreID");
            Assert(0 == OffsetToScoreID, $"ScoreID is {OffsetToScoreID} not 0 as expected");
            OffsetToScore = reader.GetOrdinal("Score");
            Assert(1 == OffsetToScore, $"Score is {OffsetToScore} not 1 as expected");
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(2 == OffsetToUserID, $"UserID is {OffsetToUserID} not 2 as expected");
            OffsetToGameID = reader.GetOrdinal("GameID");
            Assert(3 == OffsetToGameID, $"GameID is {OffsetToGameID} not 3 as expected");
            OffsetToAmountPlayed = reader.GetOrdinal("AmountPlayed");
            Assert(4 == OffsetToAmountPlayed, $"AmountPlayed in {OffsetToAmountPlayed} not 4 as expected");
            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(5 == OffsetToEmail, $"Email is {OffsetToEmail} not 5 as expected");
            OffsetToGameName = reader.GetOrdinal("GameName");
            Assert(6 == OffsetToGameName, $"GameName is {OffsetToGameName} not 6 as expected");
        }

        public ScoreDAL ScoreFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            ScoreDAL ProposedReturnValue = new ScoreDAL();
            ProposedReturnValue.ScoreID = reader.GetInt32(OffsetToScoreID);
            ProposedReturnValue.Score = reader.GetInt32(OffsetToScore);
            ProposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            ProposedReturnValue.GameID = reader.GetInt32(OffsetToGameID);
            ProposedReturnValue.AmountPlayed = reader.GetInt32(OffsetToAmountPlayed);
            ProposedReturnValue.Email = reader.GetString(OffsetToEmail);
            ProposedReturnValue.GameName = reader.GetString(OffsetToGameName);
            return ProposedReturnValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LogEntriesMapper : Mapper 
    {
        int OffsetToLogEntryID;
        int OffsetToMessage;
        int OffsetToTimeOfException;
        int OffsetToLogComments;
        int OffsetToCategory;
        int OffsetToErrorLevel;
        
        public LogEntriesMapper (System.Data.SqlClient.SqlDataReader reader)
        {
            OffsetToLogEntryID = reader.GetOrdinal("LogEntryID");
            Assert(0 == OffsetToLogEntryID, $"LogEntryID is {OffsetToLogEntryID} not 0 as expected");
            OffsetToMessage = reader.GetOrdinal("Message");
            Assert(1 == OffsetToMessage, $"Message is {OffsetToMessage} not 1 as expected");
            OffsetToTimeOfException = reader.GetOrdinal("TimeOfException");
            Assert(2 == OffsetToTimeOfException, $"TimeOfException is {OffsetToTimeOfException} not 2 as expected");
            OffsetToLogComments = reader.GetOrdinal("LogComments");
            Assert(3 == OffsetToLogComments, $"LogComments is {OffsetToLogComments} not 3 as expected");
            OffsetToCategory = reader.GetOrdinal("Category");
            Assert(4 == OffsetToCategory, $"Category is{OffsetToCategory} not 4 as expected");
            OffsetToErrorLevel = reader.GetOrdinal("ErrolLevel");
            Assert(5 == OffsetToErrorLevel, $"ErrorLevel is {OffsetToErrorLevel} not 5 as expected");
        }
        public LogEntriesDAL LogEntriesFromReader (System.Data.SqlClient.SqlDataReader reader)
        {
            LogEntriesDAL ProposedReturnValue = new LogEntriesDAL();
            ProposedReturnValue.LogEntryID = reader.GetInt32(OffsetToLogEntryID);
            ProposedReturnValue.Message = reader.GetString(OffsetToMessage);
            ProposedReturnValue.TimeOfException = reader.GetDateTime(OffsetToTimeOfException);
            ProposedReturnValue.LogComments = reader.GetString(OffsetToLogComments);
            ProposedReturnValue.Category = reader.GetString(OffsetToCategory);
            ProposedReturnValue.ErrorLevel = reader.GetString(OffsetToErrorLevel);
            return ProposedReturnValue;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class LogEntriesBLL
    {
        public LogEntriesBLL()
        {

        }
        public int LogEntryID { get; set; }
        public string Message { get; set; }
        public DateTime TimeOfException { get; set; }
        public string LogComments { get; set; }
        public string Category { get; set; }
        public string ErrorLevel { get; set; }
        public LogEntriesBLL(LogEntriesDAL dal)
        {
            this.LogEntryID = dal.LogEntryID;
            this.Message = dal.Message;
            this.TimeOfException = dal.TimeOfException;
            this.LogComments = dal.LogComments;
            this.Category = dal.Category;
            this.ErrorLevel = dal.ErrorLevel;
        }
        public override string ToString()
        {
            return $"LogEntry: LogEntryID: {LogEntryID} Message: {Message} TimeOfException: {TimeOfException} LogComments: {LogComments} Category: {Category} ErrorLevel: {ErrorLevel}";
        }
    }
}

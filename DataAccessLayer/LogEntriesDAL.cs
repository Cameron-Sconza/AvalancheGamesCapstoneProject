using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LogEntriesDAL
    {
        public int LogEntryID { get; set; }
        public string Message { get; set; }
        public DateTime TimeOfException { get; set; }
        public string LogComments { get; set; }
        public string Category { get; set; }
        public string ErrorLevel { get; set; }
        public override string ToString()
        {
            return $"LogEntry: LogEntryID: {LogEntryID} Message: {Message} TimeOfException: {TimeOfException} LogComments: {LogComments} Category: {Category} ErrorLevel: {ErrorLevel}";
        }
    }
}

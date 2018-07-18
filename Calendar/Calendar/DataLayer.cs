using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Calendar
{
    public class EventDatabase
    {
        private SQLiteConnection dbConn;
        public string StatusMessage { get; set; }

        public EventDatabase(string dbPath)
        {
            //initialize a new SQLiteConnection 
            if (dbConn == null)
            {
                dbConn = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadOnly, false);
                dbConn.CreateTable<EventInfo>();
            }
        }

        public List<EventInfo> GetDataFromDB(string condition)
        {
            string query = "SELECT * FROM EventInfo";
            List<EventInfo> results = dbConn.Query<EventInfo>(query);
            return results;
        }
    }

    [Table("EventInfo")]
    public class EventInfo
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Subject { get; set; }
        public string Color { get; set; }
    }
}

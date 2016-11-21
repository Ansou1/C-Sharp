using System;

namespace MusicSheetWriter.DataModel
{
    class connectionData
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public DateTime LastActivityDate { get; set; }

        public override string ToString()
        {
            return "ID: " + Id +
                "\nUsername: " + Username +
                "\nlast_activity_date" + LastActivityDate;
        }
    }
}

using System;

namespace MusicSheetWriter
{
    class Score
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String LocationPreview { get; set; }
        public String LocationProject { get; set; }
        public Boolean Is_Favourite { get; set; }
        public User Compositor { get; set; }

        public Score()
        {
            Compositor = new User();
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Title + " by " + Compositor.PersonalData.Username;
        }
    }
}

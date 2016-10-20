using MSW_10_UWA.Common;
using Music_Sheet_Writer;
using Music_Sheet_Writer.Models;
using MusicSheetWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_10_UWA.Models
{
    public class Score : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private String title;
        public String Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private String locationPreview;
        public String LocationPreview
        {
            get { return locationPreview; }
            set
            {
                locationPreview = value;
                OnPropertyChanged("LocationPreview");
            }
        }

        private String locationProject;
        public String LocationProject
        {
            get { return locationProject; }
            set
            {
                locationProject = value;
                OnPropertyChanged("LocationProject");
            }
        }

        private Boolean is_Favourite;
        public Boolean Is_Favourite
        {
            get { return is_Favourite; }
            set
            {
                is_Favourite = value;
                OnPropertyChanged("Is_Favourite");
            }
        }

        private User compositor;
        public User Compositor
        {
            get { return compositor; }
            set
            {
                compositor = value;
                OnPropertyChanged("Compositor");
            }
        }

        public Score()
        {
            Compositor = new User();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

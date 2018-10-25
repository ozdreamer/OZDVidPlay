using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OZDVidPlay
{
    public class PlayListViewModel : BaseViewModel
    {
        private readonly PlayList playList;

        private ICollection<Video> videos;

        public ICollection<Video> Videos
        {
            get
            {
                return this.videos;
            }

            set
            {
                if (this.videos != value)
                {
                    this.videos = value;
                    this.NotifyOfPropertyChange(() => this.Videos);
                }
            }
        }

        public PlayListViewModel(PlayList playList) : base()
        {
            this.playList = playList;
        }

        public void AddVideoToPlayList(string path)
        {
            var video = new Video
            {
                Id = this.DatabaseManager.NextVideoId,
                PlayListId = this.playList.Id,
                Name = Guid.NewGuid().ToString(),
                Path = path
            };

            this.DatabaseManager.AddVideo(video);
            this.Videos.Add(video);
        }

        public void LoadVideos()
        {
            this.Videos = new ObservableCollection<Video>(this.DatabaseManager.GetVideos(playList.Id));
        }
    }
}


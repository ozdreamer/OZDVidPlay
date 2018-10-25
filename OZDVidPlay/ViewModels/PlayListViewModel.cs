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
                Id = App.DatabaseManager.NextVideoId,
                PlayListId = this.playList.Id,
                Name = Guid.NewGuid().ToString(),
                Path = path
            };

            App.DatabaseManager.AddVideo(video);
            this.Videos.Add(video);
        }

        public void Play()
        {
            this.Navigate?.Invoke(new VideoPlayerPage(this.Videos));
        }

        public void LoadVideos()
        {
            this.Videos = new ObservableCollection<Video>(App.DatabaseManager.GetVideos(playList.Id));
        }
    }
}


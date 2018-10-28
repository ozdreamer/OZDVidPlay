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

        private Video selectedVideo;

        public Video SelectedVideo
        {
            get
            {
                return this.selectedVideo;
            }

            set
            {
                if (this.selectedVideo != value)
                {
                    this.selectedVideo = value;
                    this.NotifyOfPropertyChange(() => this.SelectedVideo);
                }
            }
        }

        public PlayListViewModel(PlayList playList) : base()
        {
            this.playList = playList;
        }

        public void Remove()
        {
            if (this.SelectedVideo == null)
            {
                return;
            }

            if (App.DatabaseManager.RemoveVideo(this.SelectedVideo.Id))
            {
                this.Videos.Remove(this.SelectedVideo);
            }
        }

        public void AddVideoToPlayList(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            var video = new Video
            {
                Id = this.Db.NextVideoId,
                PlayListId = this.playList.Id,
                Name = Guid.NewGuid().ToString(),
                Path = path,
                Sequence = this.Db.NextVideoSequence(this.playList.Id)
            };

            this.Videos.Add(this.Db.AddVideo(video));
        }

        public void Play()
        {
            this.Navigate?.Invoke(this.CreatePage(typeof(VideoPlayerPage), this.Videos));
        }

        public void LoadVideos()
        {
            this.Videos = new ObservableCollection<Video>(this.Db.GetVideos(playList.Id));
        }
    }
}


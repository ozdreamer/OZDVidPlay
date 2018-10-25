using System;
using System.Collections.Generic;

namespace OZDVidPlay
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        private readonly IEnumerable<Video> videos;

        public VideoPlayerViewModel(IEnumerable<Video> videos)
        {
            this.videos = new List<Video>(videos);
        }

        public void Play(VideoPlayer player)
        {
            foreach (var video in this.videos)
            {
                player.Source = VideoSource.FromFile(video.Path);
                player.Play();
            }
        }
    }
}

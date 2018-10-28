using System;
using System.Collections.Generic;
using System.Linq;

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
            player.Source = this.videos.Select(v => new FileVideoSource { File = v.Path });
            player.Play();
        }
    }
}

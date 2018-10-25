using System.Collections.Generic;

namespace OZDVidPlay
{
    public partial class VideoPlayerPage : BaseContentPage
    {
        private VideoPlayerViewModel pageViewModel => this.ViewModel as VideoPlayerViewModel;

        public VideoPlayerPage(IEnumerable<Video> videos) : base(new VideoPlayerViewModel(videos))
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.pageViewModel.Play(this.videoPlayer);
        }
    }
}

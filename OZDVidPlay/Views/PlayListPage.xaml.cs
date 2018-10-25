using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace OZDVidPlay
{
    public partial class PlayListPage : BaseContentPage
    {
        private PlayListViewModel pageViewModel => this.ViewModel as PlayListViewModel;

        public PlayListPage(PlayList playList) : base(new PlayListViewModel(playList))
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.pageViewModel.LoadVideos();
        }

        async public void OnSelectVideoClicked(object sender, EventArgs e)
        {
            string filePath = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();
            this.pageViewModel.AddVideoToPlayList(filePath);
        }

        void OnPlayVideoClicked(object sender, System.EventArgs e)
        {
            this.pageViewModel.Play();
        }
    }
}

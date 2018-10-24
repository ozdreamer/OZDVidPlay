﻿using System;
using Xamarin.Forms;

namespace OZDVidPlay
{
    public partial class VideoPlayerPage : BaseContentPage
    {
        public VideoPlayerPage() : base(new VideoPlayerViewModel())
        {
            InitializeComponent();
        }

        async void OnShowVideoLibraryClicked(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = false;

            string filename = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();

            if (!String.IsNullOrWhiteSpace(filename))
            {
                videoPlayer.Source = new FileVideoSource
                {
                    File = filename
                };
            }

            btn.IsEnabled = true;
        }
    }
}
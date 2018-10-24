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
    }
}

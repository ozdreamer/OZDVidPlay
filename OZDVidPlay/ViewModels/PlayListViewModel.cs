using System;

using Xamarin.Forms;

namespace OZDVidPlay
{
    public class PlayListViewModel : BaseViewModel
    {
        private readonly PlayList playList;

        public PlayListViewModel(PlayList playList)
        {
            this.playList = playList;
        }
    }
}


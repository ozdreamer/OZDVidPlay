using System.Collections.ObjectModel;

namespace OZDVidPlay
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields and Properties

        private ObservableCollection<PlayList> playLists;

        public ObservableCollection<PlayList> PlayLists
        {
            get
            {
                return this.playLists;
            }

            set
            {
                if (this.playLists != value)
                {
                    this.playLists = value;
                    this.NotifyOfPropertyChange(() => this.PlayLists);
                }
            }
        }

        public void OpenPlayList(PlayList playList)
        {
            this.Navigate?.Invoke(this.CreatePage(typeof(PlayListPage), playList));
        }

        #endregion

        #region Methods

        public void LoadPlayLists()
        {
            this.PlayLists = new ObservableCollection<PlayList>(this.Db.GetAllPlayLists());
        }

        #endregion
    }
}


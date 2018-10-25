using System.Collections.Generic;
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

        #endregion

        #region Methods

        public void LoadPlayLists()
        {
            this.PlayLists = new ObservableCollection<PlayList>(App.DatabaseManager.GetAllPlayLists());
        }

        #endregion
    }
}


using System;
using Caliburn.Micro;
using Xamarin.Forms;

namespace OZDVidPlay
{
    public class BaseViewModel : PropertyChangedBase
    {
        private string title = string.Empty;

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        /// 
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(this.Title)); }
        }

        private string subTitle = string.Empty;

        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public string Subtitle
        {
            get { return subTitle; }
            set { Set(ref subTitle, value, nameof(this.Subtitle)); }
        }

        private string icon = null;

        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { Set(ref icon, value, nameof(this.Icon)); }
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value, nameof(this.IsBusy)); }
        }

        private bool canLoadMore = true;

        /// <summary>
        /// Gets or sets if we can load more.
        /// </summary>
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { Set(ref canLoadMore, value, nameof(this.CanLoadMore)); }
        }

        protected DatabaseManager Db => App.DatabaseManager;

        public Action<string> ShowError;

        public Action<string> ShowInformation;

        public Action<Page> Navigate;

        protected Page CreatePage(Type pageType, params object[] args) => Activator.CreateInstance(pageType, args) as Page;
    }
}

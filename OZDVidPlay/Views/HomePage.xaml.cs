using System;
using Xamarin.Forms;

namespace OZDVidPlay
{
    public partial class HomePage : BaseContentPage
    {
        private HomeViewModel pageViewModel => this.ViewModel as HomeViewModel;

        public HomePage() : base(new HomeViewModel())
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.pageViewModel.LoadPlayLists();
        }

        void OnPlayListTapped(object sender, EventArgs e)
        {
            this.pageViewModel.OpenPlayList((sender as ViewCell).BindingContext as PlayList);
        }
    }
}

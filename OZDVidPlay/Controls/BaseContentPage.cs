using System;
using Xamarin.Forms;

namespace OZDVidPlay
{
    public class BaseContentPage : ContentPage
    {
        protected BaseViewModel ViewModel => this.BindingContext as BaseViewModel;

        protected BaseContentPage(BaseViewModel viewModel)
        {
            this.BindingContext = viewModel;

            viewModel.ShowError = (message) =>
            {
                this.DisplayAlert("Error", message, "Cancel");
            };

            viewModel.ShowInformation = (message) =>
            {
                this.DisplayAlert("Information", message, "Cancel");
            };

            viewModel.Navigate = (page) =>
            {
                Navigation.PushAsync(page);
            };
        }
    }
}

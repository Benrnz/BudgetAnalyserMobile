using System;
using Xamarin.Forms;

namespace BAXMobile.Overview
{
    public partial class OverviewPage : ContentPage
    {
        public OverviewPage()
        {
            InitializeComponent();
        }

        private OverviewViewModel ViewModel => (OverviewViewModel) BindingContext;

        private async void OnPageAppearing(object sender, EventArgs e)
        {
            await ViewModel.PageIsLoading();
        }
    }
}
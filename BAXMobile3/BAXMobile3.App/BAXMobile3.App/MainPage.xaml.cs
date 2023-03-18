using System;

namespace BAXMobile
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private MainViewModel ViewModel => (MainViewModel) BindingContext;

        private async void OnPageAppearing(object sender, EventArgs e)
        {
            await ViewModel.PageIsLoading();
        }
    }
}
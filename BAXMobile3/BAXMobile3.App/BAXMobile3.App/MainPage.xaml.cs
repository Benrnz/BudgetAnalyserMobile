using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BAXMobile
{
    public partial class MainPage : TabbedPage
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
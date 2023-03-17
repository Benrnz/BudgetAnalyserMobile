using System;
using Xamarin.Forms;

namespace BAXMobile.Buckets
{
    public partial class BucketViewPage : ContentPage
    {
        public BucketViewPage()
        {
            InitializeComponent();
        }

        private async void OnAppearing(object sender, EventArgs e)
        {
            if (BindingContext == null) return;
            var controller = (LedgerBucketViewModel) BindingContext;
            await controller.PageIsLoading();

            var changeValueAction = new Action<double>(d => this.fundsVisual.HeightRequest = d);
            double targetHeight;
            if (controller.OpeningBalance == 0)
            {
                targetHeight = 0;
            }
            else
            {
                var percentage = (double)controller.RemainingBalance / (double)controller.OpeningBalance;
                targetHeight = 200 * percentage;
            }

            this.fundsVisual.Animate("HeightRequest", changeValueAction, 200D, targetHeight, length: 750, easing: Easing.BounceOut);
        }
    }
}
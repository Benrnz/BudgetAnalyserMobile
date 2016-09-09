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

            if (controller.OpeningBalance == 0)
            {
                await this.progressBar.ProgressTo(0, 750, Easing.BounceOut);
            }
            else
            {
                var percentage = controller.RemainingBalance / controller.OpeningBalance;
                await this.progressBar.ProgressTo((double)percentage, 750, Easing.BounceOut);
            }
        }
    }
}
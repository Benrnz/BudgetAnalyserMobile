﻿using System;
using Xamarin.Forms;

namespace BAXMobile.Buckets
{
    public partial class BucketsListPage : ContentPage
    {
        public BucketsListPage()
        {
            InitializeComponent();
        }

        private BucketsListViewModel ViewModel => (BucketsListViewModel) BindingContext;

        private async void OnPageAppearing(object sender, EventArgs e)
        {
            await ViewModel.PageIsLoading();
        }
    }
}
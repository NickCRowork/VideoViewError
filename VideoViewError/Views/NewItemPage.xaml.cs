using System;
using System.Collections.Generic;
using System.ComponentModel;
using VideoViewError.Models;
using VideoViewError.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoViewError.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
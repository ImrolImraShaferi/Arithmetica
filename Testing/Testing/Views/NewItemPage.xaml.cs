using System;
using System.Collections.Generic;
using System.ComponentModel;
using Testing.Models;
using Testing.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Testing.Views
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
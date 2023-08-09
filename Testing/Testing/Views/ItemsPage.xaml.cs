using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Models;
using Testing.Services;
using Testing.ViewModels;
using Testing.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;
using System.Globalization;
using CsvHelper;

namespace Testing.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool result = await  DisplayAlert("Alert", "Are you sure you want to delete all?", "Yes", "No");

            if (result)
            {
                
                //// Get the viewmodel from the DataContext
                //ItemsViewModel vm = this.BindingContext as ItemsViewModel;

                ////Call command from viewmodel     
                //vm.ClearItemsCommand.Execute(vm);

                _viewModel.ClearItemsCommand.Execute(this);
            }
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            //string path = "";
            //string csv = "";
            //var newPath = System.IO.Path.Combine(await DependencyService.Get<IFileSystem>().GetExternalStorage(),"data1.txt");
            //using (var writer = File.CreateText(path))
            //{
            //    writer.Write(csv);
            //}
            // CSVhelper method
            

            //await DependencyService.Get<ISaveFileServicecs>().SaveFileAsync(csv);

        }
    }
}
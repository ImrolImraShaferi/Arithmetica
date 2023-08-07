using System.ComponentModel;
using Testing.ViewModels;
using Xamarin.Forms;

namespace Testing.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
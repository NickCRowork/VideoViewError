using System.ComponentModel;
using VideoViewError.ViewModels;
using Xamarin.Forms;

namespace VideoViewError.Views
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
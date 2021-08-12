using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoViewError.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoViewError.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayerPage : ContentPage
    {
        public VideoPlayerPage()
        {
            InitializeComponent();
            //Sets where the Binding keyword in the xaml looks
            this.BindingContext = new VideoPlayerViewModel();
        }
    }
}
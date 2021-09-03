using LibVLCSharp.Shared;
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((VideoPlayerViewModel)BindingContext).OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((VideoPlayerViewModel)BindingContext).OnDisappearing();
        }

        private void VideoView_MediaPlayerChanged(object sender, MediaPlayerChangedEventArgs e)
        {
            ((VideoPlayerViewModel)BindingContext).OnVideoViewInitialized();
        }
    }
}
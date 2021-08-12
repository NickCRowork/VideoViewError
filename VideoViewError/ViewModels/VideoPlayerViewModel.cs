using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VideoViewError.ViewModels
{
    class VideoPlayerViewModel : BaseViewModel
    {
        private string videosource;
        public string VideoSource
        {
            get => videosource;
            set => SetProperty(ref videosource, value);
        }

        public VideoPlayerViewModel()
        {
            VideoSource = "https://uwt-testing.s3.us-east-2.amazonaws.com/Coffee_test.mp4";
        }
    }
}

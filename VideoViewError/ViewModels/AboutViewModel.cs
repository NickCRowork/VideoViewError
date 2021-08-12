using System;
using System.Windows.Input;
using VideoViewError.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VideoViewError.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(NavigateToVideoPage);
        }

        public Command OpenWebCommand { get; }

        private async void NavigateToVideoPage()
        {
            await Shell.Current.Navigation.PushModalAsync(new VideoPlayerPage());
        }
    }
}
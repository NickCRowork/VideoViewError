using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VideoViewError.ViewModels
{
    class VideoPlayerViewModel : BaseViewModel
    {
        private bool endReached = false;
        public Command SendGoBack { get; }


        public VideoPlayerViewModel()
        {
            SendGoBack = new Command(NavigateBack);
            Initialize();
        }

        private LibVLC libVLC;
        public LibVLC LibVLC
        {
            get => libVLC;
            private set => Set(nameof(LibVLC), ref libVLC, value);
        }

        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        private bool IsLoaded { get; set; }
        private bool IsVideoViewInitialized { get; set; }
        private bool IsMediaPlayerReady { get; set; }

        private void Initialize()
        {
            Core.Initialize();
            LibVLC = new LibVLC(enableDebugLogs: true);

            libVLC.Log += LibVLC_Log;
            var media = new Media(LibVLC, new Uri("https://uwt-testing.s3.us-east-2.amazonaws.com/Coffee_test.mp4"));
            MediaConfiguration mediaConfiguration = new MediaConfiguration();
            mediaConfiguration.FileCaching = 18000;
            mediaConfiguration.NetworkCaching = 18000;
            media.AddOption(mediaConfiguration);

            MediaPlayer = new MediaPlayer(LibVLC)
            {
                Media = media,
                EnableHardwareDecoding = false
            };


            var success = MediaPlayer.SetRole(MediaPlayerRole.Video);
            MediaPlayer.EndReached += (sender, args) => ThreadPool.QueueUserWorkItem(_ => MediaPlayer_EndReached(sender, args));
            MediaPlayer.EncounteredError += (sender, args) => ThreadPool.QueueUserWorkItem(_ => MediaPlayer_EndReached(sender, args));
            MediaPlayer.ToggleFullscreen();
            var count = MediaPlayer.VoutCount;

            media.Dispose();
            IsMediaPlayerReady = true;
            Play();

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (endReached)
                {
                    Device.BeginInvokeOnMainThread(() => NavigateBack());
                    return false;
                }
                return true;
            });
        }

        private void LibVLC_Log(object sender, LogEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
        }

        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            endReached = true;
        }

        public void OnAppearing()
        {
            IsLoaded = true;
            Play();
        }

        internal void OnDisappearing()
        {
            if (MediaPlayer?.State == VLCState.Playing)
            {
                MediaPlayer?.Stop();
            }
            ThreadPool.QueueUserWorkItem(_ => MediaPlayer?.Dispose());
            ThreadPool.QueueUserWorkItem(_ => LibVLC.Dispose());
        }

        public void OnVideoViewInitialized()
        {
            IsVideoViewInitialized = true;
            Play();
        }

        private void Play()
        {
            if (IsLoaded && IsVideoViewInitialized && IsMediaPlayerReady)
            {
                MediaPlayer.Play();
            }
        }



        //Return to previous page
        private async void NavigateBack()
        {
            try
            {
                while (Application.Current.MainPage.Navigation.ModalStack.Count <= 0)
                {
                    Console.WriteLine("Trying to pop a page that hasn't been pushed yet.");//Spinning shouldn't be done, but can't think of any other way to do this
                }
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

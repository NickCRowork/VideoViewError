using LibVLCSharp.Shared;
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
            var media = new Media(LibVLC, new Uri("http://streams.videolan.org/streams/mkv/multiple_tracks.mkv"));

            MediaPlayer = new MediaPlayer(LibVLC)
            {
                Media = media,
                EnableHardwareDecoding = false
            };
            var success = MediaPlayer.SetRole(MediaPlayerRole.Video);
            MediaPlayer.EndReached += MediaPlayer_EndReached;
            MediaPlayer.EncounteredError += MediaPlayer_EndReached;
            var count = MediaPlayer.VoutCount;

            media.Dispose();
            IsMediaPlayerReady = true;
            Play();
        }

        private void LibVLC_Log(object sender, LogEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);
        }

        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            NavigateBack();
        }

        public void OnAppearing()
        {
            IsLoaded = true;
            Play();
        }

        internal void OnDisappearing()
        {
            //MediaPlayer.Dispose();
            LibVLC.Dispose();
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

        private async void NavigateBack()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}

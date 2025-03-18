using System;
using System.Linq;
using System.Threading.Tasks;
using SpotifyFlyout.Models;
using CommunityToolkit.Mvvm;
using Windows.Media.Control;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Microsoft.UI.Xaml;

namespace SpotifyFlyout.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private MediaData _mediaData = new MediaData();

        private GlobalSystemMediaTransportControlsSessionManager _sessionManager;
        private DispatcherTimer _timer;

        public MainViewModel()
        {
            InitialiseMediaSession();
            StartPolling();
        }

        private void StartPolling()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5) // Poll every 2 seconds
            };
            _timer.Tick += async (s, e) => await UpdateMediaProperties();
            _timer.Start();
        }


        private async void InitialiseMediaSession()  {
            try {

                Debug.WriteLine("InitializeMediaSession called.");
                _sessionManager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
                if (_sessionManager == null)
                {
                    Debug.WriteLine("Failed to initialize session manager.");
                    return;
                }
                Debug.WriteLine("Session manager initialized successfully.");
                _sessionManager.SessionsChanged += OnSessionChanged;
                await UpdateMediaProperties();

            } catch(System.Runtime.InteropServices.COMException ex) {
                Debug.WriteLine($"COMException in InitializeMediaSession: {ex.Message}");
            }
        }

        private async void OnSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, SessionsChangedEventArgs args)
        {
            try
            {
                Debug.WriteLine("Session changed detected!");
                await UpdateMediaProperties();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.WriteLine($"COMException in OnSessionChanged: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in OnSessionChanged: {ex.Message}");
            }

        }

        private async Task UpdateMediaProperties()
        {
            var sessions = _sessionManager?.GetSessions();
            if (sessions == null || sessions.Count == 0)
            {
                Debug.WriteLine("No active media sessions found.");
                return;
            }

            var session = _sessionManager.GetCurrentSession();
            if (session == null)
            {
                Debug.WriteLine("No active media sessions found.");
                return;
            }
            try
            {
                var mediaProperties = await session.TryGetMediaPropertiesAsync();
                if (mediaProperties == null)
                {
                    return;
                }
                MediaData.Title = mediaProperties.Title;
                MediaData.Artist = mediaProperties.Artist;
                MediaData.AlbumArtUrl = mediaProperties.Thumbnail?.ToString();
                Debug.WriteLine($"Updated media: {MediaData.Title} by {MediaData.Artist}");
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.WriteLine($"COMException in UpdateMediaProperties: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in UpdateMediaProperties: {ex.Message}");
            }
        }
    }
}

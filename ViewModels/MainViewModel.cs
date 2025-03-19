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
using SpotifyFlyout.Services;

namespace SpotifyFlyout.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private SpotifyTrack _spotifyTrack = new SpotifyTrack()
        {
            AlbumArtUrl = "https://upload.wikimedia.org/wikipedia/en/4/48/SleepTokenTMBTE.jpg",
            Artist = "Artist: Sleep Token",
            Title = "Title: Take Me Back To Eden"
        };

        public MainViewModel()
        {

        }
    }
}

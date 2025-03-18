using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyFlyout.Models
{
    public class MediaData
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtUrl { get; set; }
        public bool IsPlaying { get; set; }
    }
}

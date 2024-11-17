using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore_SoftUni.Data.Models

{
    public class BeatPlaylist
    {
        [Required]
        public Guid BeatId { get; set; }

        [ForeignKey(nameof(BeatId))]
        public virtual Beat Beat { get; set; } = null!;

        [Required]
        public Guid PlaylistId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        public virtual Playlist Playlist { get; set; } = null!;
    }
}
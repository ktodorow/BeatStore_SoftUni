using Xunit;
using Moq;
using BeatStore_SoftUni.Services.Data;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.ViewModels.BeatDtos;
using Microsoft.EntityFrameworkCore;

namespace BeatStore.Tests.Services
{
    public class BeatServiceTests
    {
        private readonly Mock<IRepository<Beat, Guid>> _mockBeatRepository;
        private readonly Mock<IRepository<BeatGenre, Guid>> _mockBeatGenreRepository;
        private readonly Mock<IRepository<Genre, Guid>> _mockGenreRepository;
        private readonly IBeatService _beatService;

        public BeatServiceTests()
        {
            _mockBeatRepository = new Mock<IRepository<Beat, Guid>>();
            _mockBeatGenreRepository = new Mock<IRepository<BeatGenre, Guid>>();
            _mockGenreRepository = new Mock<IRepository<Genre, Guid>>();

            _beatService = new BeatService(
                _mockBeatRepository.Object,
                _mockBeatGenreRepository.Object,
                _mockGenreRepository.Object
            );
        }

        [Fact]
        public async Task GetAllBeatsAsync_ReturnsAllActiveBeats()
        {
            // Arrange
            var beats = new List<Beat>
            {
                new Beat
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Beat 1",
                    IsActive = true,
                    CoverArtUrl = "https://example.com/cover1.png",
                    Price = 10.99m,
                    DateUploaded = DateTime.UtcNow,
                    AudioFileUrl = "https://example.com/audio1.mp3",
                    Artist = new ApplicationUser { UserName = "Artist1" },
                    BeatGenres = new List<BeatGenre>
                    {
                        new BeatGenre { Genre = new Genre { Name = "Hip-Hop" } }
                    }
                },
                new Beat
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Beat 2",
                    IsActive = true,
                    CoverArtUrl = "https://example.com/cover2.png",
                    Price = 15.99m,
                    DateUploaded = DateTime.UtcNow,
                    AudioFileUrl = "https://example.com/audio2.mp3",
                    Artist = new ApplicationUser { UserName = "Artist2" },
                    BeatGenres = new List<BeatGenre>
                    {
                        new BeatGenre { Genre = new Genre { Name = "Pop" } }
                    }
                }
            };

            _mockBeatRepository.Setup(repo => repo.GetAllAttached())
                .Returns(beats.AsQueryable());

            // Act
            var result = await _beatService.GetAllBeatsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var firstBeat = result.First();
            Assert.Equal("Test Beat 1", firstBeat.Title);
            Assert.Equal("Hip-Hop", firstBeat.Genre);
            Assert.Equal("Artist1", firstBeat.ArtistUsername);
            Assert.True(firstBeat.IsActive);
        }
    }
}

using Xunit;
using Moq;
using System.Linq.Expressions;
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

        [Fact]
        public async Task CreateBeatAsync_CreatesNewBeatWithGenres()
        {
            // Arrange
            var artistId = Guid.NewGuid();
            var genreIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var createBeatDto = new CreateBeatDTO
            {
                Title = "New Beat",
                Price = 19.99m,
                AudioFileUrl = "https://example.com/audio.mp3",
                CoverArtUrl = "https://example.com/cover.png",
                GenreIds = genreIds
            };

            // Act
            await _beatService.CreateBeatAsync(createBeatDto, artistId);

            // Assert
            _mockBeatRepository.Verify(repo => repo.AddAsync(It.Is<Beat>(b =>
                b.Title == "New Beat" &&
                b.Price == 19.99m &&
                b.AudioFileUrl == "https://example.com/audio.mp3" &&
                b.CoverArtUrl == "https://example.com/cover.png" &&
                b.ArtistId == artistId
            )), Times.Once);

            _mockBeatGenreRepository.Verify(repo => repo.AddAsync(It.Is<BeatGenre>(bg =>
                genreIds.Contains(bg.GenreId)
            )), Times.Exactly(2));
        }

        [Fact]
        public async Task SoftDeleteBeatAsync_SetsBeatAsInactiveAndUpdatesSuccessfully()
        {
            // Arrange
            var beatId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var beat = new Beat
            {
                Id = beatId,
                ArtistId = userId,
                IsActive = true // Initially active
            };

            _mockBeatRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Beat, bool>>>()))
                .ReturnsAsync(beat);

            _mockBeatRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Beat>()))
                .ReturnsAsync(true);

            // Act
            var result = await _beatService.SoftDeleteBeatAsync(beatId, userId);

            // Assert
            Assert.True(result); // Ensure the method returns true

            // Verify that IsActive is set to false
            Assert.False(beat.IsActive);

            // Verify that UpdateAsync is called with the modified beat
            _mockBeatRepository.Verify(repo => repo.UpdateAsync(It.Is<Beat>(b =>
                b.Id == beatId &&
                b.ArtistId == userId &&
                !b.IsActive
            )), Times.Once);
        }

        [Fact]
        public async Task SoftDeleteBeatAsync_ReturnsFalseIfBeatNotFound()
        {
            // Arrange
            var beatId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Mock the repository to return null (no beat found)
            _mockBeatRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Beat, bool>>>()))
                .ReturnsAsync((Beat?)null);

            // Act
            var result = await _beatService.SoftDeleteBeatAsync(beatId, userId);

            // Assert
            Assert.False(result); // Ensure the method returns false

            // Verify that UpdateAsync is never called
            _mockBeatRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Beat>()), Times.Never);
        }
    }
}

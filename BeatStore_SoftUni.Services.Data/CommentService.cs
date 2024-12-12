using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.CommentDTOs;

namespace BeatStore_SoftUni.Services.Data;

public class CommentService : ICommentService
{
    private readonly IRepository<Comment, Guid> _commentRepository;
    private readonly IRepository<Beat, Guid> _beatRepository;
    private readonly IRepository<ApplicationUser, Guid> _userRepository;

    public CommentService(
        IRepository<Comment, Guid> commentRepository,
        IRepository<Beat, Guid> beatRepository,
        IRepository<ApplicationUser, Guid> userRepository)
    {
        _commentRepository = commentRepository;
        _beatRepository = beatRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<CommentDTO>> GetCommentsForBeatAsync(Guid beatId, Guid? userId)
    {
        var comments = await _commentRepository.GetAllAsync();

        return comments
            .Where(c => c.BeatId == beatId && !c.IsDeleted)
            .Select(c => new CommentDTO
            {
                Id = c.Id,
                Username = c.User.UserName,
                ProfilePicture = c.User.ProfilePicture,
                Content = c.Content,
                DatePosted = c.DatePosted,
                EditedOn = c.EditedOn,
                IsOwner = c.UserId == userId,
                IsBeatOwner = c.Beat.ArtistId == userId
            });
    }

    public async Task AddCommentAsync(CreateCommentDTO createCommentDTO, Guid userId)
    {
        var beat = await _beatRepository.GetByIdAsync(createCommentDTO.BeatId);
        if (beat == null)
        {
            throw new InvalidOperationException("Beat not found.");
        }

        var newComment = new Comment
        {
            Id = Guid.NewGuid(),
            BeatId = createCommentDTO.BeatId,
            UserId = userId,
            Content = createCommentDTO.Content,
            DatePosted = DateTime.UtcNow,
            IsDeleted = false
        };

        await _commentRepository.AddAsync(newComment);
    }

    public async Task EditCommentAsync(EditCommentDTO editCommentDTO, Guid userId)
    {
        var comment = await _commentRepository.GetByIdAsync(editCommentDTO.Id);
        if (comment == null || comment.IsDeleted)
        {
            throw new InvalidOperationException("Comment not found.");
        }

        if (comment.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this comment.");
        }

        comment.Content = editCommentDTO.Content;
        comment.EditedOn = DateTime.UtcNow;

        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid userId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null || comment.IsDeleted)
        {
            throw new InvalidOperationException("Comment not found.");
        }

        var beat = await _beatRepository.GetByIdAsync(comment.BeatId);
        if (comment.UserId != userId && beat.ArtistId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this comment.");
        }

        comment.IsDeleted = true;
        await _commentRepository.UpdateAsync(comment);
    }
}
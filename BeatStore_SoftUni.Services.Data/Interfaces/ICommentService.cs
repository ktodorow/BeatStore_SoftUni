using BeatStore_SoftUni.ViewModels.CommentDTOs;

namespace BeatStore_SoftUni.Services.Data.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDTO>> GetCommentsForBeatAsync(Guid beatId, Guid? userId);
    Task AddCommentAsync(CreateCommentDTO createCommentDTO, Guid userId);
    Task EditCommentAsync(EditCommentDTO editCommentDTO, Guid userId);
    Task DeleteCommentAsync(Guid commentId, Guid userId);
}
using Humanizer;

namespace BeatStore_SoftUni.Common;

public static class EntityValidationConstants
{
    public const int ProfilePicturePathMaxLength = 260;
    public const int BeatTitleMinLength = 3;
    public const int BeatTitleMaxLength = 100;
    public const int BeatGenreMaxLength = 50;
    public const string BeatPriceMinValue = "0.99";
    public const string BeatPriceMaxValue = "999.99";
    public const int BeatDurationMinValue = 1;
    public const int BeatDurationMaxValue = 300; // Seconds
    public const int BeatAudioFileUrlMaxLength = 2048;
    public const int PlaylistNameMinLength = 1;
    public const int PlaylistNameMaxLength = 80;
    public const int PlaylistDescriptionMinLength = 5;
    public const int PlaylistDescriptionMaxLength = 100;
    public const int CommentContentMinLength = 1;
    public const int CommentContentMaxLength = 200;

}

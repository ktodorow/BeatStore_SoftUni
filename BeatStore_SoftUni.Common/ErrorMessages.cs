using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeatStore_SoftUni.Common
{
    public static class ErrorMessages
    {
        public const string ErrBeatNoLongerAvailable = "This beat is no longer available.";
        public const string ErrUnableToRetrieveInformation = "Unable to retrieve artist information. Please log in again.";
        public const string ErrUnableToCompletePurchase = "Unable to complete the purchase. Please check your balance or the items in your cart.";
        public const string ErrAlreadyPurchasedBeatCart = "You have already purchased this beat. It cannot be added to the cart.";
        public const string ErrItemAlreadyAddedInCart = "This item is already in your cart.";
        public const string ErrPurchase = "You either don't have enough balance or have already purchased this beat.";
        public const string ErrMigration = "Error migrating database: ";

    }
}

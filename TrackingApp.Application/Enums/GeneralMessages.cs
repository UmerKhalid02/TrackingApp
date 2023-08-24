namespace TrackingApp.Application.Enums
{
    public sealed class GeneralMessages
    {
        private GeneralMessages() { }
        public const string RecordAdded = "Record has been added successfully";
        public const string RecordNotAdded = "Record not added successfully";
        public const string RecordUpdated = "Record has been updated successfully";
        public const string RecordAddedUpdated = "Record has been added/updated successfully";
        public const string RecordNotUpdated = "Record not updated successfully";
        public const string RecordDeleted = "Record has been deleted successfully";
        public const string RecordNotDeleted = "Record not deleted successfully";
        public const string RecordFetched = "Record has been fetched successfully";
        public const string RecordNotFetched = "Record not fetched successfully";
        public const string RecordNotFound = "Record not found";
        
        
        public const string UserNotFound = "User not found";
        public const string UserWithUsernameExists = "User with this username already exists";
        public const string UserWithEmailExists = "User with this email already exists";
        public const string InvalidUserName = "User with this username does not exist";

        public const string InvalidCredentials = "Invalid Credentials";
        public const string TokenIssue = "Something went wrong with token...";
        public const string UserLoggedInSuccessMessage = "Logged in Successfully.";
        public const string UserLoggedInFailMessage = "Logged in fail.";
        public const string UserLoggedInFailPassword = "Incorrect password.";
        public const string UserLogoutSuccessMessage = "Logged out Successfully.";
        public const string UserLogoutFailMessage = "Logged out fail.";
        public const string InvalidToken = "Invalid access token";

        public const string RegisterUserFail = "Something went wrong while registering a new user";
        public const string RegisterUserSuccess = "User has been registered successfully";

        public const string InvalidOrderId = "Order ID is invalid";
        public const string InvalidOrderStatus = "Order status is invalid";
        public const string NoOrderPlaced = "No order has been placed yet";
        public const string OrderNotFound = "Order not found";
    }
}

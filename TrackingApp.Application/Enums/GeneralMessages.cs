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

        public const string InvalidCredentials = "Invalid Credentials";
        public const string TokenIssue = "Something went wrong with token...";
        public const string UserLoggedInSuccessMessage = "Logged in Successfully.";
        public const string UserLoggedInFailMessage = "Logged in fail.";
        public const string UserLoggedInFailPassword = "Incorrect password.";
    }
}

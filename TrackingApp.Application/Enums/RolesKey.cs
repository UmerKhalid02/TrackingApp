namespace TrackingApp.Application.Enums
{
    public static class RolesKey
    {
        public static readonly Guid AdminRoleId = Guid.Parse("35DC76B5-8DE7-4EB3-A29C-9A05686A6F89");
        public static readonly Guid UserRoleId = Guid.Parse("2BD8F739-13C4-46E2-B0CC-5888851F373A");

        public const string AD = "AD";
        public const string US = "US";

        public const string Admin = "Admin";
        public const string User = "User";
    }
}

namespace LatinJobs.Api.Shared
{
    public static class Constants
    {
        public const string ConnectionStringName = "DefaultConnection";
        public const string DefaultUserPassword = "12345678";

        public static class Permissions
        {
            public const string Write = "write";
            public const string Read = "read";
            public const string Edit = "edit";
            public const string Delete = "delete";
        }

        public static class Roles
        {
            public const string Admin = "admin";
            public const string User = "user";
            public const string Guest = "guest";
        }
    }
}

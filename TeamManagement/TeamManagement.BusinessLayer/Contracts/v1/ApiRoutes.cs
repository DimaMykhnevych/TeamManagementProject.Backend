namespace TeamManagement.Contracts.v1
{
    public static class ApiRoutes
    {
        private const string Version = "v1";

        public static class Identity
        {
            public const string Base = "identity";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string Register = BaseWithVersion + "/register";
            public const string Login = BaseWithVersion + "/login";
            public const string Callback = BaseWithVersion + "/callback";
            public const string AlterCookieCallback = BaseWithVersion + "/alterCookieCallback";
            public const string MakeAdmin = BaseWithVersion + "/make-admin/{id}";
            public const string GetUsers = BaseWithVersion + "/users";
            public const string GetUser = BaseWithVersion + "/user";
        }

        public static class Auth
        {
            public const string Base = "auth";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string Login = BaseWithVersion + "/login";
            public const string UserInfo = BaseWithVersion + "/user-info";
        }

        public static class Articles
        {
            public const string Base = "articles";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string GetById = BaseWithVersion + "/{id}";
            public const string Update = BaseWithVersion + "/{id}";
            public const string Delete = BaseWithVersion + "/{id}";
            public const string GetForCurrentUser = BaseWithVersion + "/for-user";
        }

        public static class HowToArticles
        {
            public const string Base = "how-to-articles";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string GetById = BaseWithVersion + "/{id}";
            public const string Delete = BaseWithVersion + "/{id}";
            public const string Update = BaseWithVersion + "/{id}";
        }

        public static class Tags
        {
            public const string Base = "tags";
            public const string BaseWithVersion = Version + "/" + Base;
        }

        public static class Polls
        {
            public const string Base = "polls";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string MakeVote = BaseWithVersion + "/make-vote";
        }
        public static class Company
        {
            public const string Base = "companies";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string GetById = BaseWithVersion + "/{id}";
        }

        public static class SubscriptionPlan
        {
            public const string Base = "subscription-plans";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string GetById = BaseWithVersion + "/{id}";
        }

        public static class Subscription
        {
            public const string Base = "subscriptions";
            public const string BaseWithVersion = Version + "/" + Base;
            public const string Update = BaseWithVersion + "/{id}";
        }

        public static class Employee
        {
            public const string Base = "employee";
            public const string BaseWithVersion = Version + "/" + Base;
        }

        public static class Project
        {
            public const string Base = "project";
            public const string BaseWithVersion = Version + "/" + Base;
        }

        public static class Team
        {
            public const string Base = "team";
            public const string BaseWithVersion = Version + "/" + Base;
        }

        public static class User
        {
            public const string Base = "user";
            public const string BaseWithVersion = Version + "/" + Base;
        }

    }
}

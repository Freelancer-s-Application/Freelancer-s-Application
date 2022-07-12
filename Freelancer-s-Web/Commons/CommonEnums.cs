namespace Freelancer_s_Web.Commons
{
    public class CommonEnums
    {
        public class ROLE
        {
            public const string USER = "USER";
            public const string ADMINISTRATOR = "ADMIN";
        }
        public class POST_STATUS
        {
            public const int PUBLIC = 1;
            public const int REMOVE = 0;
            public const int PRIVATE = 2;
        }

        public class APPLICATION_FORM_STATUS
        {
            public const int PENDING = 1;
            public const int APPROVED = 2;
            public const int REMOVED = 0;
            public const int CANCELED = 3;
        }
        
        public class POST_CONTENT_TYPE
        {
            public const int IMAGE = 1;
            public const int VIDEO = 2;
        }
    }
}
namespace Freelancer_s_Web.Commons
{
    public static class CommonEnums
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
        
        public static class POST_CONTENT_TYPE
        {
            public static string[] PDF = { "application/pdf" };
            public static string[] VIDEO = { "video/mp4", "video/quicktime", "video/x-ms-wmv", "video/ogg" };
            public static string[] IMAGE = { "image/jpeg", "image/png" };
            public static string[] AUDIO = { "audio/mp3", "audio/mp4", "audio/mpeg", "audio/mpeg3", "audio/mpg", "audio/wav", "audio/x-mpeg-3" };
        }
    }
}
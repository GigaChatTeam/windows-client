namespace GigaChat.patterns
{
    internal class Intentions
    {
        public static string[] ADMIN_CHANNELS_CREATE = new string[] { "ADMIN", "CHANNELS", "CREATE" };
        public static string[] ADMIN_CHANNELS_DELETE = new string[] { "ADMIN", "CHANNELS", "DELETE" };
        public static string[] ADMIN_CHANNELS_USERS_ADD = new string[] { "ADMIN", "CHANNELS", "USERS", "ADD" };
        public static string[] ADMIN_CHANNELS_USERS_REMOVE = new string[] { "ADMIN", "CHANNELS", "USERS", "REMOVE" };

        public static string[] ADMIN_CHANNELS_SETTINGS_EXTERNAL_CHANGE_TITLE = new string[] { "ADMIN", "CHANNELS", "SETTINGS", "EXTERNAL", "CHANGE", "TITLE" };
        public static string[] ADMIN_CHANNELS_SETTINGS_EXTERNAL_CHANGE_DESCRIPTION = new string[] { "ADMIN", "CHANNELS", "SETTINGS", "EXTERNAL", "CHANGE", "DESCRIPTION" };

        public static string[] USER_CHANNELS_JOIN = new string[] { "USER", "CHANNELS", "JOIN" };
        public static string[] USER_CHANNELS_LEAVE = new string[] { "USER", "CHANNELS", "LEAVE" };

        public static string[] USER_CHANNELS_MESSAGES_POST_NEW = new string[] { "USER", "CHANNELS", "MESSAGES", "POST", "NEW" };
        public static string[] USER_CHANNELS_MESSAGES_POST_FORWARD_MESSAGE = new string[] { "USER", "CHANNELS", "MESSAGES", "POST", "FORWARD", "MESSAGE" };
        public static string[] USER_CHANNELS_MESSAGES_POST_FORWARD_POST = new string[] { "USER", "CHANNELS", "MESSAGES", "POST", "FORWARD", "POST" };
        public static string[] USER_CHANNELS_MESSAGES_EDIT = new string[] { "USER", "CHANNELS", "MESSAGES", "EDIT" };
        public static string[] USER_CHANNELS_MESSAGES_DELETE = new string[] { "USER", "CHANNELS", "MESSAGES", "DELETE" };

        public static string[] USER_CHANNELS_MESSAGES_REACTIONS_ADD = new string[] { "USER", "CHANNELS", "MESSAGES", "REACTIONS", "ADD" };
        public static string[] USER_CHANNELS_MESSAGES_REACTIONS_REMOVE = new string[] { "USER", "CHANNELS", "MESSAGES", "REACTIONS", "REMOVE" };

        public static string[] SYSTEM_CHANNELS_LISTEN_ADD = new string[] { "SYSTEM", "CHANNELS", "LISTEN", "ADD" };
        public static string[] SYSTEM_CHANNELS_LISTEN_REMOVE = new string[] { "SYSTEM", "CHANNELS", "LISTEN", "REMOVE" };

        public static string[] SYSTEM_TTOKENS_GENERATE = new string[] { "SYSTEM", "TTOKENS", "GENERATE" };
    }
}

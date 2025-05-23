namespace Frutti.Shared.Common;

public static class Errors
{
    public static readonly Error NONE = new(nameof(NONE), "");

    public static class General
    {
        public static readonly Error INTERNAL_SERVER_ERROR = new(nameof(INTERNAL_SERVER_ERROR), "An unexpected internal server error occurred.");
    }

    public static class Network
    {
        public static readonly Error INVALID_PACKET_FORMAT = new(nameof(INVALID_PACKET_FORMAT), "The received packet has an invalid format.");
        public static readonly Error UNKNOWN_PACKET_TYPE = new(nameof(UNKNOWN_PACKET_TYPE), "The received packet has an invalid type.");
    }

    public static class User
    {
        public static readonly Error USER_NOT_FOUND = new(nameof(USER_NOT_FOUND), "User not found.");

        public static readonly Error INVALID_CREDENTIALS = new(nameof(INVALID_CREDENTIALS), "Invalid username or password.");

        public static readonly Error USERNAME_ALREADY_EXISTS = new(nameof(USERNAME_ALREADY_EXISTS), "Username already exists.");

        public static readonly Error INVALID_ACCESS_TOKEN = new(nameof(INVALID_ACCESS_TOKEN), "Invalid access token.");

        public static readonly Error INVALID_USER_ID_TOKEN = new(nameof(INVALID_USER_ID_TOKEN), "Invalid user ID in access token.");

        public static readonly Error INVALID_REFRESH_TOKEN = new(nameof(INVALID_REFRESH_TOKEN), "Invalid refresh token.");
    }
}

/*
 * Arquivo: LanguageEN.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
namespace PBLauncher.Localize
{
    public class LanguageEN : IStrings
    {
        public string LOADING { get; set; } = "Loading...";
        public string EXIT_ASK { get; set; } = "Are you sure you want to quit?";
        public string CHECK_IMPORTANT_FILES { get; set; } = "Checking important files...";
        public string GAME_RUNNING { get; set; } = "There is already a game window open.";
        public string LAUNCHER_RUNNING { get; set; } = "There is already a Launcher window open.";
        public string CONNECTING_TO_SERVER { get; set; } = "Connecting to the server...";
        public string SERVER_CONNECTION_UNKNOWN { get; set; } = "Unable to connect to server.\nLauncher will be terminated.";
        public string CHECK_LAUNCHER_UPDATE { get; set; } = "Checking for Updates...";
        public string LAUNCHER_UPDATE_ERROR { get; set; } = "An error occurred while downloading launcher update!";
        public string SERVER_UNKNOWN_PACKAGE { get; set; } = "Something wrong happened...\nContact support:\nprivatepointblank.com/suporte";
        public string AUTH_LOGIN { get; set; } = "Logging in, please wait...";
        public string AUTH_ACC_NO_STAFF { get; set; } = "Login available to staff members only.";
        public string AUTH_ACC_BANNED { get; set; } = "Your launcher is permanently banned.";
        public string AUTH_ACC_CREAT_ERROR { get; set; } = "We were unable to create your user on the server,\nyou are creating accounts too fast!";
        public string AUTH_ACC_NULL { get; set; } = "Unable to find your username on server.\nContact support for more information!\nprivatepointblank.com/suporte";
        public string CHECK_SERVER_STATUS { get; set; } = "Checking server status...";
        public string SERVER_MAINTENANCE { get; set; } = "The game is currently under maintenance.\nTry again later!";
        public string DOWNLOAD_USERLIST { get; set; } = "Downloading UserFileList: ";
        public string DOWNLOAD_LAUNCHER_UPDATE { get; set; } = "Downloading update: ";
        public string DOWNLOAD_USERLIST_ERROR { get; set; } = "An error occurred while downloading the UserFileList.";
        public string DELET_TRASH { get; set; } = "Deleting Useless Files...";
        public string EXTRACT_FILE_UPDATE { get; set; } = "Extracting and installing the update...";
        public string EXTRACT_FILE { get; set; } = "Extracting files...";
        public string FILE_CHECK_GET { get; set; } = "Updating GAME SHIELD, please wait...";
        public string ACCESS_DENIED { get; set; } = "Access denied";
        public string BAN_TIME_LEFT { get; set; } = "Time left: {0}Days {1}Hours {2}Minutes {3}Seconds";
        public string GAME_NOT_AVAILABLE { get; set; } = "The game is not available at the moment.";
        public string UPDATE_REC { get; set; } = "Update your game version.";
        public string GAME_IS_UPDATE { get; set; } = "Your game is up to date. You can play now.";
        public string FILE { get; set; } = "File";
        public string TOTAL { get; set; } = "Total";
        public string CHECK_REC { get; set; } = "Invalid files detected. Verify Customer Integrity!";
        public string LARGER_VERSION { get; set; } = "Client version larger than server version.\nPerform updates again!";
        public string DOWNLOAD_FILE { get; set; } = "Downloading the files...";
        public string DOWNLOAD_FILE_ERROR { get; set; } = "An error occurred while downloading the file [{0}].\nTry again later!";
        public string CHECKING_FILES { get; set; } = "Checking the game files...";
        public string CHECK_ERROR { get; set; } = "An error occurred while trying to verify client files!";
        public string CONFIG_ERROR { get; set; } = "Could not find\nthe game configuration file.";
        public string CONNECTION_LOST { get; set; } = "Server connection broken.";
        public string LOADING_PRE_START { get; set; } = "Loading GAME SHIELD...";
        public string GAME_NOT_FOUND { get; set; } = "Unable to find PointBlank.exe";
        public string START_ERROR { get; set; } = "Could not start the game.\nThe server has not validated your passkey.";
        public string USERLIST_ERROR { get; set; } = "An error occurred with the UserFileList.dat file.";
        public string GET_UPDATE_INFO { get; set; } = "Getting update information...";
        public string GET_UPDATE_ERROR { get; set; } = "It was not possible to get an update list from the server.";
        public string TOP_INFO { get; set; } = "Launcher: {0}\nPoints: {1}";
        public string CORE_NOTFOUND { get; set; } = "Launcher_core.dll not found.";
    }
}

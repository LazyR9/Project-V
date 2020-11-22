using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    public UserManager userManager;
    public ActivityManager activityManager;
    public LobbyManager lobbyManager;
    Discord.Activity activity;
    string state;
    string details;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager levelData = null;

        if (GameObject.Find("LevelManager"))
        {
            levelData = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }

        string discordAppId = Utils.GetSecret("DISCORD_APP_ID");
        discord = new Discord.Discord(long.Parse(discordAppId), (System.UInt64)Discord.CreateFlags.Default);
        userManager = discord.GetUserManager();
        activityManager = discord.GetActivityManager();
        //lobbyManager = discord.GetLobbyManager();
        
        discord.SetLogHook(Discord.LogLevel.Debug, LogProblemsFunction);

        userManager.OnCurrentUserUpdate += () =>
        {
            var currentUser = userManager.GetCurrentUser();
            Debug.Log(currentUser.Username);
            Debug.Log(currentUser.Discriminator);
            Debug.Log(currentUser.Id);
        };

        if (levelData)
        {

            if (levelData.isMultiplayer)
            {
                state = "In a multiplayer game with (User)"; // Fetch the User here somehow, no multiplayer system is in place yet anyway
            
            }
            else
            {
                //state = "Playing a test build";
                state = "In a singleplayer game";
            }
            details = "Currently on: " + levelData.levelName;
            
            activity = new Discord.Activity
            {
                State = state,
                Details = details
            };
            
            UpdateActivity(activity);
        }
        else
        {
            GameObject.Find("DiscordData").GetComponent<DiscordData>().RefeshActivity();
        }


        /*
        var txn = lobbyManager.GetLobbyCreateTransaction();

        txn.SetCapacity(2);
        txn.SetType(Discord.LobbyType.Private);
        txn.SetMetadata("info", "testing the lobby system");

        lobbyManager.CreateLobby(txn, (Result result, ref Lobby lobby) =>
        {
            Debug.Log(string.Format("lobby {0} created with secret {1}", lobby.Id, lobby.Secret));
            
            lobbyManager.ConnectNetwork(lobby.Id);
            lobbyManager.OpenNetworkChannel(lobby.Id, 0, true);

            //lobbyManager.SendNetworkMessage
        });
        */

    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }

    public void LogProblemsFunction(Discord.LogLevel level, string message)
    {
        Debug.Log(string.Format("Discord:{0} - {1}", level.ToString(), message.ToString()));
    }

    // Called before the game closes
    void OnApplicationQuit()
    {
        ClearActivity();
        discord.Dispose();
        Debug.Log("Game is closing!");
    }

    public void UpdateActivity(Discord.Activity activity)
    {
        activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Succeeded to update activity!");
            }
            else
            {
                Debug.LogError("Failed to update activity: " + result);
            }
        });
    }

    public void ClearActivity()
    {
        activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Succeeded to clear activity!");
            }
            else
            {
                Debug.LogError("Failed to clear activity: " + result);
                ClearActivity();
            }
        });
    }

}

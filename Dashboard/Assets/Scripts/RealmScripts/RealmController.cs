using UnityEngine;
using Realms;
using System.Threading.Tasks;
using Realms.Sync;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine.UI;

public class RealmController : MonoBehaviour
{
    private VisualTreeAsset authenticationUXMLVisualTree;
    private string uiToolKitPath = "Assets/Scripts/UI ToolKit/";
    private static Realm realm;
    private static int runTime; // total amount of time you've been playing during this playthrough/run (losing/winning resets runtime)

    private static Player currentPlayer; // the Player object for the current playthrough
    public static Stat currentStat; // the Stat object for the current playthrough

    private static App realmApp = App.Create(Constants.Realm.AppId); // (Part 2 Sync): realmApp represents the MongoDB Realm backend application
    public static User syncUser; // (Part 2 Sync): syncUser represents the realmApp's currently logged in user

    #region PublicMethods
    // LogOut() logs out and reloads the scene
    public static void LogOut()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // LogOutBackend() is an asynchronous method that logs out 
    // the current MongoDB Realm User
    public static async void LogOutBackend()
    {
        await syncUser.LogOutAsync();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // RestartGame() creates a new plathrough Stat object and shares this new
    // Stat object with the ScoreCardManager to update in the UI and listen for
    // changes to it
    public static void RestartGame()
    {
        var stat = new Stat();
        stat.StatOwner = currentPlayer;
        realm.Write(() =>
        {
            currentStat = realm.Add(stat);
            currentPlayer.Stats.Add(currentStat);
        });

        // ScoreCardManager.SetCurrentStat(currentStat); // call `SetCurrentStat()` to set the current stat in the UI using ScoreCardManager
        // ScoreCardManager.WatchForChangesToCurrentStats(); // call `WatchForChangesToCurrentStats()` to register a listener on the new score in the ScoreCardManager

        StartGame(); // start the game by resetting the timer and officially starting a new run/playthrough
    }

    // SetLoggedInUser() is an asynchronous method that logs in as a Realms.Sync.User, creates a new Stat object for the current playthrough
    // and returns the Player object that corresponds to the logged in Realms.Sync.User
    // SetLoggedInUser() takes a userInput and passInput, representing a username/password, as a parameter
    public static async Task<Player> SetLoggedInUser(string userInput, string passInput)
    {
        syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        if (syncUser != null)
        {
            realm = await GetRealm(syncUser);
            currentPlayer = realm.Find<Player>(syncUser.Id);
            if (currentPlayer != null)
            {
                var stat = new Stat();
                stat.StatOwner = currentPlayer;
                realm.Write(() =>
                {
                    currentStat = realm.Add(stat);
                    currentPlayer.Stats.Add(currentStat);
                });
                StartGame();
            }
            else
            {
                Debug.Log("This player exists a MongoDB Realm User but not as a Realm Object, please delete the MongoDB Realm User and create one using the Game rather than MongoDB Atlas or Realm Studio");
            }
        }
        return currentPlayer;
    }

    // OnPressRegister() is an asynchronous method that registers a user,
    // creates a new Player and Stat object OnPressRegister takes a userInput
    // and passInput, representing a username/password, as a parameter
    public static async Task<Player> OnPressRegister(string userInput, string passInput)
    {
        await realmApp.EmailPasswordAuth.RegisterUserAsync(userInput, passInput);
        syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        realm = await GetRealm(syncUser);

        var player = new Player();
        player.Id = syncUser.Id;
        player.Name = userInput;
        var stat = new Stat();
        stat.StatOwner = player;
        realm.Write(() =>
        {
            currentPlayer = realm.Add(player);
            currentStat = realm.Add(stat);
            currentPlayer.Stats.Add(currentStat);
        });
        StartGame();
        return currentPlayer;
    }


    #endregion

    #region PrivateMethods
    private void GenerateUIObjects(GameObject canvasGameObject, string uiObjectName)
    {
        var panelSettings = EditorGUIUtility.Load(uiToolKitPath + "UIPanelSettings.asset");
        // create an empty GameObject
        var gameObject = new GameObject();
        // create a UI object to add to the GameObject
        var uiDocument = gameObject.AddComponent<UIDocument>();
        // attach existing panel settings to the UI Document
        uiDocument.panelSettings = (PanelSettings)panelSettings;

        // Attach Manager Scripts to interact with UI Documents and attach the UI Document's VisualTreeAsset 
        switch (uiObjectName)
        {
            case "Authentication":
                gameObject.AddComponent<AuthenticationManager>();
                uiDocument.name = "Authentication";
                uiDocument.visualTreeAsset = authenticationUXMLVisualTree;
                break;
        }

        // Attach the UI Document as a child of the Canvas
        uiDocument.transform.parent = canvasGameObject.transform;
    }

    // GetRealm() is an asynchronous method that returns a synced realm
    // GetRealm() takes a logged in Realms.Sync.User as a parameter
    private static async Task<Realm> GetRealm(User loggedInUser)
    {
        var syncConfiguration = new SyncConfiguration("UnityTutorialPartition", loggedInUser);
        return await Realm.GetInstanceAsync(syncConfiguration);
    }


    // StartGame() records how long the player has been playing during the
    // current playthrough (i.e since logging in or since last losing or
    // winning)
    private static void StartGame()
    {
        // execute a timer every 10 second
        var myTimer = new System.Timers.Timer(10000);
        myTimer.Enabled = true;
        myTimer.Elapsed += (sender, e) => runTime += 10; // increment runTime (runTime will be used to calculate bonus points once the player wins the game)
    }

    #endregion

    #region UnityLifecycleMethods
    private void Start()
    {
        // Load UXML Assets
        authenticationUXMLVisualTree = EditorGUIUtility.Load(uiToolKitPath + "Authentication.uxml") as VisualTreeAsset;

        // Create canvas as a container to hold UIDocuments
        var canvasGameObject = new GameObject();
        canvasGameObject.name = "Canvas";
        var canvas = canvasGameObject.AddComponent<Canvas>();

        // Configure canvas properties
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();

        // Generate Authentication, Leaderboard, and Scorecard UI Objects
        GenerateUIObjects(canvasGameObject, "Authentication");
    }

    #endregion
}
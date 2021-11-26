using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealmController : MonoBehaviour //was not static but was :Monobehaviour
{
    
    private static App realmApp = App.Create(Constants.Realm.AppId); // (Part 2 Sync): realmApp represents the MongoDB Realm backend application
    private static Realm realm;
    public static User syncUser; // (Part 2 Sync): syncUser represents the realmApp's currently logged in user
    
    public static async Task<User> SetLoggedInUser(string userInput, string passInput)
    {
        
        syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        if (syncUser != null) {
            realm = await GetRealm(syncUser);
        }
        return syncUser;
    }
    
    public static async Task<User> OnPressRegister(string userInput, string passInput)
    {
        await realmApp.EmailPasswordAuth.RegisterUserAsync(userInput, passInput);
        syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
        realm = await GetRealm(syncUser);
        
        return syncUser;
    }
    
    public static async void LogOutBackend()
    {
        await syncUser.LogOutAsync();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private static async Task<Realm> GetRealm(User loggedInUser)
    {
        var syncConfiguration = new SyncConfiguration("UnityTutorialPartition", loggedInUser);
        return await Realm.GetInstanceAsync(syncConfiguration);
    }
}

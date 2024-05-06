
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;

public static class DatabaseHandler
{
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";
    
    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);
    public delegate void GetPokemonsCallback(Dictionary<string, Pokemon> pokes);
    public delegate void GetHabitsCallback(Dictionary<string, Habit> pokes);
    public delegate void GetEggsCallback(Dictionary<string, Egg> pokes);
    public delegate void GetItemsCallback(Dictionary<int,int> pokes);
    public delegate void GetFoodsCallback(Dictionary<int, int> pokes);
    public delegate void GetUserIdsCallback(Dictionary<int, string> pokes);
    public delegate void DeleteHabitCallback();


    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    /// <param name="idToken"> Token which authenticates the request </param>
    public static void PostUser(User user, string userId,string idToken, PostUserCallback callback )
    {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json?auth={idToken}", user).Then(response => { callback(); });
    }

    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    /// <param name="idToken"> Token which authenticates the request </param>
    public static void GetUser(string userId,  string idToken,GetUserCallback callback )
    {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json?auth={idToken}").Then(user => { callback(user); });
    }

    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json?auth={AuthHandler.Instance.idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }

    public static void PostPokemon(Pokemon poke)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<Pokemon>($"{databaseURL}pokemonlist/{userId}/{poke.uniqueId}.json?auth={idToken}", poke).Then(response => {  });
    }

    
    public static void GetPokemons(GetPokemonsCallback callback)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}pokemonlist/{userId}.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Pokemon>), ref deserialized);

            var pokes = deserialized as Dictionary<string, Pokemon>;
            callback(pokes);
        });
    }

    public static void PostHabit(Habit h)
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<Habit>($"{databaseURL}habits/{userId}/{h.title}.json?auth={idToken}", h).Then(response => {  
            //curUser.numberOfHabits+=1;
        }).Then(()=>{
            GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
        });
        
    }

    public static void DeleteHabit(Habit h, DeleteHabitCallback callback){
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Delete($"{databaseURL}habits/{userId}/{h.title}.json?auth={idToken}").Then(response => {  
            callback();
        });
    }

    public static void DeleteEgg(Egg e, DeleteHabitCallback callback){
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Delete($"{databaseURL}eggs/{userId}/{e.uniqueId}.json?auth={idToken}").Then(response => {  
            callback();
        });
    }

    

    public static void GetHabits(GetHabitsCallback callback)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}habits/{userId}.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string,Habit>), ref deserialized);

            var habits = deserialized as Dictionary<string, Habit>;

            callback(habits);
        });
    }

    public static void PostEgg(Egg e)
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<Egg>($"{databaseURL}eggs/{userId}/{e.uniqueId}.json?auth={idToken}", e).Then(response => {  
            //curUser.numberOfHabits+=1;
        });
        
    }

    public static void GetEggs(GetEggsCallback callback)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}eggs/{userId}.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string,Egg>), ref deserialized);

            var eggs = deserialized as Dictionary<string, Egg>;

            callback(eggs);
        });
    }

    

    public static void PostItem(int id,int qty)
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<int>($"{databaseURL}items/{userId}/{id}.json?auth={idToken}", qty).Then(response => {  
            //curUser.numberOfHabits+=1;
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });
        
    }

    public static void GetItems(GetItemsCallback callback)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}items/{userId}.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<int,int>), ref deserialized);

            var items = deserialized as Dictionary<int, int>;

            callback(items);
        });
    }

    

    public static void GetFoods(GetFoodsCallback callback)
    {
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}foods/{userId}.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<int,int>), ref deserialized);

            var foods = deserialized as Dictionary<int, int>;

            callback(foods);
        });
    }

    public static void PostTradePokemon(TradePokemon tp)
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<TradePokemon>($"{databaseURL}gts/{tp.userId}/{tp.uniqueId}.json?auth={idToken}", tp).Then(response => {  
            //curUser.numberOfHabits+=1;
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });
        
    }


    
    
    
    



}
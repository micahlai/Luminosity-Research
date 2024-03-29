using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using static activityMonitor;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager manager;

    public TextMeshProUGUI messageText;
    public InputField nameInput;

    public GameObject loginObjects;
    public GameObject loggedInObjects;
    public TextMeshProUGUI loggedInText;

    bool hasDisplayName;

    // Start is called before the first frame update

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Login();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ContinueButton()
    {

        FindObjectOfType<activityMonitor>().timerStart = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = System.Guid.NewGuid().ToString(),
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
            
            
        };

        PlayFabClientAPI.LoginWithCustomID(request, onSuccess, onError);
    }
    public void UpdateDisplayName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        };
    PlayFabClientAPI.UpdateUserTitleDisplayName(request, onTitleSuccess, onError);
    }
    void onTitleSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result);
        loginObjects.SetActive(false);
        loggedInObjects.SetActive(true);
        loggedInText.text = "Welcome " + result.DisplayName.ToString();
    }
    void onSuccess(LoginResult loginResult)
    {
        Debug.Log("Login Success");
        messageText.text = "Login Success";
        if (loginResult.InfoResultPayload.PlayerProfile != null)
        {
            if(loginResult.InfoResultPayload.PlayerProfile.DisplayName == null)
            {

            }
            else
            {
                loggedInText.text = "Welcome " + loginResult.InfoResultPayload.PlayerProfile.DisplayName.ToString();
            

            loginObjects.SetActive(false);
                loggedInObjects.SetActive(true);
            }
        }
        else
        {
            hasDisplayName = false;
        }
    }
    void onError(PlayFabError error)
    {
        Debug.LogWarning("Error while logging in");
        Debug.LogWarning(error.GenerateErrorReport());
        try
        {
            messageText.text = error.GenerateErrorReport();
        }
        catch
        {

        }
    }
    public void sendActivityData(List<activityMonitor.activityData> activityData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Activity Data", JsonConvert.SerializeObject(activityData) }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, onDataSend, onError);
    }
    public void getActivityData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnActivityDataRecieved, onError);
    }
    void OnActivityDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Received");
        if(result.Data != null && result.Data.ContainsKey("Activity Data"))
        {
            List<activityMonitor.activityData> _activityData = JsonConvert.DeserializeObject<List<activityMonitor.activityData>>(result.Data["Activity Data"].Value);

            FindObjectOfType<activityMonitor>().updateData(_activityData);
        }
        else
        {
            FindObjectOfType<activityMonitor>().createData();
        }
    }
    void onDataSend(UpdateUserDataResult result)
    {
        Debug.Log(result);
    }
    public void SendLeaderboard(int score, string list)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = list,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, onError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log(result);
    }
    public void GetLeaderboard(string list)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = list,
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, onError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard Data Recieved");
        FindObjectOfType<LeaderboardManager>().recieveData(result);
    }
}
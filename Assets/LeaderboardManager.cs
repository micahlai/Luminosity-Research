using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] positions;
    public TextMeshProUGUI[] scores;
    // Start is called before the first frame update
    void Start()
    {
        foreach(TextMeshProUGUI t in positions)
        {
            t.text = "n/a";
        }
        foreach (TextMeshProUGUI t in scores)
        {
            t.text = "n/a";
        }
    }

    public void recieveData(GetLeaderboardResult result)
    {
        for (int i = 0; i < result.Leaderboard.ToArray().Length; i++)
        {
            positions[i].text = result.Leaderboard[i].DisplayName;
            scores[i].text = result.Leaderboard[i].StatValue.ToString();
        }
    }
    public void refreshData(string title)
    {
        PlayfabManager.manager.GetLeaderboard(title);
    }
}

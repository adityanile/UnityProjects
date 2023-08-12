using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    /// <summary>
    /// 
    /// Public Key :- 4477289268227c032d76ae36474c2f7656660c7943da9f12ddc153265391a121
    /// Secret Key :- c32f4bb97f4948522a08393202953e43063eedf029b23a399b65997f05e806942ebe977ef916dd1ddf9c19720da7069c5a978ff3086c4038071341e2263a9001827903cfc02279435a70152d9dd69d98be97e828ac8cd3368e2e1210012d68f252602d6160cece49c73f8565c8ae49eb61d45ee333699aa7b95c8181923a9247
    /// 
    /// </summary>

    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> time;
    public List<TextMeshProUGUI> rank;
    public GameObject reconnect;
    public GameObject message;

    private string publickey = "4477289268227c032d76ae36474c2f7656660c7943da9f12ddc153265391a121";

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publickey, true , ((gotdata) =>
        {
            int loopcount = (names.Count > gotdata.Length) ? gotdata.Length : names.Count;

            for(int i=0; i < loopcount; i++)
            {
                names[i].text = gotdata[i].Username;
                time[i].text = SecondsToMinutes(gotdata[i].Score).ToString();
                rank[i].text = (i+1).ToString();
            }

            if (names[0].text == "")
            {
                reconnect.SetActive(true);
            }

        }));
    }

    public void SetLeaderBoard(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publickey, username, score, ((msg) =>
        {
            GetLeaderBoard();     // When Upload an entry update LeadderBoard;
        }
        // This is calling a function within the funtion
        ));
    }

    public void StartNote()
    {
        message.SetActive(true);      // Note when we start the leaderboard
        StartCoroutine(StopNote());
    }

    IEnumerator StopNote()
    {
        yield return new WaitForSeconds(9);
        message.SetActive(false);
    }

    public float SecondsToMinutes(int seconds)
    {
        float minutes = 0;

        while(seconds > 60)
        {
            seconds -= 60;
            minutes++;
        }

        return (minutes + (seconds * 0.01f));
    }

}

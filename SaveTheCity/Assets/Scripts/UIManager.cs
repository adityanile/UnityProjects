using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject option;
    public GameObject optiontext;

    public GameObject leaderboard;
    public GameObject gameUI;
    public LeaderBoard leaderBoard;

    public GameObject notice;

    // Start is called before the first frame update
    void Start()
    {
        optiontext.SetActive(false);
        leaderboard.SetActive(false);

        StartCoroutine(CloseNotice()); // Close Notice UI

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);  // This is to load the main scene
    }

    public void OnClickOptions()
    {
        option.SetActive(false);
        optiontext.SetActive(true);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickLeaderBoard()
    {
        leaderBoard.GetLeaderBoard();       // Get Scores from LeaderBoard 
        leaderboard.SetActive(true);
        gameUI.SetActive(false);
        leaderBoard.StartNote();
    }

    public void OnClickBack()
    {
        leaderboard.SetActive(false);
        gameUI.SetActive(true);
    }

    IEnumerator CloseNotice()
    {
        yield return new WaitForSeconds(7);
        notice.SetActive(false);
    }

}

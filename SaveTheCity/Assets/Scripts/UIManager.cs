using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject option;
    public GameObject optiontext;
   

    // Start is called before the first frame update
    void Start()
    {
        optiontext.SetActive(false);
        
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

   

    

}

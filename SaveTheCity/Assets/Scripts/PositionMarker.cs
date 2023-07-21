using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionMarker : MonoBehaviour
{
    public GameObject positionMarker;
    public GameObject player;

    // UI Changes in Position Marker
    public GameObject mainpanel;
    public TextMeshProUGUI positionmarker;
    int showthisafter = 40;
    int showfor = 10;
    public bool shown = false;

    private InGameUI gameUI;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();
        positionmarker.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnMarker();
        }

        if((gameUI.maintime.seconds >= showthisafter) && !shown)
        {
            shown = true;
            DisplayUi();
        }
    }

    void DisplayUi()
    {
        mainpanel.SetActive(true);
        positionmarker.text = "You Can Press 'R' To Mark Your Position";
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(showfor);
        mainpanel.SetActive(false);
        positionmarker.text = "";
    }

    void SpawnMarker()
    { 
      Instantiate(positionMarker, player.transform.position, positionMarker.transform.rotation);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public GameObject mark1;
    public GameObject mark2;
    public GameObject mark3;
    public GameObject mark4;

    public GameObject panel;
    public GameObject rightpanel;


    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        mark1.SetActive(false);
        mark2.SetActive(false);
        mark3.SetActive(false);
        mark4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Mark1"))
            {
                panel.SetActive(true);
                mark1.SetActive(true);
                rightpanel.SetActive(false);
            }
            if (other.gameObject.CompareTag("Mark2"))
            {
                panel.SetActive(true);
                mark2.SetActive(true);
                rightpanel.SetActive(false);
            }
            if (other.gameObject.CompareTag("Mark3"))
            {
                panel.SetActive(true);
                mark3.SetActive(true);
                rightpanel.SetActive(false);
            }
            if (other.gameObject.CompareTag("Mark4"))
            {
                panel.SetActive(true);
                mark4.SetActive(true);
                rightpanel.SetActive(false);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Mark1"))
            {
                panel.SetActive(false);
                mark1.SetActive(false);
                rightpanel.SetActive(true);
            }
            if (other.gameObject.CompareTag("Mark2"))
            {
                panel.SetActive(false);
                mark2.SetActive(false);
                rightpanel.SetActive(true);
            }
            if (other.gameObject.CompareTag("Mark3"))
            {
                panel.SetActive(false);
                mark3.SetActive(false);
                rightpanel.SetActive(true);
            }
            if (other.gameObject.CompareTag("Mark4"))
            {
                panel.SetActive(false);
                mark4.SetActive(false);
                rightpanel.SetActive(true);
            }
        }
    }

}

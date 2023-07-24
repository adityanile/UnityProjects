using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMusic : MonoBehaviour
{
    private LevelManager levelManager;

    public AudioClip maze1clip;
    public AudioClip maze2clip;
    public AudioClip maze3clip;

    private AudioSource playaudio;
    public float volumne = 1;

    public Slider slider;

    // Walking of the player
    private AudioSource walkmanager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Player").GetComponent<LevelManager>();
        playaudio = GetComponent<AudioSource>();

        walkmanager = GameObject.Find("LevelManager").GetComponent<AudioSource>();

        volumne = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        WhenToStopAudio();
        ControlWalk();

        playaudio.volume = slider.value;
    }

    void WhenToStopAudio()
    {
        if (levelManager.currentmaze == 0)
        {
            playaudio.Stop();
        }
    }

    public void Maze1Audio()
    {
        playaudio.clip = maze1clip;
        playaudio.Play();
    }
    public void Maze2Audio()
    {
        playaudio.clip = maze2clip;
        playaudio.Play();
    }
    public void Maze3Audio()
    {
        playaudio.clip = maze3clip;
        playaudio.Play();
    }

    void ControlWalk()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            walkmanager.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {

            walkmanager.Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            walkmanager.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            walkmanager.Stop();
        }
    }
}

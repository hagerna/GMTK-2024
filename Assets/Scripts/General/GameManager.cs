using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RunManager.Begin.AddListener(HandleBegin);
        RunManager.Stop.AddListener(HandleStop);
        ShapesManager.AllShapesLocked.AddListener(IncreaseTempo);
        AudioManager.instance.Play("Menu Music");
    }

    private void HandleBegin()
    {
        AudioManager.instance.Stop("Menu Music");
        // Add Begin Sound Effect
        AudioManager.instance.ChangePitch("Round Music", 1f);
        AudioManager.instance.Play("Round Music");
    }

    private void HandleStop()
    {
        // Add End Sound Effect
        AudioManager.instance.Stop("Round Music");
        AudioManager.instance.Play("Menu Music");
    }

    private void IncreaseTempo()
    {
        AudioManager.instance.ChangePitch("Round Music", 1.25f);
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        RunManager.instance.LoadTutorial();
    }

    public void Play()
    {
        SceneManager.LoadScene("Base");
        RunManager.instance.LoadNewRun();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}

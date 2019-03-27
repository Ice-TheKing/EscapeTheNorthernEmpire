using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool IsPaused = false;
    public GameObject PauseMenuPrefab;
    private GameObject pauseMenuInstance;

    void Start()
    {
        if (PauseMenuPrefab == null)
        {
            throw new NullReferenceException("Must supply a pause menu! Pause menu script is on " + gameObject.name);
        }
        pauseMenuInstance = Instantiate(PauseMenuPrefab);
        foreach (var cc in pauseMenuInstance.GetComponentsInChildren<PauseMenuButton>())
        {
            cc.Pauser = this;
        }
        pauseMenuInstance.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuInstance.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        pauseMenuInstance.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

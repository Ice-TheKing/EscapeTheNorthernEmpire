using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    public PauseController Pauser;

    void Start()
    {
        if (Pauser == null)
        {
            Pauser = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseController>();
        }
    }

    public void QuitClicked()
    {
        Pauser.Quit();
    }

    public void ResumeClicked()
    {
        Pauser.Resume();
    }
}

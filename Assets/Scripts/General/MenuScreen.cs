using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public void OnTutorial()
    {
        GameManager.instance.PlayTutorial();
    }

    public void OnPlay()
    {
        GameManager.instance.Play();
    }

    public void OnExit()
    {
        GameManager.instance.Exit();
    }

    public void CheckboxSelected(GameObject checkMark)
    {
        Generator.instance.ToggleRandomLoadout(checkMark);
    }
}

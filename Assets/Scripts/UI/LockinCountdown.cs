using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockinCountdown: MonoBehaviour
{
    TextMeshProUGUI text;
    bool LockedIn = false;
    RunManager rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = RunManager.instance;
        ShapesManager.AllShapesLocked.AddListener(ShowCountdown);
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    void ShowCountdown()
    {
        LockedIn = true;
    }

    public void HideCountdown()
    {
        LockedIn = false;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (LockedIn)
        {
            text.text = rm.GetLockInTime().ToString("0.00");
        }
    }
}

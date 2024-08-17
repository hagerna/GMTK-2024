using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockinCountdown: MonoBehaviour
{
    TextMeshProUGUI text;
    bool LockedIn = false;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.LockIn.AddListener(ShowCountdown);
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    void ShowCountdown()
    {
        LockedIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LockedIn)
        {
            text.text = gm.lockInTimer.ToString("0.00");
        }
    }
}

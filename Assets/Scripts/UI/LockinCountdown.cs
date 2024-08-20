using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockinCountdown: MonoBehaviour
{
    TextMeshProUGUI text;
    bool LockedIn = false;
    RunManager rm;
    [SerializeField]
    GameObject Lock1, Lock2;
    // Start is called before the first frame update
    void Start()
    {
        rm = RunManager.instance;
        ShapesManager.AllShapesLocked.AddListener(ShowCountdown);
        RunManager.LoadLevel.AddListener(HideOnLoad);
        text = GetComponent<TextMeshProUGUI>();
        HideCountdown();
        
    }

    void ShowCountdown()
    {
        Lock1.SetActive(true);
        Lock2.SetActive(true);
        LockedIn = true;
    }

    public void HideCountdown()
    {
        Lock1.SetActive(false);
        Lock2.SetActive(false);
        LockedIn = false;
        text.text = "";
    }

    private void HideOnLoad(int arg0, LevelSO arg1)
    {
        HideCountdown();
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

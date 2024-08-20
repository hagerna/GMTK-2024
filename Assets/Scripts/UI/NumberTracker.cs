using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTracker : MonoBehaviour
{
    [SerializeField]
    DisplayType displayType;
    TextMeshProUGUI text;
    RunManager rm;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rm = RunManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (displayType)
        {
            case DisplayType.Score:
                text.text = "Score: " + rm.GetRoundScore().ToString("0");
                break;
            case DisplayType.Currency:
                text.text = "Currency: " + rm.GetRoundCurrency().ToString("0");
                break;
            case DisplayType.TotalCurrency:
                text.text = "Currency: " + rm.GetTotalCurrency().ToString("0");
                break;
            case DisplayType.TargetScore:
                text.text = "Next Target: " + rm.GetTargetScore().ToString("0");
                break;
        }
    }
}

public enum DisplayType {
    Score,
    TargetScore,
    Currency,
    TotalCurrency
}

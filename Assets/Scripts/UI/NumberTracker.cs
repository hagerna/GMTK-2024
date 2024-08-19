using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTracker : MonoBehaviour
{
    [SerializeField]
    ProductionType displayType;
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
            case ProductionType.Score:
                text.text = "Score: " + rm.GetRoundScore().ToString("0");
                break;
            case ProductionType.Currency:
                text.text = "Currency: " + rm.GetRoundCurrency().ToString("0");
                break;


        }
    }
}

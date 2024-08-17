using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTracker : MonoBehaviour
{
    [SerializeField]
    ProductionType displayType;
    TextMeshProUGUI text;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (displayType)
        {
            case ProductionType.Score:
                text.text = "Score: " + gm.score.ToString("0");
                break;
            case ProductionType.Currency:
                text.text = "Currency: " + gm.roundCurrency.ToString("0");
                break;


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRow : MonoBehaviour
{
    [SerializeField]
    UpgradeUI UpgradeUIPrefab;
    [SerializeField]
    UpgradeSO[] Upgrades;
    [SerializeField]
    Transform StartingPoint;
    [SerializeField]
    float refreshCost = 10;
    List<UpgradeUI> availableUpgrades = new List<UpgradeUI>();
    RunManager rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = RunManager.instance;
        Refresh();
    }

    void Refresh()
    {
        foreach(UpgradeUI upgrade in availableUpgrades)
        {
            Destroy(upgrade.gameObject);
        }
        availableUpgrades.Clear();
        int offsetX = 60;
        for (int i = 0; i < 6; i++)
        {
            UpgradeUI temp = Instantiate(UpgradeUIPrefab, StartingPoint.position + new Vector3(offsetX, 0, 0), Quaternion.identity, transform);
            temp.SetUpgrade(Upgrades[Random.Range(0, Upgrades.Length)], 1);
            availableUpgrades.Add(temp);
            offsetX += 145;
        }
    }

    public void OnRefreshPressed()
    {
        if (rm.CheckPurchase(refreshCost))
        {
            rm.MakePurchase(refreshCost);
            Refresh();
        }
    }
}

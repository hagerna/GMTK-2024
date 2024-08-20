using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField]
    GameObject blockUpgrades;
    RunManager rm;
    UpgradeUI purchasingUpgrade;

    public static UpgradesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnNextLevel()
    {
        RunManager.instance.LoadNextLevel();
        gameObject.SetActive(false);
    }

    public bool HandlePurchase(UpgradeUI upgrade)
    {
        if (rm == null)
        {
            rm = RunManager.instance;
        }
        if (!rm.CheckPurchase(upgrade.GetCost())){
            return false;
        }
        else
        {
            UpgradeSO data = upgrade.GetData();
            if (data.Class == UpgradeClass.NewShape)
            {
                ShapesManager.instance.AddShape(data.NewShape);
                rm.MakePurchase(upgrade.GetCost());
            }
            else
            {
                purchasingUpgrade = upgrade;
                ShapeUI.Select.AddListener(HandleSelection);
                blockUpgrades.SetActive(true);
            }
            return true;
        }
    }

    void HandleSelection(ShapeUI shapeUI)
    {
        RunManager.instance.MakePurchase(purchasingUpgrade.GetCost());
        ApplyUpgradeToShape(purchasingUpgrade.GetData(), shapeUI.GetData());
        shapeUI.Refresh();
        purchasingUpgrade.ShowPurchase();
        purchasingUpgrade = null;
        blockUpgrades.SetActive(false);
        ShapeUI.Select.RemoveListener(HandleSelection);
        ShapeUI.Select.Invoke(shapeUI);
    }

    public void CancelWait()
    {
        ShapeUI.Select.RemoveListener(HandleSelection);
        blockUpgrades.SetActive(false);
        purchasingUpgrade = null;
    }

    private void ApplyUpgradeToShape(UpgradeSO upgrade, ShapeSO shape)
    {
        if (upgrade.Class == UpgradeClass.Upgrade)
        {
            switch (upgrade.Type)
            {
                case UpgradeType.Width:
                    shape.width *= upgrade.Modifier;
                    break;
                case UpgradeType.Height:
                    shape.height *= upgrade.Modifier;
                    break;
                case UpgradeType.Size:
                    shape.width *= upgrade.Modifier;
                    shape.height *= upgrade.Modifier;
                    break;
                case UpgradeType.VerticalGrowth:
                    shape.verticalGrowth *= upgrade.Modifier;
                    break;
                case UpgradeType.HorizontalGrowth:
                    shape.horizontalGrowth *= upgrade.Modifier;
                    break;
                case UpgradeType.Growth:
                    shape.verticalGrowth *= upgrade.Modifier;
                    shape.horizontalGrowth *= upgrade.Modifier;
                    break;
                case UpgradeType.ProductionMultiplier:
                    shape.prodMultiplier *= upgrade.Modifier;
                    break;
                case UpgradeType.ProductionDelay:
                    shape.prodDelay *= upgrade.Modifier;
                    break;

            }
        }
    }
}

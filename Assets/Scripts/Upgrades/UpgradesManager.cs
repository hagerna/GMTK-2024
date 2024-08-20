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
            if (data.Class == UpgradeClass.UpgradeShape || data.Class == UpgradeClass.Modifier)
            {
                purchasingUpgrade = upgrade;
                ShapeUI.Select.AddListener(HandleSelection);
                blockUpgrades.SetActive(true);
            }
            else if (data.Class == UpgradeClass.GeneralUpgrade)
            {
                ApplyGeneralUgrade(data);
                upgrade.ShowPurchase();

            }
            else if (data.Class == UpgradeClass.NewShape)
            {
                ShapesManager.instance.AddShape(data.NewShape);
                upgrade.ShowPurchase();
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
        if (upgrade.Class == UpgradeClass.UpgradeShape)
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
                    if (shape.verticalGrowth == 0)
                    {
                        shape.verticalGrowth = upgrade.Modifier - 1;
                    }
                    else
                    {
                        shape.verticalGrowth = upgrade.Modifier;
                    }
                    break;
                case UpgradeType.HorizontalGrowth:
                    if (shape.horizontalGrowth == 0)
                    {
                        shape.horizontalGrowth = upgrade.Modifier - 1;
                    }
                    else
                    {
                        shape.horizontalGrowth *= upgrade.Modifier;
                    }
                    break;
                case UpgradeType.Growth:
                    if (shape.verticalGrowth == 0)
                    {
                        shape.verticalGrowth = upgrade.Modifier - 1;
                    }
                    else
                    {
                        shape.verticalGrowth = upgrade.Modifier;
                    }
                    if (shape.horizontalGrowth == 0)
                    {
                        shape.horizontalGrowth = upgrade.Modifier - 1;
                    }
                    else
                    {
                        shape.horizontalGrowth *= upgrade.Modifier;
                    }
                    break;
                case UpgradeType.ProductionMultiplier:
                    shape.prodMultiplier *= upgrade.Modifier;
                    break;
                case UpgradeType.ProductionDelay:
                    shape.prodDelay *= upgrade.Modifier;
                    break;

            }
        }
        else if (upgrade.Class == UpgradeClass.Modifier)
        {
            shape.ModifierTags.Add(upgrade.UpgradeName);
        }
    }

    private void ApplyGeneralUgrade(UpgradeSO upgrade)
    {
        switch (upgrade.Type)
        {
            case UpgradeType.Width:
                break;
        }
    }
}

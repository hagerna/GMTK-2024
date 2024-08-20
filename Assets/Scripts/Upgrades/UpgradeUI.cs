using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public static UnityEvent<UpgradeUI> UpgradeSelected = new UnityEvent<UpgradeUI>();

    [SerializeField]
    GameObject selectionBorder, purchaseCover;
    [SerializeField]
    Image upgradeSprite;
    [SerializeField]
    TextMeshProUGUI costText;
    bool purchased;

    UpgradeSO upgrade;
    float costMultiplier;

    public void SetUpgrade(UpgradeSO baseUpgrade, float priceModifier)
    {
        upgrade = baseUpgrade;
        upgradeSprite.sprite = upgrade.Sprite;
        if (baseUpgrade.Class == UpgradeClass.NewShape)
        {
            ShapeSO shape = baseUpgrade.NewShape;
            upgradeSprite.color = Constants.GetBackgroundColor(new Vector2(shape.horizontalGrowth, shape.verticalGrowth));
            if (shape.prodType == ProductionType.Currency)
            {
                upgradeSprite.color = Constants.GREEN;
            }
            float max = Mathf.Max(shape.width, shape.height);
            upgradeSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(shape.width/max, shape.height/max) * 120f;
        }
        costMultiplier = priceModifier;
        costText.text = GetCost().ToString();
    }

    public void Select()
    {
        if (!purchased)
        {
            selectionBorder.SetActive(true);
            UpgradeSelected.Invoke(this);
        }
    }

    public void Deselect()
    {
        selectionBorder.SetActive(false);
    }

    public void ShowPurchase()
    {
        purchased = true;
        purchaseCover.SetActive(true);
        UpgradeSelected.Invoke(null);
    }

    public string GetName() { return upgrade.UpgradeName; }
    public string GetDescription() { return upgrade.Description; }
    public Sprite GetSprite() { return upgrade.Sprite; }
    public float GetModifier() { return upgrade.Modifier; }
    public float GetCost() { return upgrade.Cost * costMultiplier; }
    public UpgradeSO GetData() { return upgrade; }
    public bool IsPurchased() { return purchased; }
}

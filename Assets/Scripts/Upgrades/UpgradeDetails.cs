using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeDetails : MonoBehaviour
{
    [SerializeField]
    Image upgradeVisual, Vertical, Horizontal, Directional;
    [SerializeField]
    Sprite Grow, Shrink;
    [SerializeField]
    TextMeshProUGUI upgradeName, description, modifier, cost, helpMessage;
    UpgradeUI selectedUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeUI.UpgradeSelected.AddListener(UpdateDisplay);
        HideDetails();
    }

    private void OnEnable()
    {
        HideDetails();
    }

    public void UpdateDisplay(UpgradeUI selected)
    {
        if (selectedUpgrade != null)
        {
            selectedUpgrade.Deselect();
        }
        ShowDetails();
        selectedUpgrade = selected;
        upgradeName.text = string.Format("{0}", selectedUpgrade.GetName());
        upgradeVisual.sprite = selectedUpgrade.GetSprite();
        if (selectedUpgrade.GetData().Class != UpgradeClass.NewShape)
        {
            description.text = string.Format("{0}", selectedUpgrade.GetDescription());
            float displayModifier = (selectedUpgrade.GetModifier() - 1) * 100;
            if (displayModifier > 0)
            {
                modifier.text = string.Format("Increase by {0}%", Mathf.Abs(displayModifier));
            }
            else if (displayModifier < 0)
            {
                modifier.text = string.Format("Decrease by {0}%", Mathf.Abs(displayModifier));
            }
        }
        else
        {
            ShapeSO shape = selected.GetData().NewShape;
            upgradeVisual.color = Constants.GetBackgroundColor(new Vector2(shape.horizontalGrowth, shape.verticalGrowth));
            if (shape.prodType == ProductionType.Currency)
            {
                upgradeVisual.color = Constants.GREEN;
            }
            HandleArrows(shape);
            float max = Mathf.Max(shape.width, shape.height);
            upgradeVisual.GetComponent<RectTransform>().sizeDelta = new Vector2(shape.width / max, shape.height / max) * 250f;
            string descriptionText = string.Format("A {0} Scaler with multiplier {1}. Base size of {2} x {3}. Growth is {4}/sec x {5}/sec. ",
                shape.scalerType.ToString(), shape.prodMultiplier.ToString("0.00"), shape.width, shape.height, shape.verticalGrowth * 10f, shape.horizontalGrowth * 10f);
            if (shape.ModifierTags.Count > 0)
            {
                descriptionText += string.Format("Modifiers: {0}", string.Join(",", shape.ModifierTags));
            }
            else
            {
                descriptionText += "Modifiers: None";
            }
            description.text = descriptionText;
            modifier.text = "";
        }
        cost.text = string.Format("{0}", selectedUpgrade.GetCost().ToString("0"));
    }

    private void HideDetails()
    {
        upgradeVisual.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        modifier.gameObject.SetActive(false);
        cost.text = "Purchase";
        upgradeName.text = "Select an Upgrade";
    }

    private void ShowDetails()
    {
        upgradeVisual.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        modifier.gameObject.SetActive(true);
        Vertical.gameObject.SetActive(false);
        Horizontal.gameObject.SetActive(false);
        Directional.gameObject.SetActive(false);
    }

    public void OnPurchasePressed()
    {
        if (!UpgradesManager.instance.HandlePurchase(selectedUpgrade))
        {
            StartCoroutine(ShowHelpMessage("Not Enough Currency."));
        }
    }

    IEnumerator ShowHelpMessage(string message)
    {
        helpMessage.text = message;
        yield return new WaitForSeconds(5f);
        helpMessage.text = "";
    }

    private void HandleArrows(ShapeSO shape)
    {
        if (shape.scalerType == ScalerType.Directional)
        {
            Directional.gameObject.SetActive(true);
            Directional.transform.rotation = Quaternion.Euler(0, 0, shape.rotation);
            return;
        }

        // Set Vertical Arrows
        if (shape.verticalGrowth > 0)
        {
            Vertical.gameObject.SetActive(true);
            Vertical.sprite = Grow;
        }
        else if (shape.verticalGrowth < 0)
        {
            Vertical.gameObject.SetActive(true);
            Vertical.sprite = Shrink;
        }

        // Set Horizontal Arrows
        if (shape.horizontalGrowth > 0)
        {
            Horizontal.gameObject.SetActive(true);
            Horizontal.sprite = Grow;
        }
        else if (shape.horizontalGrowth < 0)
        {
            Horizontal.gameObject.SetActive(true);
            Horizontal.sprite = Shrink;
        }
    }
}

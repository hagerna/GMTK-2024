using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeDetails : MonoBehaviour
{
    [SerializeField]
    Image upgradeVisual;
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
        description.text = string.Format("{0}", selectedUpgrade.GetDescription());
        cost.text = string.Format("{0}", selectedUpgrade.GetCost().ToString("0"));
        upgradeVisual.sprite = selectedUpgrade.GetSprite();
        float displayModifier = (selectedUpgrade.GetModifier() - 1) * 100;
        if (displayModifier > 0)
        {
            modifier.text = string.Format("Increase by {0}%", Mathf.Abs(displayModifier));
        }
        else
        {
            modifier.text = string.Format("Decrease by {0}%", Mathf.Abs(displayModifier));
        }
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
}

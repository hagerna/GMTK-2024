using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Results : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI header, target, roundScore, roundCurrency, totalCurrency;
    [SerializeField]
    GameObject resultsPopup, storeButton, endRunButton, tryAgainButton, gameOverButton, upgradesScreen;
    RunManager rm;

    private void Start()
    {
        RunManager.Stop.AddListener(ShowResults);
        rm = RunManager.instance;
        HideResults();
    }

    public void ShowResults()
    {
        resultsPopup.SetActive(true);
        bool levelComplete = rm.GetTargetScore() <= rm.GetRoundScore();
        target.text = string.Format("Target: {0}", rm.GetTargetScore().ToString("0"));
        roundScore.text = string.Format("Score: {0}", rm.GetRoundScore().ToString("0"));
        if (levelComplete)
        {
            header.text = "Level Completed";
            header.color = Constants.GREEN;
            roundCurrency.text = string.Format("Currency Earned: {0}", rm.GetRoundCurrency().ToString("0"));
            totalCurrency.text = string.Format("Currency Total: {0}", rm.GetTotalCurrency().ToString("0"));
            storeButton.SetActive(true);
            endRunButton.SetActive(true);
        }
        else
        {
            header.text = "Level Failed";
            header.color = Constants.RED;
            roundCurrency.text = "Currency only earned on Completion";
            totalCurrency.text = string.Format("Attempts Remaining: {0}", rm.GetAttempts());
            if (rm.GetAttempts() > 0)
            {
                endRunButton.SetActive(true);
                tryAgainButton.SetActive(true);
            }
            else
            {
                gameOverButton.SetActive(true);
            }
        }
    }

    public void OpenStore()
    {
        HideResults();
        upgradesScreen.SetActive(true);
        ShapesManager.instance.ClearAllScalers();

    }

    public void TryAgain()
    {
        rm.ResetRound();
        ShapesManager.instance.ClearAllScalers();
        HideResults();

    }

    public void EndRun()
    {
        GameManager.instance.GoToMenu();
    }

    public void HideResults()
    {
        storeButton.SetActive(false);
        endRunButton.SetActive(false);
        tryAgainButton.SetActive(false);
        gameOverButton.SetActive(false);
        resultsPopup.SetActive(false);
    }
}

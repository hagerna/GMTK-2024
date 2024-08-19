using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI targetScore, levelTitle, helpMessage;
    [SerializeField]
    Image background;
    [SerializeField]
    Transform playSpaceAnchor;
    Tutorial tutorial;

    // Start is called before the first frame update
    void Start()
    {
        RunManager.LoadLevel.AddListener(UpdateDisplay);
        UpdateDisplay(RunManager.instance.GetLevel(), RunManager.instance.GetLevelData());
    }

    void UpdateDisplay(int level, LevelSO levelData)
    {
        targetScore.text = string.Format("Target Score: {0}", RunManager.instance.GetTargetScore());
        levelTitle.text = levelData.LevelName;
        background.color = levelData.BackgroundColor;
        Instantiate(levelData.PlayAreaPrefab, playSpaceAnchor);
    }

    public void OnBeginPress()
    {
        if (RunManager.instance.GetLevel() == 0)
        {
            if (tutorial == null)
            {
                tutorial = FindObjectOfType<Tutorial>();
            }
            if (!tutorial.TutorialComplete)
            {
                StartCoroutine(ShowHelpMessage("Complete all steps of the Tutorial first."));
                return;
            }
        }
        if (!RunManager.instance.TriggerBegin())
        {
            StartCoroutine(ShowHelpMessage("All Shapes must be placed to Begin."));
        }
    }

    IEnumerator ShowHelpMessage(string message)
    {
        helpMessage.text = message;
        yield return new WaitForSeconds(5f);
        helpMessage.text = "";
    }
}

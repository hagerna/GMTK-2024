using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject[] tutorialSteps;
    int currentStep;
    public bool TutorialComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject step in tutorialSteps)
        {
            step.SetActive(false);
        }
        currentStep = 0;
        tutorialSteps[currentStep].SetActive(true);
    }

    public void NextStep()
    {
        tutorialSteps[currentStep].SetActive(false);
        currentStep++;
        tutorialSteps[currentStep].SetActive(true);
    }

    public void Complete()
    {
        tutorialSteps[currentStep].SetActive(false);
        TutorialComplete = true;
    }

    public void StartOver()
    {
        tutorialSteps[currentStep].SetActive(false);
        currentStep = 0;
        tutorialSteps[currentStep].SetActive(true);
    }
}

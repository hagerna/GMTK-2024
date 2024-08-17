using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scaler: Producer
{
    public static UnityEvent LockedIn = new UnityEvent();
    [SerializeField]
    protected float verticalGrowthRate, horizontalGrowthRate;
    [SerializeField]
    protected bool verticalSpace = true;
    [SerializeField]
    protected bool horizontalSpace = true;
    protected bool activelyScaling = false;

    private void Start()
    {
        GameManager.instance.Begin.AddListener(Begin);
    }

    protected void Begin()
    {
        isProducing = true;
        StartCoroutine(Scale());
    }

    public bool IsScaling() { return activelyScaling; }

    protected virtual IEnumerator Scale()
    {
        activelyScaling = true;
        while (verticalSpace && horizontalSpace)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = newScale.x * (1f + (horizontalGrowthRate * Time.deltaTime));
            newScale.y = newScale.y * (1f + (verticalGrowthRate * Time.deltaTime));
            if (newScale.x < 0.4f)
            {
                newScale.x = 0.4f;
                horizontalSpace = false;
            }
            if (newScale.y < 0.4f)
            {
                newScale.y = 0.4f;
            }
            transform.localScale = newScale;
            yield return new WaitForFixedUpdate();
        }
        activelyScaling = false;
        LockedIn.Invoke();
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        horizontalSpace = false;
        verticalSpace = false;
        Debug.Log("Collision Detected");
    }
}

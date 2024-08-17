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
        GameManager.instance.Stop.AddListener(StopProduction);
    }

    public virtual void Setup(ShapeSO scriptable)
    {
        SetupProduction(scriptable);
        transform.localScale = new Vector2(scriptable.width, scriptable.height);
        verticalGrowthRate = scriptable.verticalGrowth;
        horizontalGrowthRate = scriptable.horizontalGrowth;
        GameManager.instance.ShapePlaced(this);

        // Setup Appearance
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (verticalGrowthRate >= 0 && horizontalGrowthRate >= 0)
        {
            renderer.color = Constants.RED;
        }
        else if (verticalGrowthRate >= 0 || horizontalGrowthRate >= 0)
        {
            renderer.color = Constants.PURPLE;
        }
        else
        {
            renderer.color = Constants.BLUE;
        }
        if (scriptable.prodType == ProductionType.Currency)
        {
            renderer.color = Constants.GREEN;
        }
    }

    protected void Begin()
    {
        isProducing = true;
        if (verticalGrowthRate != 0 && horizontalGrowthRate != 0)
        {
            StartCoroutine(Scale());
        }
        else
        {
            CanNoLongerGrow();
        }
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
        CanNoLongerGrow();
    }

    protected void CanNoLongerGrow()
    {
        activelyScaling = false;
        LockedIn.Invoke();
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        horizontalSpace = false;
        verticalSpace = false;
    }
}

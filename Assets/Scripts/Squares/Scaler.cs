using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scaler: Producer
{
    public static UnityEvent LockedIn = new UnityEvent();
    [SerializeField]
    Sprite Grow, Shrink;
    [SerializeField]
    SpriteRenderer Vertical, Horizontal;
    [SerializeField]
    GameObject _selectBorder, _lockPrefab;
    GameObject _lockInstance;

    protected float verticalGrowth, horizontalGrowth;
    protected bool verticalSpace = true;
    protected bool horizontalSpace = true;
    protected bool activelyScaling = false;

    private void Start()
    {
        RunManager.Begin.AddListener(Begin);
        RunManager.Stop.AddListener(StopProduction);
        ShapeSelected();
    }

    public virtual void Setup(ShapeSO scriptable)
    {
        SetupProduction(scriptable);
        transform.localScale = new Vector2(scriptable.width, scriptable.height);
        verticalGrowth = scriptable.verticalGrowth;
        horizontalGrowth = scriptable.horizontalGrowth;
        UpdateAppearance();
    }

    protected void Begin()
    {
        isProducing = true;
        if (Vertical != null)
        {
            Vertical.gameObject.SetActive(false);
        }
        if (Horizontal != null)
        {
            Horizontal.gameObject.SetActive(false);
        }
        if (verticalGrowth != 0 && horizontalGrowth != 0)
        {
            StartCoroutine(Scale());
        }
        else
        {
            // Give Buffer time so Lock-In doesn't start immediately
            Invoke("CanNoLongerGrow", 0.5f);
        }
    }

    public bool IsScaling() { return activelyScaling; }

    protected virtual IEnumerator Scale()
    {
        activelyScaling = true;
        while (verticalSpace && horizontalSpace)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = newScale.x * (1f + (horizontalGrowth * Time.deltaTime));
            newScale.y = newScale.y * (1f + (verticalGrowth * Time.deltaTime));
            if (newScale.x < 0.4f)
            {
                newScale.x = 0.4f;
                horizontalSpace = false;
            }
            if (newScale.y < 0.4f)
            {
                newScale.y = 0.4f;
                verticalSpace = false;
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
        _lockInstance = Instantiate(_lockPrefab, transform.position, Quaternion.identity);
        float size = Mathf.Min(transform.localScale.x, transform.localScale.y);
        _lockInstance.transform.localScale *= size;
    }

    private void OnDestroy()
    {
        if (_lockInstance != null)
        {
            Destroy(_lockInstance);
        }
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        horizontalSpace = false;
        verticalSpace = false;
    }

    protected void SetBackgroundColor()
    {
        // Set Background
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Constants.GetBackgroundColor(new Vector2(horizontalGrowth, verticalGrowth));
        if (GetProdType() == ProductionType.Currency)
        {
            renderer.color = Constants.GREEN;
        }
    }

    protected virtual void UpdateAppearance()
    {
        SetBackgroundColor();

        // Set Vertical Arrows
        if (verticalGrowth > 0)
        {
            Vertical.sprite = Grow;
        }
        else if (verticalGrowth < 0)
        {
            Vertical.sprite = Shrink;
        }
        else
        {
            Vertical.sprite = null;
        }

        // Set Horizontal Arrows
        if (horizontalGrowth > 0)
        {
            Horizontal.sprite = Grow;
        }
        else if (horizontalGrowth < 0)
        {
            Horizontal.sprite = Shrink;
        }
        else
        {
            Horizontal.sprite = null;
        }
    }

    public void Deselect()
    {
        _selectBorder.SetActive(false);
    }

    public void ShapeSelected()
    {
        _selectBorder.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    ProductionType _type = ProductionType.Score;
    float _prodDelay = 1f;
    float _prodTimer = 0f;
    protected float _prodMultiplier = 1f;
    protected float _lockInMultiplier = 0f;
    protected bool isProducing = false;

    protected void SetupProduction(ShapeSO scriptable)
    {
        _type = scriptable.prodType;
        _prodMultiplier = scriptable.prodMultiplier;
        _lockInMultiplier = scriptable.lockInMultiplier;
        _prodDelay = scriptable.prodDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isProducing)
        {
            return;
        }
        if (_prodTimer >= _prodDelay)
        {
            Produce();
            _prodTimer = 0f;
        }
        else
        {
            _prodTimer += Time.deltaTime;
        }
    }

    protected void StopProduction()
    {
        isProducing = false;
    }

    public virtual void Produce()
    {
        GameManager.instance.AddProduction(_type, _prodMultiplier * getArea());
    }

    public float getArea()
    {
        return transform.localScale.x * transform.localScale.y;
    }
}

public enum ProductionType
{
    Score,
    Currency,
}

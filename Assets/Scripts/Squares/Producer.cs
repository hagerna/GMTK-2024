using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    protected float productionMultiplier = 1f;
    ProductionType type = ProductionType.Score;
    float _prodDelay = 1f;
    float _prodTimer = 0f;
    protected bool isProducing = false;

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

    public virtual void Produce()
    {
        GameManager.instance.AddProduction(type, productionMultiplier * getArea());
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

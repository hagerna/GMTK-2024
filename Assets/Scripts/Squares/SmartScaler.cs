using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartScaler : Scaler
{
    protected override IEnumerator Scale()
    {
        activelyScaling = true;
        while (verticalSpace || horizontalSpace)
        {
            Vector3 newScale = transform.localScale;
            if (horizontalSpace)
            {
                newScale.x = newScale.x * (1f + (horizontalGrowthRate * Time.deltaTime));
            }
            if (verticalSpace)
            {
                newScale.y = newScale.y * (1f + (verticalGrowthRate * Time.deltaTime));
            }
            transform.localScale = newScale;
            yield return new WaitForFixedUpdate();
        }
        activelyScaling = false;
        LockedIn.Invoke();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 _collisionDirection = (collision.transform.position - transform.position).normalized;
        if (Mathf.Abs(_collisionDirection.x) > Mathf.Abs(_collisionDirection.y))
        {
            horizontalSpace = false;
        }
        else
        {
            verticalSpace = false;
        }
    } 
}

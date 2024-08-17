using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionScaler : SmartScaler
{
    Vector3 growDirection = Vector3.up;
    Vector3 startPosition;
    float halfStartingHeight;

    private void Awake()
    {
        startPosition = transform.position;
        halfStartingHeight = transform.localScale.y / 2;
    }

    protected override IEnumerator Scale()
    {
        activelyScaling = true;
        while (verticalSpace)
        {
            Vector3 newScale = transform.localScale;
            newScale.y = newScale.y * (1f + (verticalGrowthRate * Time.deltaTime));
            transform.localScale = newScale;
            transform.position = startPosition + ((newScale.y/2 - 0.5f) * growDirection);
            yield return new WaitForFixedUpdate();
        }
        activelyScaling = false;
        LockedIn.Invoke();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 leftCorner = new Vector2(transform.position.x - transform.localScale.x/2, transform.position.y + transform.localScale.y/2 + 0.02f);
        Vector2 rightCorner = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y + transform.localScale.y/2 + 0.02f);
        Debug.DrawRay(leftCorner, growDirection);
        Debug.DrawRay(rightCorner, growDirection);
        RaycastHit2D leftCornerRay = Physics2D.Raycast(leftCorner, growDirection, 0.1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D rightCornerRay = Physics2D.Raycast(rightCorner, growDirection, 0.1f, LayerMask.GetMask("Obstacles"));
        if (leftCornerRay.collider != null || rightCornerRay.collider != null)
        {
            Debug.Log("Left Corner Hit: " + leftCornerRay.collider.name);
            Debug.Log("Right Corner Hit: " + rightCornerRay.collider.name);
            verticalSpace = false;
        }
    }
}

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
        // Raycast all edges to see which direction is limiting growth
        Vector2 topRightCorner = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y + transform.localScale.y / 2 + 0.02f);
        RaycastHit2D topFace = Physics2D.Raycast(topRightCorner, -transform.right, transform.localScale.x / 2, LayerMask.GetMask("Obstacles"));
        topRightCorner += new Vector2(0.02f, -0.02f);
        RaycastHit2D rightFace = Physics2D.Raycast(topRightCorner, -transform.up, transform.localScale.x / 2, LayerMask.GetMask("Obstacles"));
        Vector2 botLeftCorner = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2 - 0.02f);
        RaycastHit2D botFace = Physics2D.Raycast(botLeftCorner, transform.right, transform.localScale.x / 2, LayerMask.GetMask("Obstacles"));
        botLeftCorner += new Vector2(-0.02f, 0.02f);
        RaycastHit2D leftFace = Physics2D.Raycast(botLeftCorner, transform.up, transform.localScale.x / 2, LayerMask.GetMask("Obstacles"));
        if (rightFace.collider != null || leftFace.collider != null)
        {
            horizontalSpace = false;
        }
        if (topFace.collider != null || botFace.collider != null)
        {
            verticalSpace = false;
        }
    } 
}

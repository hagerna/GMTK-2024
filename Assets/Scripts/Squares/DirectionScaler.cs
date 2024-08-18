using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionScaler : SmartScaler
{
    [SerializeField]
    GameObject _directionArrow;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public override void Setup(ShapeSO scriptable)
    {
        base.Setup(scriptable);
        transform.rotation = Quaternion.Euler(0, 0, scriptable.rotation);
    }

    protected override IEnumerator Scale()
    {
        activelyScaling = true;
        _directionArrow.SetActive(false);
        while (verticalSpace)
        {
            Vector3 newScale = transform.localScale;
            newScale.y = newScale.y * (1f + (verticalGrowth * Time.deltaTime));
            transform.localScale = newScale;
            transform.position = startPosition + ((newScale.y/2 - 0.5f) * transform.up);
            yield return new WaitForFixedUpdate();
        }
        activelyScaling = false;
        LockedIn.Invoke();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 growingEdge = startPosition + ((transform.localScale.y - 0.48f) * transform.up);
        RaycastHit2D leftCornerRay = Physics2D.Raycast(growingEdge, -transform.right, transform.localScale.x/2, LayerMask.GetMask("Obstacles"));
        RaycastHit2D rightCornerRay = Physics2D.Raycast(growingEdge, transform.right, transform.localScale.x/2, LayerMask.GetMask("Obstacles"));
        if (leftCornerRay.collider != null || rightCornerRay.collider != null)
        {
            verticalSpace = false;
        }
    }

    protected override void UpdateAppearance()
    {
        SetBackgroundColor();
    }
}

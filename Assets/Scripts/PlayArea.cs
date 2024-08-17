using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject selectedPiece;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPosition = FindAnyObjectByType<Camera>().ScreenToWorldPoint(eventData.position);
        print("clicked position on the gameobject  is :" + clickPosition.x);
        Instantiate(selectedPiece, clickPosition, Quaternion.identity);
    }
}

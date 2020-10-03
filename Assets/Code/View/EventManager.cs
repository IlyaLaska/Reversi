using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{
    public delegate void MouseDownEvent();
    public static event MouseDownEvent BoardSquareClicked;


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
        Debug.Log("Called MouseDownEvent");
        if (BoardSquareClicked != null)
            BoardSquareClicked();
    }
}

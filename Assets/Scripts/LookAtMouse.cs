using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector2 mousePos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookMouse();
    }

    void LookMouse()
	{
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - new Vector2(transform.position.x,transform.position.y);
        transform.right = dir;
	}
}

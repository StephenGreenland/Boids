using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    private Transform t;
    
        RaycastHit2D hit;

    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.Raycast(new Vector2(t.position.x, t.position.y), GetDirection(45f), 5f);
        Debug.DrawRay(new Vector2(t.position.x, t.position.y), GetDirection(45f)*5f,Color.red );

    }

    Vector2 GetDirection(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Rad2Deg)).normalized;

    }
    
    
}

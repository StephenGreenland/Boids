using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Fiod : MonoBehaviour
{
    
    
    public Transform forward;
    public GameObject pos;
    private Transform posT;

    public float seeingRange;
   
    public float maxSpeed;
    public float speed;
    public Rigidbody2D rb;

    //public Transform target;
    public float rotationspeed;


    private RaycastHit2D hit;

    
    //RaycastHit hit;

    void Start()
    {
        posT = pos.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        

    }


    void FixedUpdate()
    {
          
        
        MoveForward();
        
        
 
        hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),new Vector2(forward.position.x,forward.position.y), seeingRange);
        if (hit.collider == null)
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0),new Vector3(forward.position.x, forward.position.y, 0), Color.green);
                
        }
        else
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0),new Vector3(forward.position.x, forward.position.y, 0), Color.red);
        }
            
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject);
            RotateTowards(Look());

        }
            
            
            
        
        
    }

    void MoveForward()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        rb.AddRelativeForce(new Vector2(0, 1) * Time.deltaTime * speed);
    }

    public Vector2 Look()
    {
        for (int i = 0; i < 35; i++)
        {
            pos.transform.eulerAngles = new Vector3(0,0,i*4);
            hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),new Vector2(forward.position.x,forward.position.y),Vector3.Distance(transform.position,forward.position));
            if (hit.collider == null)
            {
                Debug.DrawLine(new Vector3(posT.position.x, posT.position.y, 0),new Vector3(forward.position.x, forward.position.y, 0), Color.yellow);
                pos.transform.rotation = transform.rotation;
                return new Vector2 (forward.position.x,forward.position.y);
                
            }

            
            
            pos.transform.eulerAngles = new Vector3(0,0,i*-4);
            hit = Physics2D.Raycast(new Vector2(posT.position.x,posT.position.y),new Vector2(forward.position.x,forward.position.y),Vector3.Distance(posT.position,forward.position));
            if (hit.collider == null)
            {
                Debug.DrawLine(new Vector3(posT.position.x, posT.position.y, 0),new Vector3(forward.position.x, forward.position.y, 0), Color.green);
                pos.transform.rotation = transform.rotation;
                return new Vector2 (forward.position.x,forward.position.y);
                
            }

        }
        

        return Vector2.zero;
    }

    public void RotateTowards(Vector2 target)
    {
        Vector2 direction = target - new Vector2(transform.position.x,transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotationspeed * Time.deltaTime);
    }
    
    
}
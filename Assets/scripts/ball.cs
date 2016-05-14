using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {
    Rigidbody2D r;
    //int c;
    // Use this for initialization
    void Start () {
        r.velocity = new Vector2(3, 5);
        r.freezeRotation = true;
        //c = 0;
	
	}

    void Awake()
    {
        r = GetComponent<Rigidbody2D>();
        Debug.Log("test kinematics");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("Collision " + coll.collider.name);
        string s = coll.collider.name;
        if (s == "root" || s== "ground")
        {
            r.velocity = new Vector2(r.velocity.x, -r.velocity.y);
        }
        if (s == "left_wall" || s == "right_wall")
        {
            r.velocity = new Vector2(-r.velocity.x, r.velocity.y);
        }
        if (s == "batty")
        {
            //r.velocity = new Vector2(r.velocity.x, -r.velocity.y);
            
            foreach (ContactPoint2D con in coll.contacts)
            {
                r.velocity = r.velocity - 2 * Vector2.Dot(r.velocity, con.normal) * con.normal;
                Debug.Log(" cpoint " + con.point.x + con.point.y);
                Debug.Log(" ball_pos " + transform.position.x + transform.position.y);
                Debug.Log("normal x" + con.normal.x + "  y  " + con.normal.y);
            }
        }
        foreach (ContactPoint2D contact in coll.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //c++;
        Debug.Log("Collision ");
        /*foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }*/
    }

        void FixedUpdate ()
    {

    }
}

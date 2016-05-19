using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {
    Rigidbody2D r;
    //int c;
    // Use this for initialization
    private bool stop = false;
    private Vector3 stop_pos;
    private GameObject batty;
    private float speed=6;
    private int block_count=5;
    void Start () {
        r.velocity = new Vector2(3, 5);
        r.velocity = r.velocity.normalized * speed;
        r.freezeRotation = true;
        block_count = GameObject.FindGameObjectsWithTag("block").Length;
        batty = GameObject.FindGameObjectWithTag("batty");
        //c = 0;
	
	}

    void destroy_block(GameObject block)
    {
        Destroy(block);
        block_count--;
        if (block_count<=0)
        {
            batty.SendMessage("IWin");
        }
    }

    void Awake()
    {
        r = GetComponent<Rigidbody2D>();
        Debug.Log("test kinematics");
    }
	
	// Update is called once per frame
	void Update () {
        if (stop)
        {
            Vector3 v = transform.position - stop_pos;
            if (v.magnitude > 0.5)
            {
                batty.SendMessage("UnStop");
                stop = false;
            }
        }

    }
    void OnCollisionExit2D(Collision2D coll)
    {
        r.velocity = r.velocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("Collision " + coll.collider.name);
        string s = coll.collider.name;
        
        if (s == "root" ) // ||  s== "ground")
        {
            r.velocity = new Vector2(r.velocity.x, -r.velocity.y);
        }
        if (s== "ground")
        {
            batty.SendMessage("ILose");
        }
        if (s == "left_wall" || s == "right_wall")
        {
            r.velocity = new Vector2(-r.velocity.x, r.velocity.y);
        }
        if (s == "batty")
        {
            //r.velocity = new Vector2(r.velocity.x, -r.velocity.y);
            stop = true;
            stop_pos = transform.position;
            coll.gameObject.SendMessage("Stop");
            batty = coll.gameObject;
            foreach (ContactPoint2D con in coll.contacts)
            {
                //r.velocity = r.velocity - 2 * Vector2.Dot(r.velocity, con.normal) * con.normal;
                if (Vector2.Dot(con.normal, r.velocity) <= 0)
                {
                    r.velocity = Vector2.Reflect(r.velocity, con.normal);
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1000, Color.red,2, false);
                //Debug.Log(" cpoint " + con.point.x + con.point.y);
                //Debug.Log(" ball_pos " + transform.position.x + transform.position.y);
                //Debug.Log("normal x" + con.normal.x + "  y  " + con.normal.y);
                
            }
        }
        s = coll.collider.tag;
        if (s=="block")
        {
            foreach ( ContactPoint2D con in coll.contacts)
            {
                r.velocity = Vector2.Reflect(r.velocity, con.normal);
            }
            destroy_block(coll.collider.gameObject);
        }
        foreach (ContactPoint2D contact in coll.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white,3);
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

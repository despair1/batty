using UnityEngine;
using System.Collections;

public class char_move : MonoBehaviour {
    float max_speed = 10;
    Rigidbody2D r;
    private bool stop = false;
    private bool start = true;
    private bool win = false;
    private bool lose = false;
	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody2D>();
        Time.timeScale = 0;

    }

    void OnGUI()
    {
        if ( start )
        {
            GUI.Label(new Rect(450, 290, 120, 20), " press space to start ");
        }
        if (win)
        {
            GUI.Label(new Rect(450, 270, 120, 20), " You win ");
        }
        if (lose)
        {
            GUI.Label(new Rect(450, 250, 120, 20), " You lose ");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!stop)
        {
            float move = Input.GetAxis("Horizontal");
            //Rigidbody2D.velocity = new Vector2(move * max_speed, Rigidbody2D.velocity.y);
            //Rigidbody2D.
            //Rigidbody2D.v
            r.velocity = new Vector2(move * max_speed, r.velocity.y);
        }
        
	}
    void Update()
    {
        RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.down);
        if (r)
        {
           // Debug.Log("hit "+r.collider.name+"dist "+r.distance);
        }
        if ( start )
        {
            if ( Input.GetKeyDown(KeyCode.Space))
            {
                start = false;
                Time.timeScale = 1;
            }
        }
        if (win || lose)
        {
            Time.timeScale = 0;
        }


    }
    void UnStop()
    {
        stop = false;
    }
    void Stop()
    {
        Debug.Log("Stop");
        r.velocity = new Vector2(0, 0);
        stop = true;
    }
    void IWin()
    {
        win = true;
    }
    void ILose()
    {
        lose = true;
    }
}

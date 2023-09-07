using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake_movement : MonoBehaviour
{
    grid2D gr;
    Vector2 current_location;
    public int mode = 0;
    public Transform target;
    [SerializeField] points point;

    // Start is called before the first frame update
    void Start()
    {
        gr =new grid2D(gameObject);
        current_location = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log( GetComponent<dijkstra>().find(target.GetChild(0)));
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveforword(mode);
            
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mode = (mode + 1) % 4;
            moveforword(mode);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mode = (mode - 1);
            if (mode < 0) mode += 4;
            moveforword(mode);
        }

    }

    /* action = 1 is move forword 
       action = 2 is take right and move forword
       action = 0 is take left and move forword */
    public void move(int action)
    {
        
        if (action==1)
        {
            moveforword(mode);
        }

        else if (action==2)
        {
            mode = (mode + 1) % 4;
            moveforword(mode);
        }
        else if (action==0)
        {
            mode = (mode - 1);
            if (mode < 0) mode += 4;
            moveforword(mode);
        }
    }

    void moveforword(int side)
    {
        //Debug.Log(transform.position.x+" "+transform.position.y+" "+GetComponent<boundry>().checkboundry(new Vector3(transform.position.x, transform.position.y, transform.position.z)));

        switch (side)
        {
            case 0:
                gr.move_up();
                break;
            case 1:
                gr.move_right();
                break;
            case 2:
                gr.move_down();
                break;
            case 3:
                gr.move_left();
                break;
            default:
                Debug.Log(side + " error");
                break;
        }
        //GetComponent<agent>().ixedUpdate();
        point.Point();

        update_tail();
    }

    void update_tail()
    {
        //Debug.Log(transform.GetChild(0).name);
        if (transform.childCount > 0) transform.GetChild(0).GetComponent<follow>().move(current_location);
        current_location = transform.position;

        //Debug.Log(transform.GetChild(0).GetComponent<follow>().check(transform.position));
        
    }
}

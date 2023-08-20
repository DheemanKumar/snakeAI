using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Vector2 current_location;

    // Start is called before the first frame update
    void Start()
    {
        current_location = transform.position;
    }


    public void move(Vector2 moveto)
    {
        //Debug.Log("dun");
        transform.position = new Vector2(moveto.x, moveto.y);

        if (transform.childCount > 2)
        {
            transform.GetChild(2).GetComponent<follow>().move(current_location);
        }

        current_location = moveto;
    }

    public bool check(Vector3 location)
    {

        if (transform.childCount > 2)
        {
            if (transform.position == location)
            {
                return true;
            }
            else
            {
                return transform.GetChild(2).GetComponent<follow>().check(location);
            }

        }
        else
        {
            if (transform.position == location)
            {
                return true;
            }
            else
            return false;
        }
    }


}

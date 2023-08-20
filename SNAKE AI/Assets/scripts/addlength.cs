using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addlength : MonoBehaviour
{
    public bool add;

    public GameObject tail;

    public GameObject newblock;

    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            add = true;
        }

        if (add)
        {
            //Debug.Log(tail.transform.childCount);
            addnew();

        }
    }

    public void addnew() {
        if (tail.transform.childCount > 2)
        {
            tail = tail.transform.GetChild(2).gameObject;
        }
        else
        {
            GameObject obj = Instantiate(newblock, tail.transform);
            obj.transform.localPosition = Vector3.zero;
            add = false;
        }
    }

    
}

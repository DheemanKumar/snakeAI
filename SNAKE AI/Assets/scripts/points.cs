using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{
    public GameObject head;
    public GameObject point;

    public Vector2 max;
    public Vector2 min;



    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //Point();
    }

    public bool Point()
    {
        if (head.transform.position == transform.GetChild(0).position)
        {
            Destroy(transform.GetChild(0).gameObject);
            head.GetComponent<addlength>().add = true;
            spawn();
            return true;
        }
        else return false;
    }

    void spawn()
    {
        int x = (int)Random.Range(min.x, max.x);
        int y = (int)Random.Range(min.y, max.y);

        while(head.transform.GetChild(0).GetComponent<follow>().check(new Vector3(x, y, 0)) || head.transform.position == new Vector3(x, y, 0))
        {
            x = (int)Random.Range(min.x, max.x);
            y = (int)Random.Range(min.y, max.y);
        }
        GameObject p = Instantiate(point, transform);

        p.transform.position = new Vector2(x, y);
    }



}

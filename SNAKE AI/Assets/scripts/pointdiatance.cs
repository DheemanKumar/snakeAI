using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointdiatance : MonoBehaviour
{
    public float[] distance;
    snake_movement sm;
    public Transform Point;
     Transform point;
    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<snake_movement>();
        distance = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        point = Point.GetChild(0);
        distance[0] = 0;
        distance[1] = 0;
        switch (sm.mode)
        {
            case 0:
                
                if (transform.position.x > point.position.x) distance[1] = -1;
                if (transform.position.x < point.position.x) distance[1] = 1;
                if (transform.position.y > point.position.y) distance[0] = -1;
                if (transform.position.y < point.position.y) distance[0] = 1;
                break;

            case 1:

                if (transform.position.x > point.position.x) distance[0] = -1;
                if (transform.position.x < point.position.x) distance[0] = 1;
                if (transform.position.y > point.position.y) distance[1] = 1;
                if (transform.position.y < point.position.y) distance[1] = -1;
                break;

            case 2:

                if (transform.position.x > point.position.x) distance[1] = 1;
                if (transform.position.x < point.position.x) distance[1] = -1;
                if (transform.position.y > point.position.y) distance[0] = 1;
                if (transform.position.y < point.position.y) distance[0] = -1;
                break;

            case 3:

                if (transform.position.x > point.position.x) distance[0] = 1;
                if (transform.position.x < point.position.x) distance[0] = -1;
                if (transform.position.y > point.position.y) distance[1] = -1;
                if (transform.position.y < point.position.y) distance[1] = 1;
                break;
        }
    }


}

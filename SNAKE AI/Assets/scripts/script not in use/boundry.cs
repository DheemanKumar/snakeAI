using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundry : MonoBehaviour
{
    public Vector2 boundry_x;
    public Vector2 boundry_y;

    public float[] distance;

    private snake_movement sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<snake_movement>();
        distance = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        switch (sm.mode)
        {
            case 0:
                distance[0] = transform.position.x - boundry_x.y+1;
                distance[1] = -transform.position.y + boundry_y.x;
                distance[2] = -transform.position.x + boundry_x.x;
                break;
            case 1:
                distance[0] = -transform.position.y + boundry_y.x;
                distance[1] = -transform.position.x + boundry_x.x;
                distance[2] = transform.position.y - boundry_y.y+1;
                break;
            case 2:
                distance[0] = -transform.position.x - boundry_x.y;
                distance[1] = transform.position.y + boundry_y.x+1;
                distance[2] = transform.position.x + boundry_x.x+1;
                break;
            case 3:
                distance[0] = transform.position.y + boundry_y.x+1;
                distance[1] = transform.position.x + boundry_x.x+1;
                distance[2] = -transform.position.y - boundry_y.y;
                break;
            default:
                break;
        }

    }

    public bool checkboundry(Vector3 point) {
        if (point.x < boundry_x.x && point.x+1 > boundry_x.y && point.y < boundry_y.x && point.y+1 > boundry_y.y) return true;
        else return false;
    }

}

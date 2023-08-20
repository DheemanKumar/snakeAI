using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_distance : MonoBehaviour
{
    public Transform Target;

    public int[] distance;
    private int mode;

    // Start is called before the first frame update
    void Start()
    {
        distance = new int[2];
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = Target.GetChild(0);
        mode = GetComponent<snake_movement>().mode;

        switch (mode)
        {
            case 0:
                distance[0] = (int)(target.position.y - transform.position.y);
                distance[1] = (int)(target.position.x - transform.position.x);
                break;
            case 1:
                distance[0] = (int)(target.position.x - transform.position.x);
                distance[1] = -(int)(target.position.y - transform.position.y);
                break;
            case 2:
                distance[0] = -(int)(target.position.y - transform.position.y);
                distance[1] = -(int)(target.position.x - transform.position.x);
                break;
            case 3:
                distance[0] = -(int)(target.position.x - transform.position.x);
                distance[1] = (int)(target.position.y - transform.position.y);
                break;
            default:
                break;
        }
    }
}

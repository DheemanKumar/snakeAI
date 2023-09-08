using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sidebody : MonoBehaviour
{
    public float[] distance;

    public follow next;
    public int radius=5;

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
        bool a = false;
        bool b = false;
        bool c = false;
        switch (sm.mode) {
            

            case 0:
                for (int i = 0; i < radius; i++) {
                    if( next.check(new Vector3(transform.position.x, transform.position.y+i, transform.position.z)) && !a) {
                    a = true;
                        distance[1] = i;
                    }
                    if (next.check(new Vector3(transform.position.x-i, transform.position.y, transform.position.z)) && !b)
                    {
                        b = true;
                        distance[0] = i;
                    }
                    if (next.check(new Vector3(transform.position.x + i, transform.position.y, transform.position.z)) && !c)
                    {
                        c = true;
                        distance[2] = i;
                    }
                    if (a && b && c)
                    {
                        break;
                    }
                }

                if (!a) distance[1] = radius;
                if (!b) distance[0] = radius;
                if (!c) distance[2] = radius;

                break;

            case 1:
                for (int i = 0; i < radius; i++)
                {
                    if (next.check(new Vector3(transform.position.x+i, transform.position.y , transform.position.z)) && !a)
                    {
                        a = true;
                        distance[1] = i;
                    }
                    if (next.check(new Vector3(transform.position.x , transform.position.y+i, transform.position.z)) && !b)
                    {
                        b = true;
                        distance[0] = i;
                    }
                    if (next.check(new Vector3(transform.position.x , transform.position.y-i, transform.position.z)) && !c)
                    {
                        c = true;
                        distance[2] = i;
                    }
                    if (a && b && c)
                    {
                        break;
                    }
                }

                if (!a) distance[1] = radius;
                if (!b) distance[0] = radius;
                if (!c) distance[2] = radius;

                break;

            case 2:
                for (int i = 0; i < radius; i++)
                {
                    if (next.check(new Vector3(transform.position.x , transform.position.y-i, transform.position.z)) && !a)
                    {
                        a = true;
                        distance[1] = i;
                    }
                    if (next.check(new Vector3(transform.position.x+i, transform.position.y , transform.position.z)) && !b)
                    {
                        b = true;
                        distance[0] = i;
                    }
                    if (next.check(new Vector3(transform.position.x-i, transform.position.y, transform.position.z)) && !c)
                    {
                        c = true;
                        distance[2] = i;
                    }
                    if (a && b && c)
                    {
                        break;
                    }
                }

                if (!a) distance[1] = radius;
                if (!b) distance[0] = radius;
                if (!c) distance[2] = radius;

                break;

            case 3:
                for (int i = 0; i < radius; i++)
                {
                    if (next.check(new Vector3(transform.position.x - i, transform.position.y, transform.position.z)) && !a)
                    {
                        a = true;
                        distance[1] = i;
                    }
                    if (next.check(new Vector3(transform.position.x, transform.position.y - i, transform.position.z)) && !b)
                    {
                        b = true;
                        distance[0] = i;
                    }
                    if (next.check(new Vector3(transform.position.x, transform.position.y + i, transform.position.z)) && !c)
                    {
                        c = true;
                        distance[2] = i;
                    }
                    if (a && b && c)
                    {
                        break;
                    }
                }

                if (!a) distance[1] = radius;
                if (!b) distance[0] = radius;
                if (!c) distance[2] = radius;

                break;

            default:
                distance[0] = 0;
                distance[1] = 0;
                distance[2] = 0;
                break;
        }


    }

    





}

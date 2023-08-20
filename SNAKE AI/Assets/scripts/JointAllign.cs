using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAllign : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.parent != null)
        transform.position = new Vector3((transform.parent.position.x + transform.parent.parent.position.x) / 2, (transform.parent.position.y + transform.parent.parent.position.y) / 2,0);
    }
}

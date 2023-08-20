using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;


public class buttons : MonoBehaviour
{
    [SerializeField] bool auto;
    [SerializeField] bool manual;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        if (auto)
        {
            if (constants.auto) transform.GetChild(0).GetComponent<Image>().enabled = true;
            else transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        if (manual)
        {
            if (constants.manual) transform.GetChild(0).GetComponent<Image>().enabled = true;
            else transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public void Auto() {
        constants.auto = !constants.auto;
        
    }

    public void Manual() {
        constants.manual = true;
    }
}

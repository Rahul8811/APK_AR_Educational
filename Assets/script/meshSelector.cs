using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TurbineObject;
    public bool isActive = true;

    public void Toggle()
    {
        if (isActive)
        {
            TurbineObject.SetActive(false);
            isActive = false;
        }
        else
        {
            TurbineObject.SetActive(true);
            isActive = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

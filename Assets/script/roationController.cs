using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roationController : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 rotationVector;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector*Time.deltaTime);
    }
}

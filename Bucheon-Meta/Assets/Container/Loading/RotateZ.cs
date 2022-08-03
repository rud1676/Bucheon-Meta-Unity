using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZ : MonoBehaviour
{
    public float angle = 50f;

    

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, angle) * Time.deltaTime);
    }

}

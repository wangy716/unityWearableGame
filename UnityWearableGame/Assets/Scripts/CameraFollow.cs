using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public Transform target;
    public float smoothSpeed = 0.125f;
    //public float offset;
    //public GameObject newball;
    //private Transform lastChild;

    public GameObject selectedOne;



    private void FixedUpdate()
    {
        if (selectedOne == null)
        {
            return;
        }
        else
        {
            //lastChild = newball.transform.GetChild(newball.transform.childCount - 1);
            //float desiredZ = target.position.z + offset;
            float desiredZ = selectedOne.transform.position.z;
            //Debug.Log(desiredZ);
            float smoothedZ = Mathf.Lerp(transform.position.z, desiredZ, smoothSpeed);
            //Debug.Log(smoothedZ);

            transform.position = new Vector3(transform.position.x, transform.position.y, smoothedZ);
        }

    }


}

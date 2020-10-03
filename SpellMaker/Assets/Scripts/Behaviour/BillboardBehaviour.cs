using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{

    [SerializeField] private bool staticBillboard = true;
    [SerializeField] private bool matchX = true;
    [SerializeField] private bool matchY = true;
    [SerializeField] private bool matchZ = true;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 previousEuler = transform.rotation.eulerAngles;

        if(staticBillboard)
        {
            transform.LookAt(mainCamera.transform);
        }
        else
        {
            transform.rotation = mainCamera.transform.rotation;
        }

        Vector3 targetEuler = transform.rotation.eulerAngles;

        if(!matchX)
        {
            targetEuler.x = previousEuler.x;
        }
        if(!matchY)
        {
            targetEuler.y = previousEuler.y;
        }
        if (!matchZ)
        {
            targetEuler.z = previousEuler.z;
        }

        transform.rotation = Quaternion.Euler(targetEuler);
    }
}

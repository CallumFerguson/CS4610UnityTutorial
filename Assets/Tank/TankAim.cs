using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    public Transform target;

    public Transform turret;
    public Transform barrel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetLocalPosition = transform.InverseTransformPoint(target.position);

        var targetFlat = targetLocalPosition;
        targetFlat.y = 0;
        
        turret.localRotation = Quaternion.LookRotation(targetFlat);

        var targetNoX = turret.InverseTransformPoint(target.position);
        targetNoX.x = 0;
        
        barrel.localRotation = Quaternion.LookRotation(targetNoX);
    }
}
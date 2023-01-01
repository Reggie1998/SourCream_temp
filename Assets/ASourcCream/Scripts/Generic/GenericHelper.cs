using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenericHelper
{   
    private static RaycastHit hit;

    //Note will need update for co-op
    public static Vector3 GetMousePostion() {
        int layerMask = 1 << 8;
        var hitResult = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, layerMask);
        var hitPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        return hitPos;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public Color rayColor = Color.white;

    private Transform[] path;

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        Transform[] pathObjs = GetComponentsInChildren<Transform>();
        path = new Transform[pathObjs.Length - 1];

        for (int i = 1; i < pathObjs.Length; i++)
        {
            if (pathObjs[i] != transform)
                path[i - 1] = pathObjs[i];
        }

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 pos = path[i].position;

            if (i > 0)
            {
                Vector3 prev = path[i - 1].position;
                Gizmos.DrawLine(prev, pos);
                Gizmos.DrawWireSphere(pos, 0.3f);
            }
        }
    }
}

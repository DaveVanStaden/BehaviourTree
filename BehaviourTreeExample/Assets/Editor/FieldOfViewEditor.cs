using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position,Vector3.up, Vector3.forward, 360, fov.viewRadius);

        Vector3 viewAngle01 = fov.DirFromAngle(-fov.viewAngle/ 2, false);
        Vector3 viewAngle02 = fov.DirFromAngle(fov.viewAngle/ 2, false);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.viewRadius);
        if (fov.PlayerVisible)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef[0].transform.position);

        }
    }
}

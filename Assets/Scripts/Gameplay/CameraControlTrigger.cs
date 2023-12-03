using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customIspectorObjects;

    private Collider2D _coll;

    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player") )
        {
            if ( customIspectorObjects.panCameraOnContact )
            {
                // pan the camera
                CameraManager.instance.PanCameraOnContact(customIspectorObjects.panDistance, customIspectorObjects.panTime, customIspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _coll.bounds.center).normalized;

            if (customIspectorObjects.swapCameras && customIspectorObjects.cameraOnLeft != null && customIspectorObjects.cameraOnRight != null)
            {
                // swap cameras
                CameraManager.instance.SwapCameras(customIspectorObjects.cameraOnLeft, customIspectorObjects.cameraOnRight, exitDirection);
            }

            if (customIspectorObjects.panCameraOnContact)
            {
                // pan the camera
                CameraManager.instance.PanCameraOnContact(customIspectorObjects.panDistance, customIspectorObjects.panTime, customIspectorObjects.panDirection, true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    up,
    down, 
    left, 
    right,
}

[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor: Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (cameraControlTrigger.customIspectorObjects.swapCameras)
        {
            cameraControlTrigger.customIspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera on left", cameraControlTrigger.customIspectorObjects.cameraOnLeft,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.customIspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera on right", cameraControlTrigger.customIspectorObjects.cameraOnRight,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if ( cameraControlTrigger.customIspectorObjects.panCameraOnContact )
        {
            cameraControlTrigger.customIspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("camera pan direction",
                cameraControlTrigger.customIspectorObjects.panDirection);

            cameraControlTrigger.customIspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraControlTrigger.customIspectorObjects.panDistance);
            cameraControlTrigger.customIspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", cameraControlTrigger.customIspectorObjects.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}
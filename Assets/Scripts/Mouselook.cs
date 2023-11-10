using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    [Header("Min and Max camera view")]
    private const float YMin = -50f;
    private const float YMax = 50f;

    [Header("camera view")]
    public Transform lookAt;
    public Transform player;


    [Header("Camera Position")]
    public float CameraDistance = 10f;
    private float CurrentX= 0.0f;
    private float CurrentY= 0.0f;
    public float CameraSensitivity = 4f;

    public FloatingJoystick floatingJoystick;


    private void LateUpdate()
    {
        CurrentX += floatingJoystick.Horizontal * CameraSensitivity * Time.deltaTime;
        CurrentY -= floatingJoystick.Horizontal * CameraSensitivity * Time.deltaTime;

        CurrentY = Mathf.Clamp(CurrentY, YMin, YMax);
        Vector3 Direction = new Vector3(0, 0, -CameraDistance);

        Quaternion rotation = Quaternion.Euler(CurrentY, CurrentX, 0);

        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);

    }


}

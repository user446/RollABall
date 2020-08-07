using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows to make own gravity and tilt it a bit by swipes
/// </summary>
public class TiltGravity : MonoBehaviour
{
    public float gravityScale = 1.0f;
    private static Vector3 globalGravity;
    private Rigidbody rb;
    private Touch touch;
    private Quaternion rotation;
    public Vector2 deltaPosition;
    public Vector2 rotationAdjust = new Vector2(1, 1);
    public Vector3 gravity = new Vector3();
    public float maxRotation = 20f;
    public float minRotation = -20f;
    void OnEnable ()
    {
        globalGravity = Physics.gravity * gravityScale;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.AddForce(globalGravity, ForceMode.Acceleration);
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rotation = Quaternion.Euler(
                                        -Mathf.Clamp(touch.deltaPosition.y * rotationAdjust.y, minRotation, maxRotation), 
                                        0, 
                                        Mathf.Clamp(touch.deltaPosition.x * rotationAdjust.x, minRotation, maxRotation));
                deltaPosition = touch.deltaPosition;
            }
        }
    }
 
    void FixedUpdate ()
    {
        gravity = rotation * globalGravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}

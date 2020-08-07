using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows to tilt a rigidbody of an object
/// </summary>
public class TiltControl : MonoBehaviour
{
    private Rigidbody tiltAim;
    private Touch touch;
    private Quaternion rotation;
    public Vector2 rotationAdjust = new Vector2(1, 1);
    public float swipeMin = -1f;
    public float swipeMax = 1f;
    public Vector2 deltaPosition;
    void Start()
    {
        rotation = Quaternion.Euler(0,0,0);
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rotation = Quaternion.Euler(
                                        Mathf.Clamp(touch.deltaPosition.y * rotationAdjust.y, swipeMin, swipeMax), 
                                        0, 
                                        -Mathf.Clamp(touch.deltaPosition.x * rotationAdjust.x, swipeMin, swipeMax));
                deltaPosition = touch.deltaPosition;
            }
            else
            {
                rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        tiltAim = EnvironmentManager.GetCurrentEnvironment().GetComponent<Rigidbody>();
        if(EnvironmentManager.GetCurrentEnvironment() != null)
            tiltAim.MoveRotation(tiltAim.rotation * rotation);
    }
}

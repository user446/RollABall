using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Environment trigger class that allows to react only on specific collider tag
/// </summary>
public class EnvironmentTrigger : MonoBehaviour
{
    public Action OnTriggerActivate;
    public string activationTag;
    void OnTriggerEnter(Collider colliderInfo)
    {
        if(colliderInfo.tag == activationTag)
        {
            OnTriggerActivate();
        }
    }
}

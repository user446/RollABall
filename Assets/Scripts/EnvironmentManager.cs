using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnvironmentManager : MonoBehaviour
{
    [Tooltip("List of possible environments which will be used")]
    public List<GameObject> environmentsPossible;

    [Tooltip("List of current environments available at runtime")]
    public GameObject environmentCurrent;
    public Vector3 spawnOffset;
    private Environment startEnvironment;
    public static EnvironmentManager _instance;

    /// <summary>
    /// Here Environment Manager should find first environment, which will be our journey begin
    /// </summary>
    void Awake()
    {
        _instance = this;
        startEnvironment = FindObjectOfType<Environment>();
        startEnvironment.halfwayPassed += CreateNewEnvironment;
        _instance.environmentCurrent = startEnvironment.gameObject;
    }

    public static GameObject GetCurrentEnvironment()
    {
        return _instance.environmentCurrent;
    }

    /// <summary>
    /// Creates new environment on New Environment Spawner position of current environment
    /// </summary>
    void CreateNewEnvironment()
    {
        var new_env_ref = environmentsPossible[UnityEngine.Random.Range(0, environmentsPossible.Count())];
        Debug.Log("Got a reference for new environment " + new_env_ref);
        var new_env = Instantiate(new_env_ref, gameObject.transform);
        //new_env.transform.position = environmentCurrent.GetComponent<Environment>().GetNextSpawnTransform().position;
        new_env.transform.position = environmentCurrent.transform.position + spawnOffset;
        new_env.GetComponent<Environment>().halfwayPassed += CreateNewEnvironment;
        new_env.GetComponent<Environment>().environmentEntered += RemoveOldEnvironment;
        new_env.GetComponent<Environment>().prevEnvironment = environmentCurrent;
        environmentCurrent.GetComponent<Environment>().nextEnvironment = new_env;
        environmentCurrent = new_env;
    }

    /// <summary>
    /// Destroys previous environment on trigger environmentEntered
    /// </summary>
    void RemoveOldEnvironment()
    {
        Debug.Log("Destroy!");
        var currnet_env = environmentCurrent.GetComponent<Environment>();
        if(currnet_env.prevEnvironment != null)
            Destroy(currnet_env.prevEnvironment);
    }
}

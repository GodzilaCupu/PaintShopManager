using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnner : MonoBehaviour
{
    Spawnner spawnControl;
    
    void Start()
    {
        spawnControl = GameObject.Find("GameManager").GetComponent<Spawnner>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            spawnControl.GetRandomizeTools();
        }
    }
}

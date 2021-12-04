using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] private Transform spawnnerPos;
    [SerializeField] private GameObject[] objSpawn = new GameObject[4];


    // Start is called before the first frame update
    void Start()
    {
        spawnnerPos = GameObject.Find("Spawnner").GetComponent<Transform>();

    }

    public void GetRandomizeTools()
    {
        int indexRandomize = Random.Range(0, objSpawn.Length);

        Instantiate(objSpawn[indexRandomize], spawnnerPos.position, spawnnerPos.rotation);
        Debug.Log("Berhasil Spawn");
    }

}

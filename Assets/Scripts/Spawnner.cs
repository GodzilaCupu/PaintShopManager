using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] private Transform spawnnerPos;
    [SerializeField] private GameObject[] objSpawn = new GameObject[4];

    // kuas = 0; bak = 1; kaleng = 2 ; roller = 3
    [SerializeField] private int[] indexTools = new int[4];


    // Start is called before the first frame update
    void Start()
    {
        spawnnerPos = GameObject.Find("Spawnner").GetComponent<Transform>();

        if (objSpawn != null)
            for (int i = 0; i < objSpawn.Length; i++)
                indexTools[i] = i;
        else
            Debug.LogError("Tools Kosong Silahkan Masukan Dulu");
    }

    public void GetRandomizeTools()
    {
        int indexRandomize = Random.Range(0, objSpawn.Length);

        Instantiate(objSpawn[indexRandomize], spawnnerPos.position, spawnnerPos.rotation);
        Debug.Log("Berhasil Spawn");
    }

}

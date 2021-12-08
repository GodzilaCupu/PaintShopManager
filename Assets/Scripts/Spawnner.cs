using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{

    [Header("Tools")]
    [SerializeField] private Transform tools_spawnnerPos;
    // kuas = 0; bak = 1; kaleng = 2 ; roller = 3
    [SerializeField] private GameObject[] toolsSpawn = new GameObject[4];

    [Header("NPC")]
    [SerializeField] private GameObject[] npcSpawn = new GameObject[2];

    [SerializeField] private Transform[] npc_TargetPos = new Transform[3];

    public int npcDone = 0;

    [Header("Place")]
    private int _checkPos;
    public bool[] isItEmpty = new bool[3];

    [Header("Wave")]
    private float time_count;

    // Start is called before the first frame update
    void Start()
    {
        tools_spawnnerPos = GameObject.Find("Spawnner").GetComponent<Transform>();
        _checkPos = 0;
        time_count = 0;
        for (int i = 0; i < isItEmpty.Length; i++)
            isItEmpty[i] = true;

        GetWaveUpdate(1);
    }

    private void Update()
    {
        time_count += Time.deltaTime;
        if (npcDone == 0)
            GetWaveUpdate(5);
        else if(npcDone > 1 && npcDone <= 10)
            GetWaveUpdate(3);
        else if (npcDone > 10)
            GetWaveUpdate(2);
    }

    public void GetRandomizeTools()
    {
        int indexRandomize = Random.Range(0, toolsSpawn.Length);

        Instantiate(toolsSpawn[indexRandomize], tools_spawnnerPos.position, tools_spawnnerPos.rotation);
        Debug.Log("Berhasil Spawn");

    }

    public void GetRandomizeNPC(int index)
    {
        int indexNPCRandomize = Random.Range(0, npcSpawn.Length);
        Instantiate(npcSpawn[indexNPCRandomize], npc_TargetPos[index].position, npc_TargetPos[index].rotation);
    }

    private void GetCheckPosition(int index)
    {
        if (isItEmpty[index] == true)
        {
            GetRandomizeNPC(index);
            isItEmpty[index] = false;
        }
        if (_checkPos >= 2)
            _checkPos = 0;
    }

    private void GetWaveUpdate(float wave)
    {
        if(time_count >= wave)
        {
            GetCheckPosition(_checkPos);
            time_count = 0f;
            _checkPos++ ;
        }
    }
}

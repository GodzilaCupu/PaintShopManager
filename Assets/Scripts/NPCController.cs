using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Spawnner spawnnManager;
    [SerializeField] private GameManager gameManager;

    [Header("Tools")]
    // kuas = 0; bak = 1; kaleng = 2 ; roller = 3
    [SerializeField] private string[] nameTools = { "Kuas", "Bak", "Kaleng", "Roller" };

    private int toolsIndex;

    [Header("UI")]
    [SerializeField] private Sprite[] spriteTools = new Sprite[4];
    [SerializeField] private Image bgImage;

    private void Start()
    {
        spawnnManager = GameObject.Find("GameManager").GetComponent<Spawnner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetRandomizeTool();
    }

    private void Update()
    {
        Debug.Log(toolsIndex + "Tools Index");
        TimmerLookToCamera();
    }

    private void GetRandomizeTool()
    {
        int indexToolsNameRandomize = Random.Range(0, nameTools.Length);
        toolsIndex = indexToolsNameRandomize;

        bgImage.sprite = spriteTools[indexToolsNameRandomize];
    }

    private void TimmerLookToCamera()
    {
        bgImage.transform.LookAt(Camera.main.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == nameTools[toolsIndex])
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameManager.bonusTime += 30;
            Debug.Log("+25");
            spawnnManager.npcDone++;
            spawnnManager.isItEmpty[toolsIndex] = true;
        }
    }

}

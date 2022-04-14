using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    private GameController GameController;

    [Header ("Prefab of spike")]
    public GameObject spikePrefab;
    public Transform[] spawnPoints;


    [SerializeField]
    private List<GameObject> spikes;

    private int spikeCount = 1;



    void Start()
    {
        GameController = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    void Update() {

        if (GameController.score >= 1 && GameController.score <= 4) 
            spikeCount = 2;
        else if (GameController.score >= 5 && GameController.score <= 10)
            spikeCount = 3;
        else if (GameController.score >= 11 && GameController.score <= 17)
            spikeCount = 4;
        else if (GameController.score >= 18 && GameController.score <= 26)
            spikeCount = 5;
        else if (GameController.score >= 27 && GameController.score <= 35)
            spikeCount = 6; 
    }

    private void CreateSpike()
    {
        GameObject newSpike = Instantiate(spikePrefab, transform);
        newSpike.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        spikes.Add(newSpike);
    }


    public void CreateLine()
    {
        for (int i = 0; i < spikeCount; i++)
            CreateSpike();
    }
 
    public void DestroyLine()
    {
        while(spikes.Count != 0) {
            Destroy(spikes[0]);
            spikes.Remove(spikes[0]);
        }
    }
}

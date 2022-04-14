using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    private GameController SC;

    [Header ("Prefab of spike")]
    public GameObject spikePrefab;
    public Transform[] spawnPoints;


    [SerializeField]
    private List<GameObject> spikes;

    int spikeCount = 1;


    void Start()
    {
         SC = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    void Update() {

        if (SC.score >= 1 && SC.score <= 4) 
            spikeCount = 2;
        else if (SC.score >= 5 && SC.score <= 10)
            spikeCount = 3;
        else if (SC.score >= 11 && SC.score <= 17)
            spikeCount = 4;
        else if (SC.score >= 18 && SC.score <= 26)
            spikeCount = 5;
        else if (SC.score >= 27 && SC.score <= 35)
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

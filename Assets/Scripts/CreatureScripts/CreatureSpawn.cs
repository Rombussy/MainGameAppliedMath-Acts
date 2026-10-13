using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CreatureSpawn : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] float spawnInterval = 4f;
    [SerializeField] int creaturePoolSize = 20;


    private Dictionary<string, List<GameObject>> creaturePool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void creaturePoolInitialize()
    {
       
    }
}

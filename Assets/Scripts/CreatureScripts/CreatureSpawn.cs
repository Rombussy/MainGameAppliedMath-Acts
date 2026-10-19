using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class CreatureSpawn : MonoBehaviour
{

    public GameObject creaturePrefab;
    public Transform targetLocation;
    public Transform[] spawnPoints;
    public int poolSize = 6;
    public float spawnInterval = 3f;
    private List<GameObject> pool = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject creature = Instantiate(creaturePrefab);
            creature.SetActive(false);
            pool.Add(creature);
        }
        StartCoroutine(creatureSpawnRoutine());
    }

    private IEnumerator creatureSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            spawnCreatureIndex(0, CreatureMovement.PathType.Quadratic);

            yield return new WaitForSeconds(spawnInterval);
            spawnCreatureIndex(1, CreatureMovement.PathType.Cubic);


        }
    }

    private void spawnCreatureIndex(int index, CreatureMovement.PathType pathType)
    {
        if (spawnPoints == null || index >= spawnPoints.Length) return;

        GameObject creature = PooledCreature();
        if (creature == null) return;

        creature.transform.position = spawnPoints[index].position;
        creature.SetActive(true);

        CreatureMovement movement = creature.GetComponent<CreatureMovement>();
        if (movement != null)
        {
            movement.Initialize(targetLocation, pathType);
        }

        TurretCone[] coneTurrets = FindObjectsByType<TurretCone>(FindObjectsSortMode.None);
        foreach (var t in coneTurrets) t.SetTarget(creature.transform);

        TurretSniper[] sniperTurrets = FindObjectsByType<TurretSniper>(FindObjectsSortMode.None);
        foreach (var t in sniperTurrets) t.SetTarget(creature.transform);

        TurretShotgun[] shotgunTurrets = FindObjectsByType<TurretShotgun>(FindObjectsSortMode.None);
        foreach (var t in shotgunTurrets) t.SetTarget(creature.transform);
    }

    private GameObject PooledCreature()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) return obj; 
        }

        return null;
    }
}

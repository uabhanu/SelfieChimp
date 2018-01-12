﻿using System.Collections;
using UnityEngine;

public class BananasSpawner : MonoBehaviour
{
    ChimpController m_chimpController;
    GameObject m_collectedTiles;

    [SerializeField] float m_spawnTime;
    [SerializeField] [Tooltip("This is max number of banana prefabs to spawn at once, Ask Bhanu for more info :)")] [Range(0 , 10)] int m_maxBananas; // Unable to use prefabs.Length for some reason
    [SerializeField] GameObject[] m_bananasPrefabs;

    public static int m_bananasCount = 0;

    void Reset()
    {
        m_spawnTime = 0.5f;
    }

    void Start()
    {
        m_chimpController = FindObjectOfType<ChimpController>();
        m_collectedTiles = GameObject.Find("Tiles");
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(m_spawnTime);

        if(m_bananasCount < m_maxBananas)
        {
            GameObject bananas = Instantiate(m_bananasPrefabs[Random.Range(0 , m_bananasPrefabs.Length)] , transform.position , Quaternion.identity);
            bananas.transform.parent = m_collectedTiles.transform.Find("Bananas").transform;
            bananas.transform.position = new Vector2(transform.position.x + Random.Range(0 , 2) , bananas.transform.position.y + Random.Range(-0.5f , 0.5f));
            m_bananasCount++;
        }

        StartCoroutine("SpawnRoutine");
    }
}
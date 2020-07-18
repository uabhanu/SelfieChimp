﻿using UnityEngine;

public class PlatformsGenerator : MonoBehaviour 
{
    private const float PLAYER_DISTANCE_SPAWN_LAND_PART = 20.0f;

    [SerializeField] private Transform _platformEndPosition;
    [SerializeField] private Transform _platformToSpawn;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    private void Awake() 
    {
        _lastEndPosition = _platformEndPosition.transform.position;
    }

    private void Update() 
    {
        if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LAND_PART) 
        {
            // Spawn another level part
            SpawnLandPart();
        }
    }

    private void SpawnLandPart() 
    {
        Transform chosenLandPart = _platformToSpawn;
        Transform lastLandPartTransform = SpawnLandPart(chosenLandPart , _lastEndPosition);
        _lastEndPosition = lastLandPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLandPart(Transform landPart , Vector3 spawnPosition) 
    {
        float randomYPos = Random.Range(spawnPosition.y - 3.5f , spawnPosition.y + 3.5f);
        Transform landPartTransform = Instantiate(landPart , new Vector3(spawnPosition.x , Mathf.Clamp(randomYPos , -4.90f , 0.70f) , spawnPosition.z) , Quaternion.identity);
        return landPartTransform;
    }

}


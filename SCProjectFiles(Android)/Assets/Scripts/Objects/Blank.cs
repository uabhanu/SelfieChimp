﻿using UnityEngine;

public class Blank : MonoBehaviour 
{
    Camera _mainCamera;
    Transform _gameLayer;
    [SerializeField] Vector3 _positionOnScreen;

    void Start()
    {
        _gameLayer = GameObject.Find("GameLayer").transform;
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();    
    }

    void Update() 
    {
		if(Time.timeScale == 0f)
		{
			return;
		}

        _positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        if(_positionOnScreen.x < 600 && transform.IsChildOf(_gameLayer))
        {
            GameManager.JumpTutorial();
        }
    }
}
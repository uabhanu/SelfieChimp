﻿using UnityEngine;
using System.Collections;

public class playerHandler : MonoBehaviour {
	
	private bool inAir = false;

	private int _animState = Animator.StringToHash("animState");
	private Animator _animator;
	public bool jumpPress = false;

	void Start () {
		_animator = this.transform.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(!inAir && Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.y) > 0.05f){

			_animator.SetInteger(_animState,1);
			inAir =true;

		}else if(inAir && this.GetComponent<Rigidbody2D>().velocity.y == 0.00f){

			_animator.SetInteger(_animState,0);
			inAir =false;
			if(jumpPress)jump ();
		}






	}


	public void jump(){
		jumpPress = true;
		if (inAir)return;
		this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * 3000);
		GameObject.Find("Main Camera").GetComponent<playSound>().PlaySound("jump");	
	}


}

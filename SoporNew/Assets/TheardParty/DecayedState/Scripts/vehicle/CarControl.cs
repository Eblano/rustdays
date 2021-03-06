using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CarControl : MonoBehaviour {
	private Animator _animator;
	public bool openDoor = false;
	public bool engineStopped;
	public bool engineStarted;
	public bool lowDoors;

	public AudioClip engineStart;
	public AudioClip engineIdle;
	public AudioClip engineStop;
	public AudioSource startStopEng;

	private GameObject objPlayer;//Player
	private CharacterControl CharCtrlScript;
	// Use this for initialization
	void Start () { 
		_animator = GetComponent<Animator>();
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		CharCtrlScript = (CharacterControl) objPlayer.GetComponent( typeof(CharacterControl) );

	}

	// Update is called once per frame
	void Update () {
		if ((CharCtrlScript.driving) && (CharCtrlScript.currCar == this.gameObject)&&(!engineStarted)) {
			if (!startStopEng.isPlaying){
				StartCoroutine(EngineStart());
			}
		}
		if (engineStarted) {
			if((!CharCtrlScript.driving)&&(!engineStopped)){
				StartCoroutine(EngineStop());
			}
		}
	}
	void FixedUpdate(){
		_animator.SetBool ("Open", openDoor);				
	}
	IEnumerator EngineStart(){
		engineStopped = false;
		engineStarted = true;
		startStopEng.clip = engineStart;
		startStopEng.Play();
		yield return new WaitForSeconds(startStopEng.clip.length);
		startStopEng.Stop();
	}
	IEnumerator EngineStop(){
		engineStopped = true;
		engineStarted = false;
		startStopEng.clip = engineStop;
		startStopEng.Play();
		yield return new WaitForSeconds(startStopEng.clip.length);
		startStopEng.Stop();
		
	}
}


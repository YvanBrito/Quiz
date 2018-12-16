using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	public string data;
	public GameManager gm;

	void Start(){
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	void Update(){
		GetComponent<Button> ().interactable = gm.allowInput;
	}

	public void sendAnswer(){
		gm.VerifyAnswer (data);
	}
}

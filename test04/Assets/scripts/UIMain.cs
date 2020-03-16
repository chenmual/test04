using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickSend() {
		Debug.Log("click1");
		Connector.getConnector().SendMessage(3, 16, 3001, "hellow");
	}

	public void swichState(int state) {
		Resources.Load("prefab/UICalc");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour {

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void next() {
		GameObject go = (GameObject)Resources.Load("prefab/UISelect");
		go = Object.Instantiate(go);

		GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
		go.transform.parent = main.uiparent;
		go.transform.localPosition = new Vector3(0, 0, 0);

		Object.Destroy(this.gameObject);
	}
}

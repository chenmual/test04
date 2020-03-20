using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

	private static GameMain instance;
	public UIMain uimain;
	public Transform uiparent;

	private GameScene scene;

	private string userName;
	private int sessionNum = 0;

	float timeSpend = 0.0f;

	private List<IEventListener> eventManager = new List<IEventListener>();

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.runInBackground = true;
		GameObject go = (GameObject)Resources.Load("prefab/UIStart");
		go = Object.Instantiate(go);

		go.transform.parent = uiparent;
		go.transform.localPosition = new Vector3(0, 0, 0);
		//GameObject.Find("UIParent").transform;
		if(instance == null){
			instance = this;
		}
		if(scene == null){
			scene = new GameScene();
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSpend += Time.deltaTime;
	}

	public static GameMain getInstance(){
		return instance;
	}

	public GameScene getScene(){
		return scene;
	}

	public string getUserName() {
		return userName;
	}

	public void setUserName(string name) {
		this.userName = name;
	}

	public int getSessionNum() {
		return sessionNum;
	}

	public void setSessionNum(int num) {
		this.sessionNum = num;
	}


	public void addListener(IEventListener listener) {
		this.eventManager.Add(listener);
	}

	public void removeListener(IEventListener listener) {
		this.eventManager.Remove(listener);
	}
	public void OnApplicationPause(bool pause) {
		Debug.Log("onpause " + pause);
		for (int i = eventManager.Count - 1; i > -1; i--) {
			eventManager[i].onEvent(EventCode.EVENT_ON_PAUSE, pause);
		}
	}

	public float getCurrentGameTime() {
		return timeSpend;
	}

	public void QuitGame() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}

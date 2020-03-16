using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIRandomGame : MonoBehaviour
{
	const int PANEL_COUNT = 3;
	const int ROW_GAP = 120;
	const float MAX_SPEED = 450f;
	const float DEFAULT_KEEPSECOND_0 = 4f;
	const float DEFAULT_KEEPSECOND_1 = 2f;
	const float DEFAULT_KEEPSECOND_2 = 6f;
	const float DEFAULT_ACCELERATION = 2f;
	const float speedUpFactor = 1.04f;
	const float speedDownFactor = 0.95f;
	const float SPEED_DOWN_KEEP = 150f;
	const float SPEED_CORRECT_KEEP = 50f;

	static float[] DEFAULT_KEEPSECOND = new float[] { DEFAULT_KEEPSECOND_0, DEFAULT_KEEPSECOND_1, DEFAULT_KEEPSECOND_2 };
	static UIState uiState = UIState.STATE_NONE;
	static SubState[] subState = new SubState[PANEL_COUNT];

	public ScrollRect[] panels = new ScrollRect[PANEL_COUNT];

	List<ItemNumber>[] itemLists	= new List<ItemNumber>[PANEL_COUNT];
	int half = 0;

	float[] speed = new float[PANEL_COUNT];
	float[] acceleration = new float[PANEL_COUNT];
	float[] keepSpeedSecond = new float[] { DEFAULT_KEEPSECOND_0, DEFAULT_KEEPSECOND_1, DEFAULT_KEEPSECOND_2 };
	float[] gapSecond = new float[PANEL_COUNT];

	int[] breakIndex = new int[PANEL_COUNT];

	void Start () {
		init();
	}
	
	void Update () {
		switch (uiState) {
			case UIState.STATE_MOVE:
				//Debug.Log("state=" + subState[i]);
				for (int i = 0; i < PANEL_COUNT; i++) {
					switch (subState[i]) {
						case SubState.SUBSTATE_SPEEDUP:
							move(i);
							calcNextSpeed(i);
							break;
						case SubState.SUBSTATE_SPEEDMAX:
							move(i);
							leftTime(i);
							break;
						case SubState.SUBSTATE_SPEEDDOWN:
							move(i);
							calcNextSpeed(i);
							break;
						case SubState.SUBSTATE_CORRECTING:
							move(i);
							calcNextSpeed(i);
							break;
						case SubState.SUBSTATE_STOP:
						default:
							break;
					}
				}
				break;
			default:
				break;
		}
	}

	public void init() {
		for (int i = 0; i < itemLists.Length; i++) {
			itemLists[i] = new List<ItemNumber>();
			for (int j = 0; j < 10; j++) {
				ItemNumber item = loadItem(j.ToString(), i);
				itemLists[i].Add(item);
				this.half = itemLists[i].Count * ROW_GAP >> 1;
			}
			acceleration[i] = DEFAULT_ACCELERATION;
			resize(i);
			//breakIndex[i] = Random.Range(0, 10);
			subState[i] = SubState.SUBSTATE_STOP;
		}
	}

	public ItemNumber loadItem(string str, int panelIndex) {

		GameObject go = (GameObject)Resources.Load("prefab/item/ItemNumber");
		go = Object.Instantiate(go);

		go.transform.parent = panels[panelIndex].content.transform;

		ItemNumber item = go.GetComponent<ItemNumber>();
		item.transform.localPosition = new Vector3(0, calcOffset(panelIndex), 0);
		item.text.text = str;
		return item;
	}

	int calcOffset(int index) {
		int ret = itemLists[index].Count * ROW_GAP;
		return ret;
	}

	void resize(int index) {
		for (int i = 0; i < itemLists[index].Count; i++) {
			if (itemLists[index][i].transform.localPosition.y > this.half) {
				itemLists[index][i].transform.Translate(new Vector3(0, -itemLists[index].Count * ROW_GAP, 0));
			}
		}
	}

	public void move(int index) {
		float deltaTime = Time.deltaTime;
		//for (int i = 0; i < PANEL_COUNT; i++) {
		//}
		for (int j = 0; j < itemLists[index].Count; j++) {
			itemLists[index][j].transform.Translate(new Vector3(0, speed[index] * deltaTime, 0));
			if (itemLists[index][j].transform.localPosition.y > this.half) {
				itemLists[index][j].transform.Translate(new Vector3(0, -(half << 1), 0));
				if (j == breakIndex[index] && speed[index] == SPEED_DOWN_KEEP) {
					subState[index] = SubState.SUBSTATE_CORRECTING;
				}
			}
		}
	}

	public void onClickStart() {
		if (uiState == UIState.STATE_NONE || uiState == UIState.STATE_STOP) {
			for (int i = 0; i < PANEL_COUNT; i++) {
				breakIndex[i] = Random.Range(0, 10);
				speed[i] = 0.1f;
				Debug.Log("[" + i + "]=" + breakIndex[i]);
			}
			uiState = UIState.STATE_MOVE;

			for (int i = 0; i < PANEL_COUNT; i++) {
				subState[i] = SubState.SUBSTATE_SPEEDUP;
			}
		}
	}

	void calcNextSpeed(int index) {
		float deltaTime = Time.deltaTime;
		switch (subState[index]) {
			case SubState.SUBSTATE_SPEEDDOWN:
				if (speed[index] == SPEED_DOWN_KEEP) {
				} else {
					speed[index] = speed[index] * speedDownFactor;
					if (speed[index] < SPEED_DOWN_KEEP) {
						speed[index] = SPEED_DOWN_KEEP;
					}
				}
				break;
			case SubState.SUBSTATE_CORRECTING:
				gapSecond[index] = gapSecond[index] + deltaTime;
				if (gapSecond[index] > 1f) {
					//Debug.Log(itemLists[index][breakIndex[index]].transform.localPosition.y);
					if (speed[index] > SPEED_CORRECT_KEEP) {
						speed[index] = speed[index] * ((itemLists[index][breakIndex[index]].transform.localPosition.y) / -(half));
						if (speed[index] < SPEED_CORRECT_KEEP) {
							speed[index] = SPEED_CORRECT_KEEP;
						}
					}
					gapSecond[index] = 0;
				} else if(speed[index] == 50f) {
					if (Mathf.Abs(itemLists[index][breakIndex[index]].transform.localPosition.y) < 2f) {
						speed[index] = 0;
						float offset = itemLists[index][breakIndex[index]].transform.localPosition.y;
						for (int j = 0; j < itemLists[index].Count; j++) {
							itemLists[index][j].transform.Translate(new Vector3(0, offset, 0));
						}
						subState[index] = SubState.SUBSTATE_STOP;
						bool needStop = true;
						for (int i = 0; i < PANEL_COUNT; i++) {
							if (subState[i] != SubState.SUBSTATE_STOP){
								needStop = false;
							}
						}
						if (needStop) {
							Debug.Log("stop");
							uiState = UIState.STATE_STOP;
						}
					}
				}
				//if (itemLists[index][breakIndex[index]].transform.localPosition.y - 0 < 0.4f) {
				//	speed = 0f;
				//	uiState = UIState.STATE_STOP;
				//	subState = SubState.SUBSTATE_CORRECTING;
				//}
				break;
			case SubState.SUBSTATE_SPEEDUP:
				speed[index] = speed[index] * speedUpFactor + acceleration[index];
				acceleration[index] = acceleration[index] - deltaTime;
				if (acceleration[index] < 0) {
					acceleration[index] = 0;
				}
				if (speed[index] > MAX_SPEED) {
					speed[index] = MAX_SPEED;
					subState[index] = SubState.SUBSTATE_SPEEDMAX;
					acceleration[index] = DEFAULT_ACCELERATION;
					keepSpeedSecond[index] = DEFAULT_KEEPSECOND[index];
				}
				break;
			default:
				break;
		}
	}

	void leftTime(int index) {
		keepSpeedSecond[index] -= Time.deltaTime;
		if (keepSpeedSecond[index] <= 0) {
			subState[index] = SubState.SUBSTATE_SPEEDDOWN;
			keepSpeedSecond[index] = DEFAULT_KEEPSECOND[index];
		}
	}
}

public enum UIState {
	STATE_NONE = 0,
	STATE_STOP = 1,
	STATE_MOVE = 2
}
public enum SubState {
	SUBSTATE_STOP = 0,
	SUBSTATE_SPEEDUP = 1,
	SUBSTATE_SPEEDMAX = 2,
	SUBSTATE_SPEEDDOWN = 3,
	SUBSTATE_CORRECTING = 4,
}


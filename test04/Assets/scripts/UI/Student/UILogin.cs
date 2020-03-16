using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login(){
		GameObject go = (GameObject)Resources.Load("prefab/UIMainMenu");
		go = Object.Instantiate(go);

		GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
		go.transform.parent = main.uiparent;
		go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        go.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

		Object.Destroy(this.gameObject);

        if(dropdown.value == 2){
            GameMain.getInstance().getScene().duty = 2;
        }else if(dropdown.value == 1){
            GameMain.getInstance().getScene().duty = 1;
        }else{
            GameMain.getInstance().getScene().duty = 0;
        }
    }
}

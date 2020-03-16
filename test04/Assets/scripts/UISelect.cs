using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP;

public class UISelect : MonoBehaviour
{

    public InputField input;

    public Button btnFraction;
    public Button btnAsk;

    public Text text;

    private bool isConnection = false;

    // Start is called before the first frame update
    void Start() {
        if (NetUtil.IsCanConnect("http://106.12.55.205:10224/test/find")) {
            new HTTPRequest(new System.Uri("http://106.12.55.205:10224/test/find"), HTTPMethods.Get, OnRequestFinished).Send();
        } else {
            text.text = "联网失败";
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnFractionBtn() {

        GameObject go = (GameObject)Resources.Load("prefab/UICalc");
        go = Object.Instantiate(go);

        GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
        go.transform.parent = main.uiparent;
        go.transform.localPosition = new Vector3(0, 0, 0);

        Object.Destroy(this.gameObject);

    }

    public void OnAskBtn() {
        if (!isConnection) {
            text.text = "请先联网";
            return;
        }

        string uname = input.text;
        if (uname.Trim() == "") {
            text.text = "请先输入名字";
            return;
        }
        GameMain.getInstance().setUserName(uname);
        GameMain.getInstance().setSessionNum(Random.Range(100000, 999999));

        GameObject go = (GameObject)Resources.Load("prefab/UIAsk");
        go = Object.Instantiate(go);

        GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
        go.transform.parent = main.uiparent;
        go.transform.localPosition = new Vector3(0, 0, 0);

        Object.Destroy(this.gameObject);
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response) {
        if (response.IsSuccess) {
            //AskQuestion askQuestion = LitJson.JsonMapper.ToObject<AskQuestion>(response.DataAsText);
            //string res = askQuestion.questionTitle;//.Replace("\\n", "\n");
            //text.text = res;
            isConnection = true;
        } else {
            text.text = "联网失败";
        }
    }
}

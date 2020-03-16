using UnityEngine;
using UnityEngine.UI;
using BestHTTP;
using LitJson;

public class UIAsk : MonoBehaviour, IEventListener
{
    public Button btnNext;
    public Button btnCheck;

    public Text txtIsRight;
    public Text txtCounter;

    public InputField[] inputFields;
    public Text[] txtAnswers;
    public Text txtTitle;

    private AskQuestion askSample;

    private int count;
    private int right = 0;
    private bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        GameMain.getInstance().addListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onNextBtn() {
        for (int i = 0; i < inputFields.Length; i++) {
            txtAnswers[i].gameObject.SetActive(false);
        }
        requestNextQuestion();
    }

    public void requestNextQuestion() {
        HTTPRequest httpRequest = new HTTPRequest(new System.Uri("http://106.12.55.205:10224/calc/getRandomAsk"), HTTPMethods.Get, onNextResponse);
        httpRequest.Send();
    }

    public void onNextResponse(HTTPRequest request, HTTPResponse response) {
        if (response.StatusCode == 200) {
            string json = response.DataAsText;
            askSample = JsonMapper.ToObject<AskQuestion>(json);
            refreshTitle();
        }
    }

    public void onCheckBtn() {
        count++;
        txtIsRight.gameObject.SetActive(true);
        isRight = checkCorrect();
        if (isRight) {
            txtIsRight.text = "正确";
            txtIsRight.color = Color.green;
            right++;
        } else {
            txtIsRight.text = "错误";
            txtIsRight.color = Color.red;
        }
        btnNext.gameObject.SetActive(true);
        btnCheck.gameObject.SetActive(false);

        for (int i = 0; i < txtAnswers.Length; i++) {
            if (i < askSample.answerTitles.Length) {
                txtAnswers[i].text = askSample.answerTitles[i].Replace("%s", askSample.result[i]);
            } else {
                txtAnswers[i].text = "";
            }
        }
        refreshCounter();
        sendRecord();
    }

    public void sendRecord() {
        HTTPRequest httpRequest = new HTTPRequest(new System.Uri("http://106.12.55.205:10224/calc/setAnswer"), HTTPMethods.Post, onRecord);
        httpRequest.AddField("qId", askSample.qId.ToString());
        httpRequest.AddField("isRight", isRight? "1" : "0");
        string answers = "";
        for (int i = 0; i < askSample.result.Length; i++) {
            answers += inputFields[i].text;
            if (i < askSample.result.Length - 1) {
                answers += ",";
            }
        }
        httpRequest.AddField("results", answers);
        httpRequest.AddField("title", askSample.questionTitle);
        httpRequest.AddField("name", GameMain.getInstance().getUserName());
        httpRequest.AddField("sessionNum", GameMain.getInstance().getSessionNum().ToString());
        httpRequest.AddField("mac", NetUtil.GetMacAddress());
        httpRequest.Send();
    }

    public void onRecord(HTTPRequest request, HTTPResponse response) { 
    }

    public void refreshCounter() {
        txtCounter.text = "答对数量/答题总数\n" + right + "/" + count;
    }

    public bool checkCorrect() {
        bool ret = true;
        for (int i = 0; i < askSample.result.Length; i++) {
            if (!askSample.result[i].Trim().Equals(inputFields[i].text.Trim())) {
                ret = false;
            }
        }
        return ret;
    }

    public void refreshTitle() {
        txtIsRight.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        btnCheck.gameObject.SetActive(true);
        btnNext.GetComponentInChildren<Text>().text = "下一题";
        for (int i = 0; i < inputFields.Length; i++) {
            inputFields[i].text = "";
        }
        for (int i = 0; i < txtAnswers.Length; i++) {
            if (i < askSample.answerTitles.Length) {
                txtAnswers[i].text = askSample.answerTitles[i].Replace("%s", "(   )"); ;
                txtAnswers[i].gameObject.SetActive(true);
            } else {
                txtAnswers[i].text = "";
            }
        }
        txtTitle.text = askSample.questionTitle;
    }

    public void onEvent(int eventCode, params object[] objs) {
        switch (eventCode) {
            case EventCode.EVENT_ON_PAUSE:
                GameObject go = (GameObject)Resources.Load("prefab/UISelect");
                go = Object.Instantiate(go);
                GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
                go.transform.parent = main.uiparent;
                go.transform.localPosition = new Vector3(0, 0, 0);
                Object.Destroy(this.gameObject);
                break;
        }
    }

    public void OnDestroy() {
        GameMain.getInstance().removeListener(this);
    }
}

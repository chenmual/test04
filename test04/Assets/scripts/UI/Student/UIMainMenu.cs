using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Text text;
    public Dropdown baseDrop;
    public Dropdown examDrop;
    public Dropdown paperDrop;

    UIBaseParent currentBase;

    // Start is called before the first frame update
    void Start()
    {
        List<Dropdown.OptionData> baseList = new List<Dropdown.OptionData>();
        List<Dropdown.OptionData> examList = new List<Dropdown.OptionData>();
        List<Dropdown.OptionData> paperList = new List<Dropdown.OptionData>();

        if (GameMain.getInstance().getScene().duty == 1)
        {
            this.text.text = "当前身份:老师";
            baseList.Add(new Dropdown.OptionData("考试类型"));
            baseList.Add(new Dropdown.OptionData("老师"));
            baseList.Add(new Dropdown.OptionData("班级"));
            baseList.Add(new Dropdown.OptionData("学生"));
            baseDrop.AddOptions(baseList);
            openBase("UIExamType");
            
            paperList.Add(new Dropdown.OptionData("扫卷"));
            paperList.Add(new Dropdown.OptionData("校对"));
            paperList.Add(new Dropdown.OptionData("成绩列表"));
            paperList.Add(new Dropdown.OptionData("其他统计"));
        }
        else if (GameMain.getInstance().getScene().duty == 2)
        {
            this.text.text = "当前身份:管理员";
            baseList.Add(new Dropdown.OptionData("科目"));
            baseList.Add(new Dropdown.OptionData("年级"));
            baseList.Add(new Dropdown.OptionData("学校"));
            baseList.Add(new Dropdown.OptionData("知识点"));
            baseList.Add(new Dropdown.OptionData("考试类型"));
            baseList.Add(new Dropdown.OptionData("老师"));
            baseList.Add(new Dropdown.OptionData("班级"));
            baseList.Add(new Dropdown.OptionData("学生"));
            baseDrop.AddOptions(baseList);
            openBase("UISubject");

            paperList.Add(new Dropdown.OptionData("扫卷"));
            paperList.Add(new Dropdown.OptionData("校对"));
            paperList.Add(new Dropdown.OptionData("成绩列表"));
            paperList.Add(new Dropdown.OptionData("其他统计"));
        }
        else
        {
            this.text.text = "当前身份:学生";
            baseDrop.gameObject.SetActive(false);
            
            examList.Add(new Dropdown.OptionData("答题卡模版"));
            examList.Add(new Dropdown.OptionData("单科考试"));
            examList.Add(new Dropdown.OptionData("综合考试"));

            paperList.Add(new Dropdown.OptionData("查询成绩"));
        }
        examList.Add(new Dropdown.OptionData("答题卡模版"));
        examList.Add(new Dropdown.OptionData("单科考试"));
        examList.Add(new Dropdown.OptionData("综合考试"));
        examDrop.AddOptions(examList);

        paperDrop.AddOptions(paperList);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void logout()
    {
        GameObject go = (GameObject)Resources.Load("prefab/UILogin");
        go = Object.Instantiate(go);

        GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
        go.transform.parent = main.uiparent;
        go.transform.localPosition = new Vector3(0, 0, 0);

        if (currentBase != null)
        {
            Object.Destroy(currentBase.gameObject);
        }
        Object.DestroyImmediate(this.gameObject);
    }

    public void onBaseChange()
    {
        switch (baseDrop.options[baseDrop.value].text)
        {
            case "科目":
                openBase("UISubject");
                break;
            case "年级":
                openBase("UIGrade");
                break;
            case "学校":
                openBase("UISchool");
                break;
            case "知识点":
                openBase("UIKnowledge");
                break;
            case "考试类型":
                openBase("UIExamType");
                break;
            case "老师":
                openBase("UITeacher");
                break;
            case "班级":
                openBase("UIClass");
                break;
            case "学生":
                break;
        }
    }

    void openBase(string name)
    {
        if(currentBase != null){
            Object.Destroy(currentBase.gameObject);
        }

        GameObject go = (GameObject)Resources.Load("prefab/" + name);
        go = Object.Instantiate(go);

        GameMain main = GameObject.Find("GameMain").transform.GetComponent<GameMain>();
        go.transform.parent = main.uiparent;
        go.transform.localPosition = new Vector3(0, 0, 0);
        currentBase = go.GetComponent<UIBaseParent>();
    }
}

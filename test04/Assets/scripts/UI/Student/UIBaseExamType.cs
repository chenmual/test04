using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseExamType : UIBaseParent
{
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        addExamType("月考");
        addExamType("周测");
        addExamType("期中考试");
        addExamType("期末考试");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addExamType(string examTypeName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseExamType");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseExamType item = go.GetComponent<ItemBaseExamType>();
        item.examTypeName.text = examTypeName;
    }

    public void onNewExamType(){
        GameObject go = (GameObject)Resources.Load("prefab/UIExamTypeNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseExamTypeNew component = go.GetComponent<UIBaseExamTypeNew>();
        component.parent = this;
    }
}

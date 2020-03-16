using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseGrade : UIBaseParent
{
    public RectTransform content;

    int itemCount;
    void Start()
    {
        itemCount = 0;
        addGrade("小学一年级");
        addGrade("小学二年级");
        addGrade("高中三年级");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGrade(string gradeName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseGrade");
        go = Object.Instantiate(go);

        go.transform.parent = content;
        // go.transform.localPosition = new Vector3(0, -20 - itemCount * 40, 0);

        ItemBaseGrade item = go.GetComponent<ItemBaseGrade>();
        item.gradeName.text = gradeName;

        itemCount++;
    }

    public void onAddGrade(){
        GameObject go = (GameObject)Resources.Load("prefab/UIGradeNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseGradeNew newSubjectComponent = go.GetComponent<UIBaseGradeNew>();
        newSubjectComponent.parent = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseSubject : UIBaseParent
{
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        addSubject("数学", "");
        addSubject("物理", "理综");
        addSubject("化学", "理综");
        addSubject("历史", "文综");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSubject(string subjectName, string subjectParentName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseSubject");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseSubject item = go.GetComponent<ItemBaseSubject>();
        item.subjectName.text = subjectName;
        item.subjectParentName.text = subjectParentName;
    }

    public void onNewSubject(){
        GameObject go = (GameObject)Resources.Load("prefab/UISubjectNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseSubjectNew component = go.GetComponent<UIBaseSubjectNew>();
        component.parent = this;
    }
}

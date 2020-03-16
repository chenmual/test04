using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseSchool : UIBaseParent
{
    public RectTransform content;
    
    // Start is called before the first frame update
    void Start()
    {
        addSchool("陈经纶中学");
        addSchool("芳草地小学");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSchool(string schoolName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseSchool");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseSchool item = go.GetComponent<ItemBaseSchool>();
        item.schoolName.text = schoolName;
    }

    public void onNewSchool(){
        GameObject go = (GameObject)Resources.Load("prefab/UISchoolNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseSchoolNew newComponent = go.GetComponent<UIBaseSchoolNew>();
        newComponent.parent = this;
    }
}

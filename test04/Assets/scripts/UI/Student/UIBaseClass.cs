using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseClass : UIBaseParent
{
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addClass(string className, string classNumber, string schoolName, string gradeName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseClass");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseClass item = go.GetComponent<ItemBaseClass>();
        item.className.text = className;
        item.classNumber.text = classNumber;
        item.schoolName.text = schoolName;
        item.gradeName.text = gradeName;
    }

    public void onNewClass(){
        GameObject go = (GameObject)Resources.Load("prefab/UIClassNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseClassNew component = go.GetComponent<UIBaseClassNew>();
        component.parent = this;
    }
}

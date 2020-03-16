using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseTeacher : UIBaseParent
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

    public void addTeacher(string teacherName, string schoolName, string subjectName){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseTeacher");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseTeacher item = go.GetComponent<ItemBaseTeacher>();
        item.teacherName.text = teacherName;
        item.schoolName.text = schoolName;
        item.subjectName.text = subjectName;
        item.markingCount.text = Mathf.FloorToInt(Random.Range(0, 100f)).ToString();
    }

    public void onNewTeacher(){
        GameObject go = (GameObject)Resources.Load("prefab/UITeacherNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseTeacherNew component = go.GetComponent<UIBaseTeacherNew>();
        component.parent = this;
    }
}

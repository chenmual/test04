using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseKnowledge : UIBaseParent
{
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        addKnowledge("知识点");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKnowledge(string knowledgeName)
    {
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseKnowledge");
        go = Object.Instantiate(go);

        go.transform.parent = content;

        ItemBaseKnowledge item = go.GetComponent<ItemBaseKnowledge>();
        item.knowledgeName.text = knowledgeName;
    }

    public void onNewSubject(){
        GameObject go = (GameObject)Resources.Load("prefab/UIKnowledgeNew");
        go = Object.Instantiate(go);

		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0, 0, 0);

        UIBaseKnowledgeNew component = go.GetComponent<UIBaseKnowledgeNew>();
        component.parent = this;
    }
}

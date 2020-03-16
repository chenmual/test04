using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBaseKnowledgeNode : MonoBehaviour
{
    public ItemBaseKnowledgeNode parent;

    public Text text;

    public List<ItemBaseKnowledgeNode> children = new List<ItemBaseKnowledgeNode>();
    public int listIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemBaseKnowledgeNode addChild(string name){
        GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseKnowledgeNode");
        go = Object.Instantiate(go);
        go.transform.parent = this.transform;
        float left = 25f;
        float top = 0f;
        go.transform.localPosition = new Vector3(left, top, 0);
		ItemBaseKnowledgeNode newNode = go.GetComponent<ItemBaseKnowledgeNode>();
        newNode.text.text = name;
        children.Add(newNode);
        return newNode;
    }

    public void addChild(){
        addChild("新增知识点");
		UIBaseKnowledgeNew[] uIBaseKnowledgeNew = this.GetComponentsInParent<UIBaseKnowledgeNew>();
        if(uIBaseKnowledgeNew.Length > 0){
            uIBaseKnowledgeNew[0].reset();
        }
    }
}

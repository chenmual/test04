using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBaseKnowledgeNew : MonoBehaviour
{
    public InputField knowledgeName;

    public UIBaseKnowledge parent;

    public RectTransform content;

    public ItemBaseKnowledgeNode rootNode;

    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        addNode(null, "知识点");
        ItemBaseKnowledgeNode node1 = addNode(rootNode, "算法与框图");
		ItemBaseKnowledgeNode node11 = node1.addChild("算法初步");
        node11.addChild("算法与程序框图");
        node11.addChild("基本算法语句");
        node1.addChild("框图");
        ItemBaseKnowledgeNode node2 = addNode(rootNode, "平面解析几何");
        node2.addChild("直线与方程");
        node2.addChild("圆与方程");
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOk(){
        parent.addKnowledge(knowledgeName.text);
        Object.Destroy(this.gameObject);
    }

    public void onBack(){
        Object.Destroy(this.gameObject);
    }

    public ItemBaseKnowledgeNode addNode(ItemBaseKnowledgeNode parent, string name){
        if(parent == null){
            GameObject go = (GameObject)Resources.Load("prefab/item/UIItemBaseKnowledgeNode");
            go = Object.Instantiate(go);
            go.transform.parent = content.transform;
            float left = 25f;
            float top = -15f;
            go.transform.localPosition = new Vector3(left, top, 0);
            rootNode = go.GetComponent<ItemBaseKnowledgeNode>();
            rootNode.text.text = name;
            return rootNode;
        }else{
            return parent.addChild(name);
        }
    }

    public void reset(){
        int count = 0;
        resetChild(rootNode, count);
    }

    public int resetChild(ItemBaseKnowledgeNode parent, int count){
        for(int i = 0; i < parent.children.Count; i++){
            count++;
			ItemBaseKnowledgeNode child = parent.children[i];
            child.listIndex = count;
            // int last = count - 1;
            // if(i > 0){
            //     last = parent.children[i - 1].listIndex;
            // }
			int delta = count - parent.listIndex;
            child.name = "node" + count;
            child.transform.localPosition = new Vector3(child.transform.localPosition.x, -delta * 30f, 0);
            count = resetChild(child, count);
        }
        return count;
    }
}

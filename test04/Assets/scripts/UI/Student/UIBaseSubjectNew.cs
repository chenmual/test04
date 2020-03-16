using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseSubjectNew : MonoBehaviour
{
    public InputField subjectName;
    public Dropdown dropdown;

    public UIBaseSubject parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOk(){
        parent.addSubject(subjectName.text, dropdown.value == 0? "" : dropdown.captionText.text);
        Object.Destroy(this.gameObject);
    }
    public void onBack(){
        Object.Destroy(this.gameObject);
    }
}

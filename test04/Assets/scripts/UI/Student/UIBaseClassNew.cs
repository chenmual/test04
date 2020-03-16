using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseClassNew : MonoBehaviour
{
    public InputField className;
    public InputField classNumber;
    public Dropdown schoolName;
    public Dropdown gradeName;
    public UIBaseClass parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOk(){
        parent.addClass(className.text, classNumber.text, schoolName.captionText.text, gradeName.captionText.text);
        Object.Destroy(this.gameObject);
    }
    public void onBack(){
        Object.Destroy(this.gameObject);
    }
}

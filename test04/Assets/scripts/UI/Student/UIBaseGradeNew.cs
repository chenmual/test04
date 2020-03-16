using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseGradeNew : MonoBehaviour
{
    public InputField gradeName;

    public UIBaseGrade parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOK(){
        parent.addGrade(gradeName.text);
        Object.Destroy(this.gameObject);
    }

    public void onBack(){
        Object.Destroy(this.gameObject);
    }
}

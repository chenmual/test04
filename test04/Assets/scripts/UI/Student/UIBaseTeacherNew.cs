using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseTeacherNew : MonoBehaviour
{

    public InputField teacherName;

    public Dropdown schoolName;

    public Dropdown subjectName;

    public UIBaseTeacher parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOk(){
        parent.addTeacher(teacherName.text, schoolName.captionText.text, subjectName.captionText.text);
        Object.Destroy(this.gameObject);
    }
    public void onBack(){
        Object.Destroy(this.gameObject);
    }
}

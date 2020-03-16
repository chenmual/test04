using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseSchoolNew : MonoBehaviour
{
    public InputField schoolName;

    public UIBaseSchool parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOk(){
        parent.addSchool(schoolName.text);
        Object.Destroy(this.gameObject);
    }

    public void onBack(){
        Object.Destroy(this.gameObject);
    }
}

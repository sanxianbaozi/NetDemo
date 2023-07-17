using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Button btnA;
    public Button btnB;

    private LoginReguest loginReguest;

    private void Start()
    {
        btnA.onClick.AddListener(ClickBtnA);
        btnB.onClick.AddListener(ClickBtnB);
        loginReguest = this.GetComponent<LoginReguest>();
    }


    private void ClickBtnA()
    {
        Debug.Log("======A");
        loginReguest.SendRequest("myc", "960922");
    }

    private void ClickBtnB()
    {
        Debug.Log("======B");
    }
}

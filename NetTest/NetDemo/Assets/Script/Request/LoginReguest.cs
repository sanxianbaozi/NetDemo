using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginReguest : BaseRequest
{
    // Start is called before the first frame update
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        base.Awake();
    }

    void Start()
    {
        
    }

    public void SendRequest(string name,string password)
    {
        string data = name + "," + password;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        Debug.Log("登陆成功！");
    }
}

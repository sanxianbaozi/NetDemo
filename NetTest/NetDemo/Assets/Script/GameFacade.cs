using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _instancel;
    public static GameFacade Instance { get { return _instancel; } }

    private RequestManger requestMgr;
    private GameManger gameMgr;
    private ClientManager client;

    private void Awake()
    {
        if (_instancel != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instancel = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        requestMgr = new RequestManger(this);
        gameMgr = new GameManger(this);
        client = new ClientManager(this);
        client.OnInit();
        gameMgr.OnInit();
        requestMgr.OnInit();
    }

    private void DestroyMgr()
    {
        client.OnDestroy();
        gameMgr.OnDestroy();
        requestMgr.OnDestroy();
    }

    private void OnDestroy()
    {
        DestroyMgr();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMgr.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestMgr.RemoveRequest(actionCode);
    }

    public void HandleRequest(ActionCode actionCode, string data)
    {
        requestMgr.HandleRequest(actionCode, data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        client.SendRequest(requestCode, actionCode, data);
    }
}

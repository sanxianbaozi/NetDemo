using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManger : BaseManager
{
    public RequestManger(GameFacade facade) : base(facade) { }

    private Dictionary<ActionCode, BaseRequest> requestDic = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDic.Add(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDic.Remove(actionCode);
    }

    public void HandleRequest(ActionCode actionCode, string data)
    {
        BaseRequest request = requestDic.TryGet<ActionCode, BaseRequest>(actionCode);
        if(request == null)
        {
            Debug.LogWarning("没有["+ actionCode + "]ActionCode");
            return;
        }
        request.OnResponse(data);
    }
}

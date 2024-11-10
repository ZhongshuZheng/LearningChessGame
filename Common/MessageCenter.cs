using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// message center
/// </summary>
public class MessageCenter 
{
    private Dictionary<string, Action<object>> msgDic; // message dict
    private Dictionary<string, Action<object>> tempMsgDic; // temp message dict, called once and removed
    private Dictionary<object, Dictionary<string, Action<object>>> objMsgDic; // message dict for specific Object

    public MessageCenter() {
        msgDic = new Dictionary<string, Action<object>>();
        tempMsgDic = new Dictionary<string, Action<object>>();
        objMsgDic = new Dictionary<object, Dictionary<string, Action<object>>>();
    }


    // msgDic functions ---------------------------------------------------------------
    public void AddEvent(string eventName, Action<object> callback) {
        _register(msgDic, eventName, callback);
    }

    public void RemoveEvent(string eventName, Action<object> callback) {
        _unregister(msgDic, eventName, callback);
    }

    public void PostEvent(string eventName, object arg=null) {
        _postevent(msgDic, eventName, arg);
    }


    // tempDic functions -------------------------------------------------------------
    public void AddTempEvent(string eventName, Action<object> callback) {
        _register(tempMsgDic, eventName, callback);
    }

    public void PostTempEvent(string eventName, object arg=null) {
        _postevent(tempMsgDic, eventName, arg);
        if (tempMsgDic.ContainsKey(eventName)) {
            tempMsgDic[eventName] = null;
            tempMsgDic.Remove(eventName);
        }
    } 


    // ObjMsgDic functions ------------------------------------------------------------
    public void AddEvent(object listenerObj, string eventName, Action<object> callback) {
        if (!objMsgDic.ContainsKey(listenerObj)) {
            Dictionary<string, Action<object>> _tmpDic = new Dictionary<string, Action<object>>();
            objMsgDic.Add(listenerObj, _tmpDic);
        }
        _register(objMsgDic[listenerObj], eventName, callback);
    }

    public void RemoveEvent(object listenerObj, string eventName, Action<object> callback) {
        if (!objMsgDic.ContainsKey(listenerObj)) {
            return;
        }
        _unregister(objMsgDic[listenerObj], eventName, callback);
        if (objMsgDic[listenerObj].Count == 0) {
            objMsgDic.Remove(listenerObj);
        }
    }

    public void PostEvent(object listenerObj, string eventName, Action<object> callback) {
        if (!objMsgDic.ContainsKey(listenerObj)) {
            return ;
        }
        _postevent(objMsgDic[listenerObj], eventName, callback);
    }


    // Inner functions ----------------------------------------------------------------
    private void _register(Dictionary<string, Action<object>> dic, string eventName, Action<object> callback) {
        if (dic.ContainsKey(eventName)) {
            dic[eventName] += callback;
        } else {
            dic.Add(eventName, callback);
        }
    }

    private void _unregister(Dictionary<string, Action<object>> dic, string eventName, Action<object> callback) {
        if (dic.ContainsKey(eventName)) {
            dic[eventName] -= callback;
        }
        if (dic[eventName] == null) {
            dic.Remove(eventName);
        }
    }

    private void _postevent(Dictionary<string, Action<object>> dic, string eventName, object arg=null) {
        if (dic.ContainsKey(eventName)) {
            dic[eventName].Invoke(arg);
        } else {
            Debug.LogWarning($"MessageCenter: event {eventName} do not exist");
        }
    }

}

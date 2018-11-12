using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CrossSceneInformation {
    public static string Logged_In_Username;
    public static string SessionID;
    public static string IP_Address;
    public static string createSessionID() {
        Guid g = Guid.NewGuid();
        string GuidString = Convert.ToBase64String(g.ToByteArray());
        Debug.Log(GuidString);
        GuidString = GuidString.Replace("=", "");
        GuidString = GuidString.Replace("+", "");
        Debug.Log(GuidString);
        return GuidString;
    }
    public static void setSessionID(string _Input) {
        SessionID = _Input;
    }
}

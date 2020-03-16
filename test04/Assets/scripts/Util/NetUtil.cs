using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetUtil
{
    public static bool IsCanConnect(string url) {
        HttpWebResponse res = null;
        bool CanCn = true;
        try {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            res = (HttpWebResponse)req.GetResponse();
        } catch (Exception) {
            CanCn = false;
        } finally {
            if (res != null) {
                res.Close();
            }
        }
        return CanCn;
    }
}

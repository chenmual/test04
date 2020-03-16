using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
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

    public static string GetMacAddress() {
        string physicalAddress = "";

        NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adaper in nice) {

            Debug.Log(adaper.Description);

            if (adaper.Description == "en0") {
                physicalAddress = adaper.GetPhysicalAddress().ToString();
                break;
            } else {
                physicalAddress = adaper.GetPhysicalAddress().ToString();

                if (physicalAddress != "") {
                    break;
                };
            }
        }

        return physicalAddress;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityJSConnector : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityPluginRequestJs();

    public void RequestJs()
    {
        UnityPluginRequestJs();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public GameObject GoUnityScript;
    public Image Image;

    public void Click()
    {
        Image.color = Color.yellow;
        GoUnityScript.GetComponent<UnityScript>().RequestJs();
    }

    public void ResponseFromJsOk()
    {
        Image.color = Color.green;
    }

    public void ResponseFromJsError()
    {
        Image.color = Color.red;
    }
}

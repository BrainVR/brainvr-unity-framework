using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InfoSenderREST : MonoBehaviour
{
    public string url;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3)) SendTestInfo();
    }

    public void SendTestInfo()
    {
        var test = new Dictionary<string, string> {{"key", "value"}, {"secondKey", "secondValue"}};
        StartCoroutine(SendPOST(test));
    }


    IEnumerator SendPOST(Dictionary<string, string> dict)
    {
        var form = new WWWForm();
        foreach (var di in dict)
        {
            form.AddField(di.Key, di.Value);
        }
        // Upload to a cgi script
        var w = UnityWebRequest.Post(url, form);
        yield return w.Send();

        if (w.isNetworkError || w.isHttpError) print(w.error);
        else print("Finished Uploading Screenshot");
    }
}

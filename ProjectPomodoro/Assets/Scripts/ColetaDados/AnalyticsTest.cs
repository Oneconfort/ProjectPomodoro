using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using UnityEditor;
using UnityEngine;

public class AnalyticsTest : MonoBehaviour
{
   public List<AnalyticsData> data;
    public static AnalyticsTest instance {get; private set;}
    private void Awake()
    {
        instance = this;
        data = new List<AnalyticsData>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Save();
        }
    }
    public void AddAnalytics(string sender, string track, string value)
    {
        AnalyticsData d = new AnalyticsData(Time.time, sender, track, value);
        Debug.Log("Send:" + d.sender + ", Track:" + d.track + ", Value" + d.value );
        data.Add(d);
    }
    public void Save()
    {
        AnalyticsFile f = new AnalyticsFile();
        f.data = data.ToArray();
        string json =  JsonUtility.ToJson(f, true);
        SaveFile(json);
        //SendEmail(json);
    }
    void SaveFile(string text)
    {
        string path = Application.dataPath + "/Analytics.txt";
        Debug.Log("Arquivo salvo em:"+ path);
        File.WriteAllText(path, text);
    }
    void SendEmail(string text)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("laysbassottollg@gmail.com", "wtyo ikgp hgvz phfo"), // gerar essa senha no App do google
            EnableSsl = true
        };
        client.Send("laysbassottollg@gmail.com", "laysbassottollg@gmail.com", "Teste", text);
        print("Email enviado");
    }
}

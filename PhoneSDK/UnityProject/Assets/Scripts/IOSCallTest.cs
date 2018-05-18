using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOSCallTest : MonoBehaviour
{

    bool querying = false;
    string label = "";
    string status = "";
    float centerx = Screen.width / 2;
    float centery = Screen.height / 2;
    string[] services = new string[0];

    // Default service name. _daap._tcp corresponds to iTunes music sharing service
    string service = "_daap._tcp";
    GUIStyle labelStyle = new GUIStyle();


    void Start()
    {
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.normal.textColor = Color.white;
    }


    void OnGUI()
    {
        GUI.Label(new Rect(centerx - 50, 25, 100, 25), "Bonjour client", labelStyle);
        service = GUI.TextField(new Rect(centerx - 125, 50, 175, 25), service);

        if (!querying && GUI.Button(new Rect(centerx + 50, 50, 75, 25), "Query"))
        {
            // Start lookup for specified service inside "local" domain
            Bonjour.StartLookup(service, "local");
            querying = true;
            status = "";
        }

        if (querying)
        {
            // Query status only every 10th frame. Managed -> Native calls are quite expensive.
            // Similar coding pattern could be considered as good practice. 
            if (Time.frameCount % 10 == 0)
            {
                status = Bonjour.GetLookupStatus();
                services = Bonjour.GetServiceNames();
                label = status;
            }

            if (status == "Done")
                querying = false;

            // Stop lookup	
            if (querying && GUI.Button(new Rect(centerx + 50, 50, 75, 25), "Stop"))
                Bonjour.StopLookup();
        }

        // Display status
        GUI.Label(new Rect(centerx - 50, 75, 100, 25), label, labelStyle);
		for (int i = 0; i < services.Length; i++)
		{
			GUI.Button(new Rect(centerx - 75, 100 + i * 25, 150, 25), services[i]);
		}
    }
}

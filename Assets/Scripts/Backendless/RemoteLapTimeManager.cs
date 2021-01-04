using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BestLapTimeGet
{
    //public int created;
    public double BestTime;
    //public int update;
    public string code;
    //public string owner;
    public string message;
}

public class BestLapTimePut
{
    public double BestTime;
    public string objectId;
}

public class RemoteLapTimeManager : MonoBehaviour
{

    public static RemoteLapTimeManager Instance { get; private set; }

    void Awake()
    {
        // force singleton instance
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        // don't destroy this object when we load scene
        DontDestroyOnLoad(gameObject);
    }

    // TODO #1 - change the signature to be a Coroutine, add callback parameter with int parameter
    public IEnumerator GetLapTimeCR(Action<double> onCompleteCallback)
    {
        // TODO #2 - construct the url for our request, including objectid!
        string url = "https://api.backendless.com/A8BEC3A8-D042-8881-FF9A-48246EEECB00/1C1CA0AA-0652-4C50-AA2A-D18F014349FE/data/BestLapTime/2E9A3357-E27F-1027-FF1E-BF92ACDD4400"; // you need to add your OWN object id here!!

        // TODO #3 - create a GET UnityWebRequest, passing in our URL
        UnityWebRequest webreq = UnityWebRequest.Get(url);

        // TODO #4 - set the request headers as dictated by the backendless documentation (3 headers)
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #5 - Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.SendWebRequest();

        // TODO #6 - check for webrequest errors
        if (webreq.isNetworkError)
        {
            Debug.Log(webreq.error);
        }
        else
        {
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            BestLapTimeGet highScoreData = JsonUtility.FromJson<BestLapTimeGet>(webreq.downloadHandler.text);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code))
            {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
            else // TODO #9 - call the callback function, passing the score as the parameter
            {
                onCompleteCallback(highScoreData.BestTime);
            }
        }
    }

    // TODO #1 - change the signature to be a Coroutine, add callback parameter
    public IEnumerator SetLapTimeCR(double score, Action onCompleteCallback)
    {
        // TODO #2 - construct the url for our request, including objectid!
        string url = "https://api.backendless.com/A8BEC3A8-D042-8881-FF9A-48246EEECB00/1C1CA0AA-0652-4C50-AA2A-D18F014349FE/data/BestLapTime/2E9A3357-E27F-1027-FF1E-BF92ACDD4400"; // you need to add your OWN object id here!!

        // TODO #3 - construct JSON string for data we want to send
        string data = JsonUtility.ToJson(new BestLapTimePut { BestTime = score, objectId = Globals.OBJ_ID });

        // TODO #4 - create PUT UnityWebRequest passing our url and data
        UnityWebRequest webreq = UnityWebRequest.Put(url, data);

        // TODO #5 set the request headers as dictated by the backendless documentation (4 headers)
        webreq.SetRequestHeader("Content-Type", "application/json");
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #6 - Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.SendWebRequest();

        // TODO #7 - check for webrequest errors
        if (webreq.isNetworkError)
        {
            Debug.Log(webreq.error);
        }
        else
        {
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            BestLapTimeGet highScoreData = JsonUtility.FromJson<BestLapTimeGet>(webreq.downloadHandler.text);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code))
            {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
            else // TODO #9 - call the callback function to signal success
            {
                onCompleteCallback();
            }
        }
    }

}

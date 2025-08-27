using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HeaderApiLecture : MonoBehaviour
{

    //[Header("Health Parameters")]
    //public Text UserName;
    //[Header("Health Parameters2")]
    // public Text passWord;

    public GameObject listPanel, addPanel;

    public GameObject itemPrefab;
    public Transform layoutTransform;

    public InputField nameTextField;
    public InputField ScoreTextField;
    public Text submitButtonText;

    private LeaderBoard editData = null;

    const string token = "Cd3yTqx43QtGPMuv";
    const string url = "https://myapigenerator.onrender.com/api/leaderboard";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(fetchHeaderApiData());
    }

    private void OnEnable()
    {
        
    }

    private IEnumerator fetchHeaderApiData()
    {
        print("fetchHeaderApiData :::::::::::: ");
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", token);
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);
        //print("request.downloadHandler.text ::::: " + request.downloadHandler.text);

        showPanel("list");

        LeaderBoardData leaderBoardData = JsonUtility.FromJson<LeaderBoardData>(request.downloadHandler.text);
        //print("leaderBoardData.Status ::: " + leaderBoardData.Status);

        foreach (Transform child in layoutTransform)  // delete duplicate data
        {
            Destroy(child.gameObject);
        }

        foreach (var leaderBoard in leaderBoardData.Data)
        {
            //print(leaderBoard.name + " -> " + leaderBoard.score);
            var clone = Instantiate(itemPrefab, layoutTransform);
            var name = clone.transform.GetChild(0).GetComponent<Text>();
            var score = clone.transform.GetChild(1).GetComponent<Text>();
            var editButton = clone.transform.GetChild(2).GetComponent<Button>();
            var deleteButton = clone.transform.GetChild(3).GetComponent<Button>();
            name.text = leaderBoard.name;
            score.text = leaderBoard.score.ToString();
            editButton.onClick.AddListener(() =>
            {
                showPanel("add");
                editData = leaderBoard;
                if (editData != null)
                {
                    nameTextField.text = leaderBoard.name;
                    ScoreTextField.text = leaderBoard.score.ToString();
                }
            });
        }
    }

    // list_panel
    public void showAddPenel()
    {
        showPanel("add");
    }

    private IEnumerator addUserToLeaderBoard(string name, int score)
    {
        //print("name : " + name + "  ->  " + "score : " + score);

        string bodyJsonString = "{\"name\": \"" + name + "\",\"score\": " + score + "}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", token);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);
        print("request.downloadHandler.text ::::: " + request.downloadHandler.text);

        StartCoroutine(fetchHeaderApiData());
    }

    public void submitButton()
    {
        if (nameTextField.text == "")
        {
            print("please enter name");
            return;
        }
        string name = nameTextField.text;
        int score = int.Parse(ScoreTextField.text);
        if (editData != null)
        {
            StartCoroutine(updateUser(name, score, editData._id));
            editData = null;
        }
        else
        {
            StartCoroutine(addUserToLeaderBoard(name, score));
        }
    }

    public IEnumerator updateUser(string name, int score, string id)
    {
        //print("name : " + name + "  ->  " + "score : " + score);

        string bodyJsonString = "{\"name\": \"" + name + "\",\"score\": " + score + "}";

        UnityWebRequest request = new UnityWebRequest(url+"/"+id, "PATCH");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", token);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);
        print("request.downloadHandler.text ::::: " + request.downloadHandler.text);
        
        StartCoroutine(fetchHeaderApiData());
    }


    public void showPanel(string panelName)
    {
        listPanel.SetActive(panelName == "list");
        addPanel.SetActive(panelName == "add");
        submitButtonText.text = editData == null ? "Add" : "Update";
    }
}

class LeaderBoardData
{
    public string Status;
    public string Message;
    public List<LeaderBoard> Data;
}

[Serializable]
class LeaderBoard
{
    public string _id;
    public string name;
    public int score;
}

/*
    normal -> get API
    header -> get API with header

    CRUD
    C -> create
    R -> read
    U -> update
    D -> delete

 */

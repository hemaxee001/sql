using System.Collections.Generic;
using System.Text;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;

public class HeaderApi : MonoBehaviour
{
   
    public List<Board> datalist = new List<Board>();

    const string tocken = "xAb2na7db5pkPrMj";
    const string url = "https://myapigenerator.onrender.com/api/databse";

    public InputField UserName;
    public InputField passWord;

    public GameObject firstPanel;
    public GameObject secondPanel,thirdPanel;
   
    public Text nameText;
    public Text scoreText;

    public static HeaderApi instance;
    public gridLayout gridLayout;

    public InputField updateName;
    public InputField updateScore;
    public GameObject updatePanel;

    public Board EditData;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OpenScreenOne();
    }
   
    private IEnumerator fetchHeaderApiData()
    {
        print("fetchHeaderApiData :::::::::::: ");
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", tocken);
        yield return request.SendWebRequest();
        showPanel("list");
        //print("request.responseCode ::::: " + request.responseCode);
       print("request.downloadHandler.text ::::: " + request.downloadHandler.text);
        BoardData leaderBoardData = JsonUtility.FromJson<BoardData>(request.downloadHandler.text);
        datalist = leaderBoardData.Data; // Store fetched data in the list
        foreach (var leaderBoard in leaderBoardData.Data)
        {
            print(leaderBoard.name + " -> " + leaderBoard.score);
            
        }
        gridLayout.ShowData(datalist);
    }
    public void OpenScreenOne()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(true);
        
        StartCoroutine(addUserToLeaderBoard()); // Refresh the leaderboard data
    }
    public void ADD()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
    }
    public void addUser()
    {
        //Invoke("OpenSecondPanel", 1.2f);      
        // screenManager.instance.OpenSecondPanel();
        string name = UserName.text;
        int score = int.Parse(passWord.text);
        StartCoroutine(addUserToLeaderBoard(name, score));
        firstPanel.SetActive(false);
        // secondPanel.SetActive(true);
        thirdPanel.SetActive(true);

        string playerName = UserName.text;
        string playerScore = passWord.text;

        nameText.text =  playerName;
        scoreText.text = playerScore;      

        //string name = "user_" + UnityEngine.Random.Range(1, 1000);
        //int score = UnityEngine.Random.Range(1, 1000);
        //StartCoroutine(addUserToLeaderBoard(name, score));
    }
    //============================delete============================
    
    public void DeleteUser(string id)
    {
        print("DeleteUser called");
        StartCoroutine(DeleteUserRequest(id));

    }
    private IEnumerator DeleteUserRequest(string id)
    {
        string deleteUrl = url + "/" + id;
        UnityWebRequest request = UnityWebRequest.Delete(deleteUrl);
        request.SetRequestHeader("Authorization", tocken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            print("Delete failed: " + request.error);
        }
        else
        {
            print("Deleted user with id: " + id);
            StartCoroutine(fetchHeaderApiData()); // refresh
        }
    }
    // ==================================update==============================================
    public void UpdateUser(Board todo)
    {
        print("**************************UpdateUser called");
        showPanel("add");
        StartCoroutine(UpdateUserRequest(todo._id, todo.name, todo.score));
        firstPanel.SetActive(true);
        thirdPanel.SetActive(false);
        EditData = todo;
        if (EditData != null)
        {
            UserName.text = todo.name;
            passWord.text = todo.score.ToString();
        }
    }  
    public IEnumerator UpdateUserRequest(string id, string name, int score)
    {
        string bodyJsonString = "{\"name\": \"" + name + "\",\"score\": " + score + "}";

        UnityWebRequest request = new UnityWebRequest(url + "/" + id, "PATCH");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", tocken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);
        print("request.downloadHandler.text ::::: " + request.downloadHandler.text);

        StartCoroutine(fetchHeaderApiData());
    }

    
    private IEnumerator addUserToLeaderBoard(string name, int score)
    {
        //print("name : " + name + "  ->  " + "score : " + score);

        string bodyJsonString = "{\"name\": \"" + name + "\",\"score\": " + score + "}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", tocken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);

        print("request.downloadHandler.text ::::: " + request.downloadHandler.text);
        StartCoroutine(fetchHeaderApiData());

    }
    private IEnumerator addUserToLeaderBoard()
    {
        //print("name : " + name + "  ->  " + "score : " + score);

       // string bodyJsonString = "{\"name\": \"" + name + "\",\"score\": " + score + "}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");

       // byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", tocken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //print("request.responseCode ::::: " + request.responseCode);

        print("request.downloadHandler.text ::::: " + request.downloadHandler.text);
        StartCoroutine(fetchHeaderApiData());

    }

    public void showAddPenel()
    {
        showPanel("add");
    }

    public void showPanel(string panelName)
    {
        thirdPanel.SetActive(panelName == "list");
        firstPanel.SetActive(panelName == "add");
       // submitButtonText.text = EditData == null ? "Add" : "Update";
    }
    public void submitButton()
    {
        if (UserName.text == "")
        {
            print("please enter name");
            return;
        }
        string name = UserName.text;
        int score = int.Parse(passWord.text);
        if (EditData != null)
        {
            StartCoroutine(UpdateUserRequest(EditData._id, name, score));
            EditData = null;
        }
        else
        {
            StartCoroutine(addUserToLeaderBoard(name, score));
        }
    }
}

[Serializable]
public class BoardData
{
    public string Status;
    public string Message;
    public List<Board> Data;
    
}

[Serializable]
public class Board
{
    public string _id;
    public string name;
    public int score;
}



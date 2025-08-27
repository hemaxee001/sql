using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class sql : MonoBehaviour
{
    //[SerializeField]
    //int speed;
    public List<MyTodo> todosList = new List<MyTodo>(); // Store fetched todos here


    public static sql instance; // Singleton instance

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(fetchData());
    }
    void Update()
    {
        
    }
    public IEnumerator fetchData()
    {
        print("fetch data");
        string url = "https://dummyjson.com/todos";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        string data = request.downloadHandler.text;
        TodoData todos = JsonUtility.FromJson<TodoData>(data);

        todosList = todos.todos; // Store fetched todos in the list
        foreach (MyTodo todo in todos.todos)
        {
            print("ID: " + todo.id);
            print("TODO: " + todo.todo);
            print("COMPELETED: " + todo.completed);
            print("USERID: " + todo.userId);
            print("=============");
        }
        gird.instance.GenerateGrid(todosList);
       

    }
}

[Serializable]
class TodoData
{

    public List<MyTodo> todos;
    public int total;
    public int skip;
    public int limit;
}
[Serializable]

public class MyTodo
{
    public int id;
    public string todo;
    public bool completed;
    public int userId;   
}



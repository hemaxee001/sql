//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class LecDatabase : MonoBehaviour
//{

//    [SerializeField]
//    int speed;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        //StartCoroutine(fetchData());

//        //print("start");

//        //StartCoroutine(test());

//        StartCoroutine(fetchData());

//        //print("end");
//    }

//    //public IEnumerator test()
//    //{
//    //    print("test function start");

//    //    yield return new WaitForSeconds(2f);

//    //    print("============");

//    //    yield return new WaitForSeconds(2f);    

//    //    print("test function end");
//    //}
    

//    public IEnumerator fetchData()
//    {
//        print("fetch data");
//        string url = "https://dummyjson.com/todos";
        
//        UnityWebRequest request = UnityWebRequest.Get(url);
//        yield return request.SendWebRequest();

//        string data = request.downloadHandler.text;

//        TodoData todo = JsonUtility.FromJson<TodoData>(data);

//        //print(todo.todos.Count);
//        //foreach (Todo t in todo.todos)
//        //{
//        //    print(t);
//        //}
//    }
//    //public IEnumerator fetchData()
//    //{
//    //    print("fetch data");
//    //    string url = "https://dummyjson.com/todos";
//    //    WWW www = new WWW(url);
//    //    yield return www;

//    //    string data = www.text;

//    //    TodoData todo = JsonUtility.FromJson<TodoData>(data);

//    //    print(todo.todos.Count);
//    //    foreach (Todo t in todo.todos)
//    //    {
//    //        print(t.todo);
//    //    }
//    //}

//}

//// model / data class
////class TodoData
////{
////    public int total;
////    public int skip;
////    public int limit;
////    public List<Todo> todos;
////}
////[Serializable]

////class Todos
////{
////    public int id;
////    public string todo;
////    public bool completed;
////    public int userId;
////}
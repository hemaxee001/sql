using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class gird : MonoBehaviour
{
    public static gird instance; // Singleton instance
    public GameObject buttonPrefab;


    private void Awake()
    {
        instance = this; // Initialize the singleton instance
    }
    void Update()
    {        
    }
    public void GenerateGrid(List<MyTodo> todos)
    {
        foreach (Transform child in transform)  // delete duplicate data
        {
            Destroy(child.gameObject);
        }
     
        //print("GenerateGrid called with " + todos.Count + " todos");
        // Create buttons for each todo
        foreach (var todo in todos)
        {
            var clone = Instantiate(buttonPrefab, transform);
            var tx = clone.GetComponentInChildren<Text>();
            //tx.text = todo.todo; // Set the todo text in the button
            tx.text =
               $"ID: {todo.id}\n" +
               $"Todo: {todo.todo}\n" +
               $"Completed: {todo.completed}\n" +
               $"UserId: {todo.userId}";             
        }
    }
}

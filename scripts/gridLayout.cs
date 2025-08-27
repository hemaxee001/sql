using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class gridLayout : MonoBehaviour
{
    public GameObject buttonPrefab;

    
    public void ShowData(List<Board> data)
    {
        print("ShowData called with " + data.Count + " items");
        foreach (Transform child in transform)  // delete duplicate data
        {
            Destroy(child.gameObject);
        }
        foreach (var todo in data)
        {


            var clone = Instantiate(buttonPrefab, transform);
            var name = clone.transform.GetChild(0).GetComponent<Text>();
            var score = clone.transform.GetChild(1).GetComponent<Text>();

            var upd = clone.transform.GetChild(2).GetComponent<Button>();
            var del = clone.transform.GetChild(3).GetComponent<Button>();

            name.text = todo.name;
            score.text = todo.score.ToString();

            //tx.text = todo.todo; // Set the todo text in the button
            //tx.text =
            //   $"ID: {todo._id}\n" +
            //   $"name: {todo.name}\n" +
            //   $"score: {todo.score}\n";

            upd.onClick.AddListener(() =>
              
                HeaderApi.instance.UpdateUser(todo)
                
            );
            del.onClick.AddListener(() =>
            HeaderApi.instance.DeleteUser(todo._id)
            );


        }
    }
}

using UnityEngine;

public class screenManager : MonoBehaviour
{
    public GameObject  firstpanel,secondPanel;

    public static screenManager instance;

    private void Awake()
    {  
            instance = this;      
    }

    void Start()
    {
        //Invoke("OpenSecondPanel",1.2f);
    }
    public void OpenSecondPanel()
    {
        firstpanel.SetActive(false);
        secondPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

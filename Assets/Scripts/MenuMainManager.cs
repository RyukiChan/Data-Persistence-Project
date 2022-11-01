using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuMainManager : MonoBehaviour
    
{
    public TMP_InputField inputtedName;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        PersistantData.Instance.LoadHighScore();
        PersistantData.Instance.userName = inputtedName.text;
        SceneManager.LoadScene(1); 
    }

    public void DisplayName()

    {
        //Debug.Log("" + inputtedName.text);
    }
}

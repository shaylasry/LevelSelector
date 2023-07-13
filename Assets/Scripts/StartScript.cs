using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public InputField userNameFiled;
    [SerializeField] public InputField passwordFiled;
    public TextMesh userName;
    public TextMesh password;
    // Start is called before the first frame update
    void Start() {
        // button = GetComponent<Button>();
        button.onClick.AddListener(() => ButtonClicked()); // Assign a click listener to the button
    }

    // Update is called once per frame
    void Update()
    {
        userName.text = userNameFiled.text;
        password.text = passwordFiled.text;
        
    }
    
    private void ButtonClicked()
    {
        Debug.Log(userName.text);
        Debug.Log(password.text);

        if (isUserValid())
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("user is not valid");
        }
        // Log a message indicating which button was clicked
    }
    
    private bool isUserValid()
    {
        return false;
        // Log a message indicating which button was clicked
    }
}

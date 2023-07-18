using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    [SerializeField] public Button button;

    // Start is called before the first frame update
    void Start() {
        // button = GetComponent<Button>();
            button.onClick.AddListener(() => ButtonClicked()); // Assign a click listener to the button
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ButtonClicked()
    {
        SceneManager.LoadScene("Menu");
        // Log a message indicating which button was clicked
    }
}

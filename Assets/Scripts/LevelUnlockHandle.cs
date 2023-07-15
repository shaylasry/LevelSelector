using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class LevelUnlockHandle : MonoBehaviour, ILevelPresenter {
    [SerializeField] public GameObject buttonPrefab;
    
    [SerializeField] private Button[] buttons;
    private IPlayerProvider _playerProvider;
    private ILevelProvider _levelProvider;
    private List<Level> _levels;
    
    
    void Awake()
    {
        _playerProvider = IPlayerProviderFactory.createPlayerProvider();
        _levelProvider = ILevelProviderFactory.createLevelProvider();
        
        DefaultLevelPresenter(_playerProvider, _levelProvider);
    }

    
    private void ButtonClicked(string levelName)
    {
        // Log a message indicating which button was clicked
        Debug.Log(levelName);
    }
    
    public void DefaultLevelPresenter(IPlayerProvider playerProvider, ILevelProvider levelProvider) {
        //get data about player and levels from the providers
        Player curPlayer = playerProvider.LoadPlayerData(67890);
        List<Level> levels = levelProvider.LoadLevels(curPlayer);
       
        //create the levels buttons
        foreach (Level level in levels)
        {
            //create button object in runtime from Prefab
            GameObject buttonGO = Instantiate(buttonPrefab, transform);
            Button button = buttonGO.GetComponent<Button>();
            
            //init button name
            Score levelScore = LevelScoring(level);
            string buttonName = "" + level.levelName + "\n" + levelScore.ScoreAsNum;
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();

            // Change the text of the button
            if (buttonText != null)
            {
                buttonText.text = buttonName;
            }
            
            // make button interacgible
            button.onClick.AddListener(() => ButtonClicked(level.levelName)); // Assign a click listener to the button
            if (!IsLevelAvailable(level))
            {
                button.enabled = false;
            }
        }
    }

    public bool IsLevelAvailable(Level level) {
        return level.isAvailable;
    }

    public Score LevelScoring(Level level) {
        return new Score(level.scoring);
    }
}

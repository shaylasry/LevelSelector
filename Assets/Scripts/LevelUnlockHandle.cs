using System;
using System.Collections;
using System.Collections.Generic;

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
       Player curPlayer = playerProvider.LoadPlayerData(12345);
       List<Level> levels = levelProvider.LoadLevels(curPlayer);
       bool rand = true;
       for (int i = 1; i < 11; i++)
       {
           int scoring = 0;
           Random random = new Random();
           // Generate a random boolean value
           bool isAvailable = rand && random.Next(2) == 0;
           if (i == 1)
           {
               isAvailable = true;
           }
           rand = isAvailable;

           if (isAvailable)
           {
               random = new Random();

               // Generate a random number between 1 and 10
               int randomNumber = random.Next(10) + 1;

               // Calculate the final random number with jumps of 10
               scoring = (randomNumber - 1) * 10 + 10;
           }

           Level l = new Level(i, "Level " + i, scoring, isAvailable);
           levels.Add(l);
       }
        //create the levels buttons
        foreach (Level level in levels)
        {
            //create button object in runtime from Prefab
            GameObject buttonGO = Instantiate(buttonPrefab, transform);
            Button button = buttonGO.GetComponent<Button>();
            
            //init button name
            Score curLevelScore = LevelScoring(level);
            string buttonName = "" + level.levelName + "\n" + curLevelScore.ScoreAsNum;
            TextMesh buttonText = buttonGO.GetComponentInChildren<TextMesh>();

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

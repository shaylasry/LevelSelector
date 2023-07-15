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
            string buttonName = "  " + level.levelName;
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            
            // Change the text of the button
            if (buttonText != null)
            {
                buttonText.text = buttonName;
                HandleScoring(levelScore, button, 100);
            }
            
            // make button interacgible
            button.onClick.AddListener(() => ButtonClicked(level.levelName)); // Assign a click listener to the button
            if (!IsLevelAvailable(level))
            {
                button.enabled = false;
                button.interactable = false;
            }
        }
    }

    public void HandleScoring(Score score, Button button, int maxScore)
    {
        Transform buttonScroingGrid = button.transform.GetChild(1);
        
        Image firstStarImage = buttonScroingGrid.GetChild(2).GetComponent<Image>();
        Image secondStarImage = buttonScroingGrid.GetChild(1).GetComponent<Image>();
        Image thirdStarImage = buttonScroingGrid.GetChild(0).GetComponent<Image>();

        float scoreDivision = maxScore / 3;
        float firstStartGrading = 0.0f;
        float secondStartGrading = scoreDivision;
        float thirdStartGrading = scoreDivision * 2;

        PaintStar(firstStarImage, score.scoreAsNum, firstStartGrading);
        PaintStar(secondStarImage, score.scoreAsNum, secondStartGrading);
        PaintStar(thirdStarImage, score.scoreAsNum, thirdStartGrading);

    }

    public void PaintStar(Image starImage, int playerScore, float scoreToPass)
    {
        if (playerScore > scoreToPass)
            starImage.color = Color.white;
        else
            starImage.color = Color.gray;
    }
    public bool IsLevelAvailable(Level level) {
        return level.isAvailable;
    }

    public Score LevelScoring(Level level) {
        return new Score(level.scoring);
    }
}

using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockHandle : MonoBehaviour, ILevelPresenter {
    [SerializeField] public GameObject buttonPrefab;
    
    [SerializeField] private Button[] buttons;
    private IPlayerProvider _playerProvider;
    private ILevelProvider _levelProvider;
    private List<Level> _levels;
    
    
    void Awake() {

        DefaultLevelPresenter(_playerProvider, _levelProvider);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        //create the levels buttons
        foreach (Level level in levels)
        {
            //create button object in runtime from Prefab
            GameObject buttonGO = Instantiate(buttonPrefab, transform);
            Button button = buttonGO.GetComponent<Button>();
            
            //init button name
            Score curLevelScore = LevelScoring(level);
            string buttonName = "" + level.LevelName + "\n" + curLevelScore.ScoreAsNum;
            button.name = buttonName;
            
            // make button interacgible
            button.onClick.AddListener(() => ButtonClicked(level.LevelName)); // Assign a click listener to the button
            if (!IsLevelAvailable(level))
            {
                button.enabled = false;
            }
        }
    }

    public bool IsLevelAvailable(Level level) {
        return level.IsAvailable;
    }

    public Score LevelScoring(Level level) {
        return new Score(level.Scoring);
    }
}

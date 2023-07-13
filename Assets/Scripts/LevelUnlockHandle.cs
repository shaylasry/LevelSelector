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
       
        Player curPlayer = playerProvider.LoadPlayerData(12345);
        
        List<Level> levels = levelProvider.LoadLevels(curPlayer);
        foreach (Level level in levels)
        {
            GameObject buttonGO = Instantiate(buttonPrefab, transform);
            Button button = buttonGO.GetComponent<Button>();
            string buttonName = "" + level.LevelName + "\n" + level.Scoring;
            button.name = buttonName;
            button.onClick.AddListener(() => ButtonClicked(level.LevelName)); // Assign a click listener to the button
            if (!level.IsAvailable)
            {
                button.enabled = false;
            }
        }
    }

    public bool IsLevelAvailable(Player player, Level level) {
        return false;
    }

    public Score LevelScoring(Player player, Level level) {
        throw new System.NotImplementedException();
    }
}

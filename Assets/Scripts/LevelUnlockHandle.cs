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
        //init buttons? check if button are ok
        for (int i = 0; i < 10; i++) {
            int buttonIndex = i + 1;
            GameObject buttonGO = Instantiate(buttonPrefab, transform);
            Button button = buttonGO.GetComponent<Button>();
            button.onClick.AddListener(() => ButtonClicked(buttonIndex)); // Assign a click listener to the button
            button.enabled = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ButtonClicked(int buttonIndex)
    {
        // Log a message indicating which button was clicked
        Debug.Log("Level " + buttonIndex + " clicked!");
    }
    
    public void DefaultLevelPresenter(IPlayerProvider playerProvider, ILevelProvider levelProvider) {
        Player curPlayer = playerProvider.LoadPlayerData(1);
        List<Level> levels = levelProvider.LoadLevels();
        foreach (Level level in levels)
        {
            // Code to be executed for each level
            if (IsLevelAvailable(curPlayer, level)) {
                Score levelScore = LevelScoring(curPlayer, level);
                
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

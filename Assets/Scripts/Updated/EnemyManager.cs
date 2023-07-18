using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float enemyPercent = .1f;
    [SerializeField] private List<GameObject> enemyPrefabs;

    private List<GameObject> enemies;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region Configuration

    public void SetEnemyPercent(float percent)
    {
        enemyPercent = percent;
    }

    #endregion

    #region Game Handling

    public void HandleMovement()
    {
        if (enemies == null) return;

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<MoleController>().HandleMovement();    
            }
        }
    }

    #endregion

    #region Initialization

    private void InstantiateEnemies(Vector3[,] gridData)
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogError("failed to instantiate enemies, no enemy prefabs were set");
            return;
        }

        var targetGridData = Vector3Utils.RemoveBottomRow(gridData);
        List<int[]> enemyPositions = Vector3Utils.GetRandomIndices(targetGridData, enemyPercent);

        List<GameObject> newEnemeies = new List<GameObject>();

        foreach (var enemyPos in enemyPositions)
        {
            Vector3 enemyWorldPos = targetGridData[enemyPos[0], enemyPos[1]];
            GameObject enemy1 = Instantiate(enemyPrefabs[0], enemyWorldPos, Quaternion.identity, transform);

            var controller = enemy1.GetComponent<MoleController>(); 
            controller.GridData = targetGridData;
            controller.GridPosition = new Vector2(enemyPos[0], enemyPos[1]);

            newEnemeies.Add(enemy1);
        }

        enemies = newEnemeies;
    }

    #endregion
    
    #region Event Handling

    private void SubscribeEvents()
    {
        GridManager.GridDidLoad += OnGridDidLoad;
    }

    private void UnsubscribeEvents()
    {
        GridManager.GridDidLoad -= OnGridDidLoad;
    }

    private void OnGridDidLoad(GridManager gridGenerator)
    {
        InstantiateEnemies(gridGenerator.GridData);
    }

    #endregion
}

using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineGridTargetGroupController : MonoBehaviour
{
    [SerializeField] private float targetRadius = 0.2f;
    
    private CinemachineTargetGroup targetGroup;

    void OnEnable()
    {
        SubscribeEvents();
        
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region Event Handling

    private void SubscribeEvents()
    {
        GridManager.GridDidLoad += OnGridLoad;
    }

    private void UnsubscribeEvents()
    {
        GridManager.GridDidLoad -= OnGridLoad;
    }

    private void OnGridLoad(GridManager gridGenerator)
    {
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[2];
        
        targetGroup.m_Targets.SetValue(new CinemachineTargetGroup.Target
        {
            target = gridGenerator.GridGameObjects[0, 0].transform,
            weight = 1,
            radius = gridGenerator.GetConfiguration().targetGroupRadius
        }, 0);

        var rows = gridGenerator.GridGameObjects.GetLength(0);
        var cols = gridGenerator.GridGameObjects.GetLength(1);
        
        targetGroup.m_Targets.SetValue(new CinemachineTargetGroup.Target
        {
            target = gridGenerator.GridGameObjects[rows - 1, cols - 1].transform,
            weight = 1,
            radius = gridGenerator.GetConfiguration().targetGroupRadius
        }, 1);
    }

    #endregion
}

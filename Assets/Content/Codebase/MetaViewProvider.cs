using CameraProcessing;
using DefaultNamespace;
using Movement;
using UnityEngine;

/// <summary>
/// Инкапсулирует ссылки на монобехи
/// </summary>
public class MetaViewProvider : MonoBehaviour
{
    [field:Header("Global")]
    [field:SerializeField]
    public CamerasContainer CamerasContainer { get; private set; }
    
    [field:SerializeField]
    public WindowsSwitcher WindowsSwitcher { get; private set; }
    
    [field:Header("Player")]
    [field:SerializeField]
    public PlayerMovement PlayerMovement { get; private set; }

    [field:SerializeField]
    public ControlMovementPlayer ControlMovementPlayer { get; private set; }
}
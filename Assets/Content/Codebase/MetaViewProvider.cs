using CameraProcessing;
using Movement;
using UnityEngine;

/// <summary>
/// Инкапсулирует ссылки на монобехи
/// </summary>
public class MetaViewProvider : MonoBehaviour
{
    [field:Header("Player")]
    [field:SerializeField]
    public PlayerMovement PlayerMovement { get; private set; }

    [field:SerializeField]
    public ControlMovementPlayer ControlMovementPlayer { get; private set; }
    
    [field:SerializeField]
    public CamerasContainer CamerasContainer { get; private set; }
}
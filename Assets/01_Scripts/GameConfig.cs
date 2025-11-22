using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config")]
public class GameConfig :ScriptableObject
{
    [Header("Tags")]
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    public string groundTag = "Ground";
    
    [Header("Layers")]
    public string playerLayer = "Player";
    public string enemyLayer = "Enemy";
    public string groundLayer = "Ground";
    
    [Header("Animation Parameters")]
    public string isWalking = "IsWalking";
    public string attack = "Attack";
    public string dash = "Dash";
}
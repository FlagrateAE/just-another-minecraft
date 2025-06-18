using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects/Item", fileName = "NewItem")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase Tile;
    public ItemType Type;
    public ActionType ActionType;
    public Vector2Int range = new Vector2Int(5, 4);
    
    [Header("Only UI")]
    public bool Stackable = true;
    public int MaxStackSize = 64;
    
    [Header("Both")]
    public Sprite Image;
}

public enum ItemType
{
    Block,
    Tool,
    Food
}

public enum ActionType
{
    Dig,
    Mine
}
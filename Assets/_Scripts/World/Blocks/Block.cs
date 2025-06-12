using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject
{
    public BlockId BlockId;
    public float Durability;
    public Texture2D Texture;
}

public enum BlockId : byte
{
    Air = 0,
    Dirt = 1
}
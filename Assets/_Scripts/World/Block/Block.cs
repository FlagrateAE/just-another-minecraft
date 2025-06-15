using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject
{
    public BlockId Id;
    public float Durability;
    public bool IsTransparent;
    public BlockTextureData TextureData;
}

[Serializable]
public class BlockTextureData
{
    [Header("Atlas textures positions")]
    public Vector2Int Front;
    public Vector2Int Back;
    public Vector2Int Left;
    public Vector2Int Right;
    public Vector2Int Top;
    public Vector2Int Bottom;


    public const int AtlasColumns = 16;
    public const int AtlasRows = 16;
    public const int TextureSize = 16;
}
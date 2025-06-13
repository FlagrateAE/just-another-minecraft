using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject
{
    public BlockId BlockId;
    public float Durability;
    public BlockTextureData TextureData;
}

[Serializable]
public class BlockTextureData
{
    [Header("Atlas textures IDs")]
    public uint Front;
    public uint Back;
    public uint Left;
    public uint Right;
    public uint Top;
    public uint Bottom;


    public const int AtlasColumns = 16;
    public const int AtlasRows = 16;
    public const int TextureSize = 16;
    public Vector2Int GetTexturePosition(Face face)
    {
        uint index = face switch
        {
            Face.Front  => Front,
            Face.Back   => Back,
            Face.Left   => Left,
            Face.Right  => Right,
            Face.Top    => Top,
            Face.Bottom => Bottom,
            _ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
        };
        return GetPixelPositionFromIndex(index);
    }

    private static Vector2Int GetPixelPositionFromIndex(uint index)
    {
        int xTile = (int)(index % AtlasColumns);
        int yTile = (int)(index / AtlasColumns);
        return new Vector2Int(xTile, yTile);
    }
}

public enum BlockId : byte
{
    Air = 0,
    Dirt = 1,
    OakPlanks = 2
}

public enum Face : byte
{
    Front,
    Back,
    Left,
    Right,
    Top,
    Bottom
}
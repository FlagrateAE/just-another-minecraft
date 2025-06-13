using System.Collections.Generic;
using UnityEngine;

public class BlockRegistry
{
    private const string BlocksFolderPath = "ScriptableObjects/Blocks";

    private List<Block> _blocks = new();

    public BlockRegistry()
    {
        _blocks.AddRange(Resources.LoadAll<Block>(BlocksFolderPath));
    }

    public float GetDurability(BlockId id) => _blocks[(int)id].Durability;
    public BlockTextureData GetTextureData(BlockId id) => _blocks[(int)id].TextureData;
}
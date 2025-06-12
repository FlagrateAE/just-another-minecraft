using System.Collections.Generic;
using UnityEngine;

public class BlockDatabase
{
    private const string BlocksFolderPath = "ScriptableObjects/Blocks";

    private List<Block> _blocks = new();

    public BlockDatabase()
    {
        _blocks.AddRange(Resources.LoadAll<Block>(BlocksFolderPath));
    }

    public float GetDurability(BlockId id) => _blocks[(int)id].Durability;
}
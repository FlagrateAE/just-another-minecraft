using System;
using UnityEngine;

public static class TerrainGenerator
{
    public static BlockType[,,] GenerateTerrain(int xOffset, int zOffset)
    {
        var result = new BlockType[
            Chunk.Width,
            Chunk.Height,
            Chunk.Width
        ];

        for (int x = 0; x < Chunk.Width; x++)
        {
            for (int z = 0; z < Chunk.Width; z++)
            {
                float height = Mathf.PerlinNoise((x + xOffset) * .2f, (z + zOffset) * .2f) * 5 + 10;

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlockType.Dirt;
                }
            }
        }

        return result;
    }
}
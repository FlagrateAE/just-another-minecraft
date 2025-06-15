using UnityEngine;

public static class Vector3IntExtensions
{
    public static Vector3Int WorldToChunkPosition(this Vector3Int position)
    {
        return new Vector3Int(position.x % Chunk.Width, position.y, position.z % Chunk.Width);
    }
}
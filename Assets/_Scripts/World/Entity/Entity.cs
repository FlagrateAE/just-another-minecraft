using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector3 Position => transform.position;
}
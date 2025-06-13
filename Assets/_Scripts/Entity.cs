using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("Maximum distance the entity can interact from. Basically \"hands length\".")]
    public float Reach = 7f;

    [Tooltip("Transform from which all the interactions are originating.")]
    public Transform Head;
}
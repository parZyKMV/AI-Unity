using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [SerializeField] string info;   

    [SerializeField] protected string nametag;
    [SerializeField] protected float maxDistance;
    [SerializeField, Range(0,100)] protected float maxAngle;

    public abstract GameObject[] GetGameObjects();
}

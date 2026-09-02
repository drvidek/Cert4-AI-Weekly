using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void Interact(GameObject other)
    {
        print($"{other.name} is interacting with {name}!");
    }
}
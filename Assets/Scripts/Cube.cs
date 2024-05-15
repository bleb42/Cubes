using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> Touched;

    public float Scale { get; private set; }
    public float ChanceToDouble { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public bool CanDouble { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CanDouble = false;
    }

    private void OnMouseUpAsButton()
    {
        if (UnityEngine.Random.Range(0, 100) <= ChanceToDouble)
            CanDouble = true;

        Touched?.Invoke(this);

        Destroy(gameObject);
    }

    public void Init(float changeToDouble, float scale)
    {
        ChanceToDouble = changeToDouble;
        Scale = scale;
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
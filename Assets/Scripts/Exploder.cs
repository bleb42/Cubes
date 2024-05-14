using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 10f;

    public void Explode(List<Cube> targets, Transform forcePosition)
    {
        foreach (Cube target in targets)
        {
            target.Rigidbody.AddExplosionForce(_explosionForce, forcePosition.position, _explosionRadius);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 100f;

    public void ExplodeCubes(List<Cube> targets, Transform forcePosition)
    {
        foreach (Cube target in targets)
        {
            target.Rigidbody.AddExplosionForce(_explosionForce, forcePosition.position, _explosionRadius);
        }
    }

    public void ExplodeAllCubes(Cube cubeToExplolde)
    {
        Collider[] hits = Physics.OverlapSphere(cubeToExplolde.transform.position, _explosionRadius / cubeToExplolde.Scale);
        List<Cube> cubes = new List<Cube>();    

        foreach (var hit in hits)
            if (hit.TryGetComponent(out Cube cube))
                cubes.Add(cube);

        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(_explosionForce / cubeToExplolde.Scale, cubeToExplolde.transform.position, _explosionRadius / cubeToExplolde.Scale);
        }
    }
}
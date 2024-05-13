using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField, Range(1, 4)] private int _startCubesAmount = 4;
    [SerializeField] private int _explodeCubesMinAmount = 2;
    [SerializeField] private int _explodeCubesMaxAmount = 6;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private Cube _cubePrefab; 
    [SerializeField] private Transform[] _spawnpoints;
    [SerializeField] private Material[] _materials;

    private void OnValidate()
    {
        if (_explodeCubesMinAmount > _explodeCubesMaxAmount)
        {
            _explodeCubesMinAmount = _explodeCubesMaxAmount - 1;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _startCubesAmount; i++)
        {
            SpawnCube(_spawnpoints[i]);
        }
    }

    public void ExplodeCube(Cube cube)
    {
        int cubesAmount = UnityEngine.Random.Range(_explodeCubesMinAmount, _explodeCubesMaxAmount);
        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < cubesAmount; i++)
        {
            cubes.Add(SpawnCube(cube.transform, cube.ChanceToDouble));
        }

        AddExplosionForce(cubes, cube.transform);
        Destroy(cube.gameObject);
    }

    private void AddExplosionForce(List<Cube> targets, Transform forcePosition)
    {
        foreach (Cube target in targets) 
        {
            if (!target.TryGetComponent(out Rigidbody rigidbody))
                return;

            rigidbody.AddExplosionForce(_explosionForce, forcePosition.position, _explosionRadius);
        }
    }


    private Cube SpawnCube(Transform transform, int changeToDouble)
    {
        int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);

        Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
        cube.GetComponent<Renderer>().material = _materials[randomMaterial];
        cube.AddCubeSpawner(this);
        cube.SetChangeToDoube(changeToDouble);

        return cube;
    }

    private Cube SpawnCube(Transform transform)
    {
        int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);

        Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
        cube.GetComponent<Renderer>().material = _materials[randomMaterial];
        cube.AddCubeSpawner(this);
        cube.SetChangeToDoube(100);

        return cube;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField, Range(1, 4)] private int _startCubesAmount = 4;
    [SerializeField] private int _explodeCubesMinAmount = 2;
    [SerializeField] private int _explodeCubesMaxAmount = 6;
    [SerializeField] private float _chanceToDouble = 0.5f;
    [SerializeField] private float _sizeChange = 0.5f;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Exploder _exploder;
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

    public void SpawnCubes(Cube cube)
    {
        int cubesAmount = UnityEngine.Random.Range(_explodeCubesMinAmount, _explodeCubesMaxAmount);
        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < cubesAmount; i++)
        {
            cubes.Add(SpawnCube(cube.transform, cube.ChanceToDouble * _chanceToDouble, cube.Scale * _sizeChange));
        }

        _exploder.ExplodeCubes(cubes, cube.transform);
    }

    private Cube SpawnCube(Transform transform, float changeToDouble = 100, float scale = 1)
    {
        int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);

        Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
        cube.Init(changeToDouble, scale);

        if (cube.TryGetComponent(out Renderer renderer))
            renderer.material = _materials[randomMaterial];

        cube.Touched += OnCubeTouched;

        return cube;
    }

    private void OnCubeTouched(Cube cube)
    {
        cube.Touched -= OnCubeTouched;

        if (cube.CanDouble)
            SpawnCubes(cube);
    }
}

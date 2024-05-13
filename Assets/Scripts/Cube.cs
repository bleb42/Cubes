using UnityEngine;

public class Cube : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;

    public int ChanceToDouble { get; private set; }

    private void OnMouseUpAsButton()
    {
        if (Random.Range(0, 100) <= ChanceToDouble)
            _cubeSpawner.ExplodeCube(this);
        else
            Destroy(gameObject);
    }

    public void AddCubeSpawner(CubeSpawner cubeSpawner)
    {
        _cubeSpawner = cubeSpawner;
    }

    public void SetChangeToDoube(int parentChangeToDouble)
    {
        ChanceToDouble = parentChangeToDouble / 2;
    }
}
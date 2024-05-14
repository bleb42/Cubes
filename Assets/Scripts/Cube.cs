using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public float Scale { get; private set; }
    public int ChanceToDouble { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        Rigidbody= GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.Range(0, 100) <= ChanceToDouble)
            _cubeSpawner.ExplodeCube(this);

        Destroy(gameObject);
    }

    public void Init(CubeSpawner cubeSpawner, int changeToDouble, float scale)
    {
        ChanceToDouble = changeToDouble;
        _cubeSpawner = cubeSpawner;
        Scale = scale;
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
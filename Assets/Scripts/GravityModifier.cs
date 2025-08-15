using UnityEngine;

public class GravityModifier : MonoBehaviour
{
    private float gravityModifier = -5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = new Vector3(0, gravityModifier, 0);
        //Lo tengo puesto en update en vez de start porque igual hago que la gravedad aumenta progresivamente para aumentar la dificultad
    }
}

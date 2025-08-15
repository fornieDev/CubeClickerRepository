using Unity.VisualScripting;
using UnityEngine;

public class BottomLimit : MonoBehaviour
{
    private Raycast raycastScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycastScript = GameObject.Find("Main Camera").GetComponent<Raycast>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")) 
        {
            Debug.Log(other.name + " ha caído.");
            StartCoroutine(raycastScript.GameOver());
        }
        
    }
}

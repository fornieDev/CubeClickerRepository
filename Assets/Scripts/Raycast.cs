using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    private float maxVerticalForce = 10f;
    private float minVerticalForce = 5f;
    private float minTorque = 1f;
    private float maxTorque = 3f;
    private Rigidbody cubeRigidbody;
    private Camera camara;
    private RaycastHit[] impactos;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI clickCounterText;
    private int clickCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeRigidbody = GameObject.Find("Cube").GetComponent<Rigidbody>();
        camara = GetComponent<Camera>();
        camara.backgroundColor = Color.white;
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Detectar clic en PC
        if (Input.GetMouseButtonDown(0))
        {
            ProcesarInput(Input.mousePosition);
        }
        */

        // Detectar toque en móvil
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcesarInput(Input.GetTouch(0).position);
        }
    }

    //Esto se cambio de vector3 a vector2
    void ProcesarInput(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition); //Crear el rayo

        // RaycastAll devuelve todos los impactos del rayo
        impactos = Physics.RaycastAll(ray);

        System.Array.Sort(impactos, (a, b) => a.distance.CompareTo(b.distance)); //Ordenar impactos

        if (impactos.Length > 0) 
        {
            applyForces();
            checkBackground(impactos[0].collider.name);
        }
    }

    void applyForces()
    {
        float fuerza = Random.Range(minVerticalForce, maxVerticalForce);
        cubeRigidbody.AddForce(Vector3.up * fuerza, ForceMode.Impulse);

        //cubeRigidbody.AddTorque(Random.Range(minTorque, maxTorque), Random.Range(minTorque, maxTorque), Random.Range(minTorque, maxTorque));
        Vector3 randomAngularVelocity = new Vector3(
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque)
        );
        cubeRigidbody.angularVelocity = randomAngularVelocity;
    }

    void checkBackground(string colorImpacto) 
    {
        Color colorBackground = Color.black;
        switch (colorImpacto) 
        {
            case "Red":
                colorBackground = Color.red;
                break;
            case "White":
                colorBackground = Color.white;
                break;
            case "Yellow":
                colorBackground = Color.yellow;
                break;
            case "Grey":
                colorBackground = Color.grey;
                break;
            case "Green":
                colorBackground = Color.green;
                break;
            case "Blue":
                colorBackground = Color.blue;
                break;
            default:
                colorBackground = Color.black;
                break;

        }
        if (camara.backgroundColor == colorBackground)
        {
            Debug.Log("Has perdido");
            //StartCoroutine(GameOver());

            camara.backgroundColor = Color.black;
            Invoke(nameof(DelayedGameOver), 0f);
        }
        else 
        {
            camara.backgroundColor = colorBackground;
            clickCounter++;
            clickCounterText.text = "Click counter: " + clickCounter;
        }
    }

    public IEnumerator GameOver() 
    {
        camara.backgroundColor = Color.black;
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DelayedGameOver()
    {
        StartCoroutine(GameOver());
    }
}

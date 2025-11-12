using UnityEngine;
using UnityEngine.InputSystem;

public class caja_fusibles_control : MonoBehaviour
{
    [Header("Palanca")]
    private bool palanca_activa = false;
    public float speed = 2f; // velocidad de movimiento vertical

    [Header("Movimiento vertical")]
    public float posXAbajo = 34f;   // posición inicial de la palanca (abajo)
    public float posXArriba = -20f; // posición cuando se abre (arriba)
    private Quaternion openRotation;

    [Header("Luces")]
    public GameObject luzVerde;
    public GameObject luzRoja;

    void Start()
    {
        // Posición inicial
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(posXArriba, 0, 0));

        // Luces
        luzRoja.SetActive(true);
        luzVerde.SetActive(false);
    }

    void Update()
    {
        // Detectar tecla P
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            palanca_activa = true;
            luzRoja.SetActive(false);
            luzVerde.SetActive(true);
        }

        // Mover palanca verticalmente
        if (palanca_activa)
        {
            transform.rotation = Quaternion.Lerp(openRotation, transform.rotation, Time.deltaTime * speed);
        }
    }
}

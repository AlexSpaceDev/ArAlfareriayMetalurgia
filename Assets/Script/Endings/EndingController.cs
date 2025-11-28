using UnityEngine;

public class EndingController : MonoBehaviour
{
    public GameObject finalA;
    public GameObject finalC;

    void Start()
    {
        // Apagar todo primero
        finalA.SetActive(false);
        finalC.SetActive(false);

        // Mostrar final correcto
        switch (GameData.finalToShow)
        {
            case 1: 
                finalA.SetActive(true); 
                break;

            case 3: 
                finalC.SetActive(true); 
                break;
        }

        // No limpiamos finalToShow inmediatamente
        // para evitar bugs si el jugador vuelve accidentalmente
        // a esta escena desde el menú.
    }
}

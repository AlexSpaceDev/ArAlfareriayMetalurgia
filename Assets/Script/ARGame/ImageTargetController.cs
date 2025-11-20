using UnityEngine;
using UnityEngine.UI;

public class ImageTargetController : MonoBehaviour
{
    public Button[] buttons;        // 3 botones
    public int correctIndex;        // índice del botón correcto
    private int attempts = 0;       // intentos fallidos

    private bool isActive = false;  // Para evitar clicks cuando no toca

    public void SetInteractable(bool state)
    {
        isActive = state;

        foreach (Button b in buttons)
        {
            b.interactable = state;
        }
    }

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int indexCopy = i;

            buttons[i].onClick.AddListener(() => OnButtonPressed(indexCopy));
        }
    }

    private void OnButtonPressed(int index)
    {
        if (!isActive)
            return;

        // Deshabilitar siempre cada botón al presionarlo
        buttons[index].interactable = false;

        if (index == correctIndex)
        {
            // Correcto
            int points = Mathf.Max(0, 2 - attempts);
            GameManager.Instance.AddScore(points);

            // Cambiar color a verde
            buttons[index].GetComponent<Image>().color = Color.green;

            Debug.Log("Correcto! +" + points + " puntos");

            GameManager.Instance.OnTargetCompleted();
        }
        else
        {
            // Incorrecto
            attempts++;

            // Cambiar color a rojo
            buttons[index].GetComponent<Image>().color = Color.red;

            Debug.Log("Incorrecto, intentos: " + attempts);
        }
    }
}

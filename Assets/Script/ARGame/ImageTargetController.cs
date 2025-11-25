using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        // Activar botones al iniciar
        SetInteractable(true);
    }

    private void OnButtonPressed(int index)
    {
        if (!isActive)
            return;

        // Deshabilitar siempre cada botón al presionarlo
        buttons[index].interactable = false;

        if (index == correctIndex)
        {
            // Deshabilitar TODOS para evitar más clicks
            SetInteractable(false);

            // Cambiar a verde permanentemente
            var img = buttons[index].GetComponent<Image>();
            img.color = Color.green;

            // Correcto
            int points = Mathf.Max(0, 2 - attempts);
            GameManager.Instance.AddScore(points);

            // Mostrar mensaje si quieres
            HUDController.Instance.ShowNextImageMessage();

            // Pasar al siguiente después de un pequeño delay
            StartCoroutine(NextAfterDelay());
        }
        else
        {
            // Incorrecto
            attempts++;

            // Cambiar color a rojo
            var img = buttons[index].GetComponent<Image>();
            img.color = Color.red;
            
            // Evitar que vuelva a su color original
            buttons[index].interactable = false;
        }
    }

    private IEnumerator NextAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OnTargetCompleted();
    }
}

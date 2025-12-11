using UnityEngine;

public class PreinstruccionController : MonoBehaviour
{
    [Header("Pantallas internas")]
    public GameObject preinstruccionPanel;
    public GameObject instruccionesPanel;

    // Llamado por el botón NEXT en Preinstruccion
    public void GoToInstrucciones()
    {
        preinstruccionPanel.SetActive(false);
        instruccionesPanel.SetActive(true);
    }

    // Llamado por el botón BACK en Instrucciones
    public void BackToPreinstruccion()
    {
        instruccionesPanel.SetActive(false);
        preinstruccionPanel.SetActive(true);
    }
}

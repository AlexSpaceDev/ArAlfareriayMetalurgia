using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCollectiblePanel : MonoBehaviour
{
    public static ShowCollectiblePanel Instance;

    [Header("Panel Principal")]
    public GameObject panelRoot;

    [Header("Contenido dinámico")]
    public Image collectImage;      // Imagen izquierda (CollectImage)
    public TMP_Text descriptionText; // Texto derecha (Text)

    [Header("Datos de los coleccionables")]
    public Sprite[] collectibleSprites = new Sprite[8];
    [TextArea(3,10)]
    public string[] collectibleTexts = new string[8];

    [Header("Opcional")]
    public Vector2 imageSize = new Vector2(774, 904); // Puedes ajustarlo desde el inspector

    private void Awake()
    {
        Instance = this;
        panelRoot.SetActive(false);
    }

    public void Show(int id)
    {
        // Precaución
        if (id < 0 || id >= collectibleSprites.Length)
        {
            Debug.LogError("ID fuera de rango en ShowCollectiblePanel");
            return;
        }

        // Poner imagen
        collectImage.sprite = collectibleSprites[id];

        // Ajustar tamaño dentro del contenedor (evita deformación)
        collectImage.rectTransform.sizeDelta = imageSize;

        // Poner texto
        descriptionText.text = collectibleTexts[id];

        // Mostrar panel
        panelRoot.SetActive(true);
    }

    public void Close()
    {
        panelRoot.SetActive(false);
    }
}

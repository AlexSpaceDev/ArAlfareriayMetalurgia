using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EndingSplashManager : MonoBehaviour
{
    [System.Serializable]
    public class EndingSplash
    {
        public string id;       // "A", "B", "C", "D"
        public GameObject panel; // SplashA, SplashB, etc.
    }

    public EndingSplash[] splashes;

    private GameObject currentSplash = null;
    private InputSystem_Actions actions;

    // Bloqueo de cierre rápido
    private float closeTime = -1f;
    private bool splashBlocked = false;

    void Awake()
    {
        actions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        actions.UI.Enable();
        actions.UI.Click.performed += OnClickPerformed;
    }

    void OnDisable()
    {
        actions.UI.Click.performed -= OnClickPerformed;
        actions.UI.Disable();
    }

    // === Llamado por los botones de los finales ===
    public void ShowSplash(string id)
    {
        // evitar que se abra si está bloqueado
        if (splashBlocked)
        {
            Debug.Log("ShowSplash bloqueado por protección de doble click.");
            return;
        }

        // 1. Verificar si ese final está realmente revelado
        if (!IsEndingRevealed(id))
        {
            Debug.Log($"ShowSplash BLOQUEADO: el final {id} NO está revelado.");
            return; // No hace nada
        }

        // Bloqueamos inmediatamente
        splashBlocked = true;

        // 2. Si está revelado, procede normalmente
        Debug.Log("ShowSplash CALLED con id = " + id);

        foreach (var s in splashes)
        {
            if (s.id == id)
            {
                s.panel.SetActive(true);
                currentSplash = s.panel;
            }
            else
            {
                s.panel.SetActive(false);
            }
        }
    }

    bool IsEndingRevealed(string id)
    {
        return id switch
        {
            "A" => GameData.finalARevealed,
            "B" => GameData.finalBRevealed,
            "C" => GameData.finalCRevealed,
            "D" => GameData.finalDRevealed,
            _ => false
        };
    }


    // === Cerrar splash con tap ===
    private void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        // Evitar que un tap inmediato active botones detrás
        if (Time.time - closeTime < 0.15f)
            return;
        
        if (currentSplash != null)
        {
            currentSplash.SetActive(false);
            currentSplash = null;
            closeTime = Time.time;

            // después de cerrar, reactivamos abrir splash
            Invoke(nameof(UnblockSplash), 0.3f);
        }
    }

    private void UnblockSplash()
    {
        splashBlocked = false;
    }
}

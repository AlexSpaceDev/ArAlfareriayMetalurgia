using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    [Header("Score Inputs")]
    public TMP_InputField alfareriaInput;
    public TMP_InputField metalurgiaInput;

    [Header("Final Toggles")]
    public Toggle finalAToggle;
    public Toggle finalBToggle;
    public Toggle finalCToggle;
    public Toggle finalDToggle;

    [Header("Final Reveal Toggles")]
    public Toggle revealAToggle;
    public Toggle revealBToggle;
    public Toggle revealCToggle;
    public Toggle revealDToggle;

    public void ApplyChanges()
    {
        // ---- Puntajes ----
        int parsed;
        if (int.TryParse(alfareriaInput.text, out parsed))
            GameData.scoreAlfareria = parsed;

        if (int.TryParse(metalurgiaInput.text, out parsed))
            GameData.scoreMetalurgia = parsed;

        // ---- Finales desbloqueados ----
        GameData.finalAUnlocked = finalAToggle.isOn;
        GameData.finalBUnlocked = finalBToggle.isOn;
        GameData.finalCUnlocked = finalCToggle.isOn;
        GameData.finalDUnlocked = finalDToggle.isOn;

        // ---- Cuáles ya fueron revelados en la pantalla de finales ----
        GameData.finalARevealed = revealAToggle.isOn;
        GameData.finalBRevealed = revealBToggle.isOn;
        GameData.finalCRevealed = revealCToggle.isOn;
        GameData.finalDRevealed = revealDToggle.isOn;

        Debug.Log("DEBUG MENU: Cambios aplicados correctamente.");
    }

    // Accesos rápidos
    public void UnlockTodo()
    {
        finalAToggle.isOn = true;
        finalBToggle.isOn = true;
        finalCToggle.isOn = true;
        finalDToggle.isOn = true;

        revealAToggle.isOn = true;
        revealBToggle.isOn = true;
        revealCToggle.isOn = true;
        revealDToggle.isOn = true;

        ApplyChanges();
    }

    public void ResetTodo()
    {
        alfareriaInput.text = "0";
        metalurgiaInput.text = "0";

        finalAToggle.isOn = false;
        finalBToggle.isOn = false;
        finalCToggle.isOn = false;
        finalDToggle.isOn = false;

        revealAToggle.isOn = false;
        revealBToggle.isOn = false;
        revealCToggle.isOn = false;
        revealDToggle.isOn = false;

        ApplyChanges();
    }

    // Pruebas directas
    public void TestFinalA() { GameData.finalToShow = 1; }
    public void TestFinalB() { GameData.finalToShow = 2; }
    public void TestFinalC() { GameData.finalToShow = 3; }
    public void TestFinalD() { GameData.finalToShow = 4; }
}


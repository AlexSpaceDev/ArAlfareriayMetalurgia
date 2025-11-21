using UnityEngine;

public static class GameData
{
    public static int scoreAlfareria = 0;
    public static int scoreMetalurgia = 0;

    // Indica si se entra directamente a una opción
    public static bool directMetalurgia = false;

    // Sistema de finales
    public static bool finalAUnlocked = false;
    public static bool finalBUnlocked = false;
    public static bool finalCUnlocked = false;
    public static bool finalDUnlocked = false;

    // Qué final se debe mostrar al entrar a la escena final
    public static int finalToShow = 0; // 0 ninguno, 1-A, 2-B, 3-C, 4-D
} 

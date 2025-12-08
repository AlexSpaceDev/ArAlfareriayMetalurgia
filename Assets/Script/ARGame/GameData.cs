using UnityEngine;

public static class GameData
{
    // Puntuaciones de cada camino
    public static int scoreAlfareria = 0;
    public static int scoreMetalurgia = 0;

    // Puntuación temporal durante el juego
    public static int tempScore = 0;
    public static int tempScoreAlfareria = 0;
    public static int tempScoreMetalurgia = 0;

    public static bool finishedAlfareria = false;
    public static bool finishedMetalurgia = false;

    // Indica si se entra directamente a una opción
    public static bool directMetalurgia = false;
    public static bool directAlfareria = false;

    // Sistema de finales
    public static bool finalAUnlocked = false;
    public static bool finalBUnlocked = false;
    public static bool finalCUnlocked = false;
    public static bool finalDUnlocked = false;

    // Qué final se debe mostrar al entrar a la escena final
    public static int finalToShow = 0; // 0 ninguno, 1-A, 2-B, 3-C, 4-D

    // Pantalla de finales para mostrar
    public static bool finalARevealed = false;
    public static bool finalBRevealed = false;
    public static bool finalCRevealed = false;
    public static bool finalDRevealed = false;

    // Nueva partida actual
    public static bool isPlaying = false;

    // Progreso temporal SOLO para la partida actual
    public static bool tempCompletedAlfareria = false;
    public static bool tempCompletedMetalurgia = false;


    public static void StartNewRun()
    {
        isPlaying = true;
        finalToShow = 0;
        directAlfareria = false;
        directMetalurgia = false;

        // NO tocamos finalAUnlocked ni finalCUnlocked (son globales)
    }

} 

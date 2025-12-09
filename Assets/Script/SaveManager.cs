using UnityEngine;

public static class SaveManager
{
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("scoreAlfareria", GameData.scoreAlfareria);
        PlayerPrefs.SetInt("scoreMetalurgia", GameData.scoreMetalurgia);

        PlayerPrefs.SetInt("finalAUnlocked", GameData.finalAUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("finalBUnlocked", GameData.finalBUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("finalCUnlocked", GameData.finalCUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("finalDUnlocked", GameData.finalDUnlocked ? 1 : 0);
        
        PlayerPrefs.SetInt("finalToShow", GameData.finalToShow);

        PlayerPrefs.SetInt("finalARevealed", GameData.finalARevealed ? 1 : 0);
        PlayerPrefs.SetInt("finalBRevealed", GameData.finalBRevealed ? 1 : 0);
        PlayerPrefs.SetInt("finalCRevealed", GameData.finalCRevealed ? 1 : 0);
        PlayerPrefs.SetInt("finalDRevealed", GameData.finalDRevealed ? 1 : 0);


        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt($"collectibleUnlocked_{i}", GameData.collectibleUnlocked[i] ? 1 : 0);
            PlayerPrefs.SetInt($"collectibleRevealed_{i}", GameData.collectibleRevealed[i] ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        if (!PlayerPrefs.HasKey("scoreAlfareria"))
        {
            // Si no existe aún, significa que es la primera vez que inicia la app
            return;
        }

        GameData.scoreAlfareria = PlayerPrefs.GetInt("scoreAlfareria");
        GameData.scoreMetalurgia = PlayerPrefs.GetInt("scoreMetalurgia");

        GameData.finalAUnlocked = PlayerPrefs.GetInt("finalAUnlocked") == 1;
        GameData.finalBUnlocked = PlayerPrefs.GetInt("finalBUnlocked") == 1;
        GameData.finalCUnlocked = PlayerPrefs.GetInt("finalCUnlocked") == 1;
        GameData.finalDUnlocked = PlayerPrefs.GetInt("finalDUnlocked") == 1;

        GameData.finalToShow = PlayerPrefs.GetInt("finalToShow");

        GameData.finalARevealed = PlayerPrefs.GetInt("finalARevealed") == 1;
        GameData.finalBRevealed = PlayerPrefs.GetInt("finalBRevealed") == 1;
        GameData.finalCRevealed = PlayerPrefs.GetInt("finalCRevealed") == 1;
        GameData.finalDRevealed = PlayerPrefs.GetInt("finalDRevealed") == 1;

        for (int i = 0; i < 8; i++)
        {
            GameData.collectibleUnlocked[i] = PlayerPrefs.GetInt($"collectibleUnlocked_{i}", 0) == 1;
            GameData.collectibleRevealed[i] = PlayerPrefs.GetInt($"collectibleRevealed_{i}", 0) == 1;
        }

    }

    public static void ResetCollectiblesInSave()
    {
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt($"collectibleUnlocked_{i}", 0);
            PlayerPrefs.SetInt($"collectibleRevealed_{i}", 0);
        }

        PlayerPrefs.Save();
    }

}

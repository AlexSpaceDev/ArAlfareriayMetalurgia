using UnityEngine;

public class EndingController : MonoBehaviour
{
    public GameObject finalA;
    public GameObject finalB;
    public GameObject finalC;
    public GameObject finalD;

    void Start()
    {
        finalA.SetActive(false);
        finalB.SetActive(false);
        finalC.SetActive(false);
        finalD.SetActive(false);

        switch (GameData.finalToShow)
        {
            case 1: finalA.SetActive(true); break;
            case 2: finalB.SetActive(true); break;
            case 3: finalC.SetActive(true); break;
            case 4: finalD.SetActive(true); break;
        }
    }
}

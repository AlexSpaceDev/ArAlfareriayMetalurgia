using UnityEngine;

public class EndWay : MonoBehaviour
{
    public bool EndAlfareriab;
    public bool EndMetalurgiab;

    public void Start()
    {
        if (EndAlfareriab)
        {
            EndAlfareria();
        }
        if (EndMetalurgiab)
        {
            EndMetalurgia();
        }
    }
    
    public void EndAlfareria()
    {
        GameData.tempCompletedAlfareria = true;
    }

    public void EndMetalurgia()
    {
        GameData.tempCompletedMetalurgia = true;
    }
}

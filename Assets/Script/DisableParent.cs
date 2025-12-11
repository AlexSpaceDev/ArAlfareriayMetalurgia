using UnityEngine;

public class DisableParent : MonoBehaviour
{
    public void DisableParentObject()
    {
        Transform parent = transform.parent;

        if (parent != null)
            parent.gameObject.SetActive(false);
        else
            Debug.LogWarning("Este objeto no tiene padre.");
    }
}

using UnityEngine;

public class PhoneInventory : MonoBehaviour
{
    public void SetupPhoneUI(GameObject inventoryUI)
    {
        inventoryUI.transform.parent = transform;
        inventoryUI.transform.localPosition = Vector3.zero;
        
        var rectTransform = inventoryUI.GetComponent<RectTransform>();
        if (rectTransform == null) return;

        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchorMin = Vector2.zero;
    }
}

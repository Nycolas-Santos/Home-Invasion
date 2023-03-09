using UnityEngine;

namespace Game.Scripts.Phone
{
    public class PhoneDialogue : MonoBehaviour
    {
        public void SetupPhoneUI(GameObject inventoryUI)
        {
        
            var rectTransform = inventoryUI.GetComponent<RectTransform>();
            if (rectTransform == null) return;
        
            rectTransform.SetParent(transform,false);
            inventoryUI.transform.localPosition = Vector3.zero;

            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.zero;
        }
    }
}
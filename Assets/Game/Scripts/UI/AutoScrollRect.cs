using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts
{
    [RequireComponent( typeof( ScrollRect ) )]
public class AutoScrollRect : MonoBehaviour 
{
    [Header( "Required: Children must have a Y Pivot of 1!" )]
    public float scrollSpeed = 10f;
 
    private ScrollRect scrollRect;
    private RectTransform rectTransform;
    private RectTransform contentRectTransform;
    private RectTransform selectedRectTransform;
       
    void Awake() {
        scrollRect = GetComponent<ScrollRect>();
        rectTransform = GetComponent<RectTransform>();
        contentRectTransform = scrollRect.content;
    }
 
    void Update() 
    {
        UpdateScrollToSelected();
    }
 
    void UpdateScrollToSelected()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
 
        if ( selected == null )
            return;

        if ( selected.transform.parent != contentRectTransform.transform )
            return;
 

        selectedRectTransform = selected.GetComponent<RectTransform>();

        float contentHeightDifference = GetContentHeightDifference();

        float selectedTop = selectedRectTransform.anchoredPosition.y;
        float selectedBottom = selectedTop - selectedRectTransform.rect.height;
        float viewportTop = NormalizedToPosition( scrollRect.verticalNormalizedPosition, contentHeightDifference );
        float viewportBottom = viewportTop - scrollRect.viewport.rect.height;

        if ( selectedTop > viewportTop )
        {
            float goalY = selectedTop;

            scrollRect.verticalNormalizedPosition = Mathf.Lerp( 
                scrollRect.verticalNormalizedPosition,
                PositionToNormalized( goalY, contentHeightDifference ),
                scrollSpeed * Time.deltaTime
            );
        }
        else if ( selectedBottom < viewportBottom && !Mathf.Approximately( selectedTop, selectedBottom ) )
        {
            float diff = selectedBottom - viewportBottom;
            float goalY = viewportTop + diff;

            scrollRect.verticalNormalizedPosition = Mathf.Lerp( 
                scrollRect.verticalNormalizedPosition,
                PositionToNormalized( goalY, contentHeightDifference ),
                scrollSpeed * Time.deltaTime
            );
        }
    }

    float GetContentHeightDifference ()
    {
        return contentRectTransform.rect.height - rectTransform.rect.height;
    }

    float NormalizedToPosition ( float normPos, float contentHeightDifference )
    {
        return ( normPos - 1.0f ) * contentHeightDifference;
    }

    float PositionToNormalized ( float pos, float contentHeightDifference )
    {
        return ( pos / contentHeightDifference ) + 1.0f;   
    }

    public bool IsClippingTop ()
    {
        return scrollRect.verticalNormalizedPosition < 1.0f;
    }
}
}

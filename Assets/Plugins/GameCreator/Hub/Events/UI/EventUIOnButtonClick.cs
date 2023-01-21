using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine.UI;

[Version(1, 0, 1)]

[Title("On Button Click")]
[Category("UI/On Button Click")]
[Description("Executed when the UI Button is Clicked/Submit")]

[Image(typeof(IconRadioOn),ColorTheme.Type.Green)]

[Keywords("Mouse","Choose","Submit","Click","Pick","Pointer")]
[Serializable]
public class EventUIOnButtonClick : Event
{
    public override Type RequiresComponent => typeof(Button);
    
    protected override void OnEnable(Trigger trigger)
    {
        base.OnEnable(trigger);
        if (this.m_Trigger.TryGetComponent(out Button button))
        {
            button.onClick.AddListener(OnButtonClick);        
        }
    }

    protected override void OnDisable(Trigger trigger)
    {
        base.OnDisable(trigger);
        if (this.m_Trigger.TryGetComponent(out Button button))
        {
            button.onClick.RemoveListener(OnButtonClick);        
        }
    }

    protected override void OnDestroy(Trigger trigger)
    {
        base.OnDestroy(trigger);
        if (this.m_Trigger.TryGetComponent(out Button button))
        {
            button.onClick.RemoveListener(OnButtonClick);        
        }
    }

    protected void OnButtonClick()
    {
        this.m_Trigger.Execute(this.Self);
    }
}

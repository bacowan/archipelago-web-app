namespace archipelago_web_app.Icons;

using Microsoft.AspNetCore.Components;

public abstract class IconBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
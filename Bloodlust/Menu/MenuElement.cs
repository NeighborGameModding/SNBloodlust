using MelonLoader;

namespace Bloodlust.Menu;

public abstract class MenuElement
{
    private bool _enabled;

    public readonly MelonEvent<bool> OnToggleElement = new();
    public string Name { get; private set; }
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (value == _enabled)
                return;

            _enabled = value;
            OnElementToggled(value);
            OnToggleElement.Invoke(value);
        }
    }

    public MenuElement(string name)
    {
        Name = name;
    }

    public abstract void Render();

    protected virtual void OnElementToggled(bool value) { }
}

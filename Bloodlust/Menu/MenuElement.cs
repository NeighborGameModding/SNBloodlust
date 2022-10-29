namespace Bloodlust.Menu;

public abstract class MenuElement
{
    public string Name { get; private set; }

    public MenuElement(string name)
    {
        Name = name;
    }

    public abstract void Render();
}

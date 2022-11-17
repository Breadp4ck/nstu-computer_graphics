using Lab1.Gui;

namespace Lab1.Control
{
    public abstract class VisualInstanceControl : Control, IGuiElement
    {
        public VisualInstanceControl(string name) : base(name) { }
    }
}
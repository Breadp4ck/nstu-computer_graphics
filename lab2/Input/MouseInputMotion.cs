using System.Numerics;

namespace Lab1.Input
{
    public class InputMouseMotion : InputEvent
    {
        public Vector2 Offset { get; init; } = Vector2.Zero;
        public InputMouseMotion(InputServer server, Vector2 offset) : base(server)
        {
            Offset = offset;
        }
    }
}
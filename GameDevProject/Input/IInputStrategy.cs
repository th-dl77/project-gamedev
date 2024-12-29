using Microsoft.Xna.Framework;

namespace GameDevProject.Input
{
    public interface IInputStrategy
    {
        Vector2 GetMovementInput();
        bool IsActionPressed(string action);
    }
}

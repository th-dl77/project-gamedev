using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Input
{
    public interface IInputStrategy
    {
        Vector2 GetMovementInput();
        bool IsActionPressed(string action);
        bool CheckFlip();
    }
}

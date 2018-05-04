using System;
using System.Drawing;
using System.Reflection;
using Gamex.src.Util;
using Gamex.src.Util.Size;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Logging;
using Gamex.src.XDGE;
using OpenTK.Input;

namespace Gamex.src.GameModel.Entities
{
    public interface Entity
    {
        void Draw(DrawAdapter drawAdapter);
        void Step();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel.Entities
{
    public class Human : GameEntity
    {
        public Human() : base(Sprites.Dude1, new GameSize(0.1f, 0.1f))
        {
            Solid = true;
        }
    }
}

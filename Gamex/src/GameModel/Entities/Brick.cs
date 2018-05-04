using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel.Entities
{
    class Brick : GameEntity
    {
        public Brick(Sprites sprite, GameSize size) : base(sprite, size)
        {
            Solid = true;
        }
    }
}

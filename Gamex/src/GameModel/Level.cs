using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Logging;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel
{
    public class Level
    {
        public List<GameEntity> Entities { get; } = new List<GameEntity>();

        public GameEntity MouseHack { get; }

        public Level()
        {
            MouseHack = new GameEntity(Sprites.FloorWood1, new GameSize(0.01f, 0.01f));
            MouseHack.Solid = true;
            MouseHack.Visible = false;
            Entities.Add(MouseHack);
            MouseHack.Collision += (sender, xd) => xd.Other.Visible = false;
        }
    }

    public class LevelBuilder
    {
        public Level Level { get; }

        private const float BlockSize = 0.1f;

        public LevelBuilder()
        {
            Level = new Level();
        }

        public void AddEntity(GameEntity entity, int x, int y)
        {
            entity.Location = new GameCoordinate(x*BlockSize, y*BlockSize);
            Level.Entities.Add(entity);
        }

        public void AddRoom(int x, int y, int width, int height)
        {
            Sprites s = Sprites.Brick1;

            for (int X = 0; X < width; X++)
            {
                var topBrick = new Brick(s, new GameSize(BlockSize, BlockSize));
                AddEntity(topBrick, x + X, y);

                var bottomBrick = new Brick(s, new GameSize(BlockSize, BlockSize));
                AddEntity(bottomBrick, x + X, y + height - 1);
            }

            for (int Y = 1; Y < height - 1; Y++)
            {
                var leftBrick = new Brick(s, new GameSize(BlockSize, BlockSize));
                AddEntity(leftBrick, x, y + Y);

                var rightBrick = new Brick(s, new GameSize(BlockSize, BlockSize));
                AddEntity(rightBrick, x + width - 1, y + Y);
            }

            for (int X = 1; X < width - 1; X++)
            {
                for (int Y = 1; Y < height - 1; Y++)
                {
                    var floor = new Floor(Sprites.FloorWood1, new GameSize(BlockSize, BlockSize));
                    AddEntity(floor, x + X, y + Y);
                }
            }
        }
    }
}

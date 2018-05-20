using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Logging;

namespace Gamex.src.GameModel
{
    public class GameState
    {
        public GameEntity Hero { get; }
        public Level CurrentLevel { get; set; }
        public Camera Camera { get; set; }

        public GameState()
        {
            // build the level
            LevelBuilder levelBuilder = new LevelBuilder();
            //levelBuilder.AddRoom(2, 3, 4, 4);
            levelBuilder.AddEntity(new Brick(Sprites.Brick1, new GameSize(0.1f, 0.1f)), 0, 0);
            levelBuilder.AddEntity(new Brick(Sprites.Brick1, new GameSize(0.1f, 0.2f)), 5, 5);
            // set the current level as the newly built level
            CurrentLevel = levelBuilder.Level;

            // create the hero
            Hero = new Human();
            Hero.Location = new GameCoordinate(0.0f, 0.3f);
            // add the hero
            CurrentLevel.Entities.Add(Hero);

            // set up the camera
            Camera = new FollowCamera(Hero);
        }

        public void Step()
        {
            if (CurrentLevel.Entities != null)
            {
                try
                {
                    HandleMovements();
                }
                catch (Exception)
                {
                    Logger.Default.Log("HandleMovements error'd");
                }

                foreach (var e in CurrentLevel.Entities)
                {
                    e.Step();
                }
            }
        }

        private void HandleMovements()
        {
            var movementInfos = Collision.CalculateMovements(CurrentLevel.Entities);

            foreach (var movementInfo in movementInfos)
            {
                movementInfo.Entity.Move(movementInfo.Movement);
            }
        }
    }

    public struct MovementInfo
    {
        public GameEntity Entity { get; }
        public MovementVector Movement { get; set; }

        public MovementInfo(GameEntity entity) : this()
        {
            Entity = entity;
        }
    }
}

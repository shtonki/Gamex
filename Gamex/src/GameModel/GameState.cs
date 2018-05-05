using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Polygon;
using Gamex.src.Util.Logging;

namespace Gamex.src.GameModel
{
    public class GameState
    {
        public GameEntity Hero { get; }
        public Level CurrentLevel { get; set; }
        public Camera Camera { get; set; }
        public GameEntity MouseTracker { get; }

        public GameState()
        {
            // build the level
            LevelBuilder levelBuilder = new LevelBuilder();
            levelBuilder.AddRoom(2, 3, 4, 4);
            // set the current level as the newly built level
            CurrentLevel = levelBuilder.Level;

            // create the hero
            Hero = new Human();
            Hero.Location = new GameCoordinate(0.0f, 0.3f);
            // add the hero
            CurrentLevel.Entities.Add(Hero);

            // set up the camera
            Camera = new FollowCamera(Hero);

            // 
            MouseTracker = new GameEntity(Sprites.FloorWood1, new GameSize(0.05f, 0.05f));
            MouseTracker.Solid = true;
            MouseTracker.Visible = false;
            CurrentLevel.Entities.Add(MouseTracker);
            MouseTracker.Collision += (sender, xd) =>
            {
                xd.Other.Tint = Color.Fuchsia;
            };
        }

        public void Step()
        {
            if (CurrentLevel.Entities != null)
            {
                try
                {
                    HandleCollisions();
                }
                catch (Exception)
                {
                    Logger.Default.Log("HandleCollisions error'd");
                }

                foreach (var e in CurrentLevel.Entities)
                {
                    e.Step();
                }
            }
        }

        private void HandleCollisions()
        {
            var unhandled = new HashSet<GameEntity>(CurrentLevel.Entities);
            var collisions = DetectCollisions(CurrentLevel.Entities);

            foreach (var collisionRecord in collisions)
            {
                var entityI = collisionRecord.EntityI;
                var entityJ = collisionRecord.EntityJ;

                entityI.Collide(entityJ, collisionRecord);
                entityJ.Collide(entityI, collisionRecord);

                var ICanMove = unhandled.Contains(entityI);
                var JCanMove = unhandled.Contains(entityJ);

                if (!ICanMove && !JCanMove) continue;


                var IMag = collisionRecord.MovementVectorI.Magnitude;
                var JMag = collisionRecord.MovementVectorJ.Magnitude;

                if ((IMag != 0 && JMag != 0) || (IMag == 0 && JMag == 0))
                {
                    continue;
                }

                GameEntity moving = null;
                Vector translationVector;

                if (ICanMove && IMag != 0)
                {
                    moving = entityI;
                    translationVector = collisionRecord.Result.MinimumTranslationVector;
                }
                else if (JCanMove && JMag != 0)
                {
                    moving = entityJ;
                    translationVector = -collisionRecord.Result.MinimumTranslationVector;
                }
                else
                {
                    Logger.Default.Log("fug #315");
                    translationVector = new Vector(); // compiler valium
                }

                if (moving != null)
                {
                    moving.Location = moving.Location.Add(translationVector.X, translationVector.Y);
                }
            }

            foreach (var entity in unhandled)
            {
                var movementvector = entity.CalculateMovementVector();
                entity.Location = entity.Location.Add(movementvector.X, movementvector.Y);
                //Logger.Default.Log(entity.Location.ToString());
            }
        }

        private IEnumerable<CollisionRecord> DetectCollisions(IEnumerable<GameEntity> entities)
        {
            var result = new List<CollisionRecord>();
            var entityList = entities.ToList();

            for (int i = 0; i < entityList.Count; i++)
            {
                var entityI = entityList[i];
                if (!entityI.Solid) continue;

                var vecI = entityI.CalculateMovementVector();

                for (int j = i+1; j < entityList.Count; j++)
                {
                    var entityJ = entityList[j];
                    if (!entityJ.Solid) continue;

                    var vecJ = entityJ.CalculateMovementVector();
                    var velocity = vecI - vecJ;
                    var collisionRecord = intersect.PolygonCollision(entityI.CollisionBounds, entityJ.CollisionBounds, velocity);

                    if (collisionRecord.Intersect || collisionRecord.WillIntersect)
                    {
                        result.Add(new CollisionRecord(entityI, vecI, entityJ, vecJ, collisionRecord));
                    }
                }
            }
            return result;
        }
    }


    /// <summary>
    /// When a collision is detected this struct is used to pass information about the collision around
    /// </summary>
    public struct CollisionRecord
    {
        public GameEntity EntityI { get; }
        public Vector MovementVectorI { get; }
        public GameEntity EntityJ { get; }
        public Vector MovementVectorJ { get; }

        public intersect.PolygonCollisionResult Result { get; }

        public CollisionRecord(GameEntity entityI, Vector movementVectorI, GameEntity entityJ, Vector movementVectorJ, intersect.PolygonCollisionResult result)
        {
            EntityI = entityI;
            MovementVectorI = movementVectorI;
            EntityJ = entityJ;
            MovementVectorJ = movementVectorJ;
            Result = result;
        }
    }
}

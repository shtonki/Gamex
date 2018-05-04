using System;
using System.Collections.Generic;
using System.Linq;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util;
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
            LevelBuilder levelBuilder = new LevelBuilder();
            levelBuilder.AddRoom(2, 3, 4, 4);
            CurrentLevel = levelBuilder.Level;

            Hero = new Human();
            Hero.Location = new GameCoordinate(0.0f, 0.3f);
            CurrentLevel.Entities.Add(Hero);

            Camera = new FollowCamera(Hero);
        }

        public void Step()
        {
            if (CurrentLevel.Entities != null)
            {
                try
                {
                    HandleCollisions();
                }
                catch (Exception e)
                {
                    int i = 2;
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
                for (int j = i+1; j < entityList.Count; j++)
                {
                    var entityJ = entityList[j];
                    if (!entityJ.Solid) continue;

                    var vecI = entityI.CalculateMovementVector();
                    var vecJ = entityJ.CalculateMovementVector();

                    var velocity = vecI - vecJ;

                    var pcr = intersect.PolygonCollision(entityI.CollisionBounds, entityJ.CollisionBounds, velocity);

                    if (pcr.Intersect || pcr.WillIntersect)
                    {
                        result.Add(new CollisionRecord(entityI, vecI, entityJ, vecJ, pcr));
                    }
                }
            }
            return result;
        }
    }

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

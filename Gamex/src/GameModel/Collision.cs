using System;
using System.Collections.Generic;
using System.Linq;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Logging;

namespace Gamex.src.GameModel
{
    public struct CollisionInfo
    {
        public GameEntity EntityA { get; }
        public GameEntity EntityB { get; }
        public MovementVector VelocityA { get; }
        public MovementVector VelocityB { get; }
        public bool Colliding { get; }
        public bool WillCollide { get; }

        public CollisionInfo(GameEntity entityA, GameEntity entityB, MovementVector velocityA, MovementVector velocityB, bool colliding, bool willCollide)
        {
            EntityA = entityA;
            EntityB = entityB;
            VelocityA = velocityA;
            VelocityB = velocityB;
            Colliding = colliding;
            WillCollide = willCollide;
        }
    }

    static class Collision
    {
        public static IEnumerable<MovementInfo> CalculateMovements(IEnumerable<GameEntity> entities)
        {
            var entityList = entities.ToList();
            var result = entityList.Select(e => new MovementInfo(e)).ToArray();

            for (int i = 0; i < entityList.Count; i++)
            {
                var entityI = entityList[i];
                var vecI = entityI.CalculateMovementVector();
                if (result[i].Movement == null) result[i].Movement = vecI;

                for (int j = i + 1; j < entityList.Count; j++)
                {
                    var entityJ = entityList[j];
                    var vecJ = entityJ.CalculateMovementVector();

                    var collisionRecord = CheckCollision(entityI, vecI, entityJ, vecJ);
                    if (collisionRecord.WillCollide || collisionRecord.Colliding)
                    {
                        result[i].Movement = collisionRecord.VelocityA;
                        result[j].Movement = collisionRecord.VelocityB;
                    }
                }
            }
            return result;
        }


        public static CollisionInfo CheckCollision
            (GameEntity entityA, MovementVector movementA, GameEntity entityB, MovementVector movementB)
        {
            var velocity = movementA - movementB;
            
            var entityWidths = entityA.Size.Width / 2 + entityB.Size.Width / 2;
            var entityHeights = entityA.Size.Height/2 + entityB.Size.Height/2;

            // pre movement
            var prediff = entityA.Location - entityB.Location;
            var gapXpre = Math.Abs(prediff.X) - entityWidths;
            var gapYpre = Math.Abs(prediff.Y) - entityHeights;
            var collisionPre =  (gapXpre < 0 && gapYpre < 0);
            
            // post movement
            var postdiff = (entityA.Location + movementA) - (entityB.Location + movementB);
            var gapXpost = Math.Abs(postdiff.X) - entityWidths;
            var gapYpost = Math.Abs(postdiff.Y) - entityHeights;
            var collisionPost = (gapXpost < Math.Abs(velocity.X) && gapYpost < Math.Abs(velocity.Y));


            if (collisionPre || collisionPost)
            {
                var shareAX = Math.Abs(movementA.X)/(Math.Abs(movementA.X) + Math.Abs(movementB.X));
                var shareBX = 1 - shareAX;
                var shareAY = Math.Abs(movementA.Y)/(Math.Abs(movementA.Y) + Math.Abs(movementB.Y));
                var shareBY = 1 - shareAY;

                var direction = Math.Atan2(velocity.Y, velocity.X)% (Math.PI * 2);
                // rotate everything 45 degreesso it's north, south, east, and west
                direction += Math.PI + Math.PI/4;   


                string i = "";

                if (direction < Math.PI/2)          // A to the right of B
                {
                    shareBX = -shareBX;
                }
                else if (direction < Math.PI)       // down
                {
                    i = "down";
                }
                else if (direction < 1.5*Math.PI)   // A to the left of B
                {
                    i = "left";
                }
                else                                // up
                {
                    i = "up";
                }

                Logger.Default.Log("{0} {1}", direction, i);

                movementA = new MovementVector(
                    shareAX * gapXpre,
                    shareAY * gapYpre);
                movementB = new MovementVector(
                    shareBX * gapXpre,
                    shareBY * gapYpre);
            }

            return new CollisionInfo(
                entityA,
                entityB,
                movementA,
                movementB,
                collisionPre,
                collisionPost
                );
        }

    }
}

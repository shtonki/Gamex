using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;

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
            
            // pre movement
            var prediff = entityA.Location - entityB.Location;
            var gapXpre = Math.Abs(prediff.X) - entityA.Size.Width / 2 - entityB.Size.Width / 2;
            var gapYpre = Math.Abs(prediff.Y) - entityA.Size.Height / 2 - entityB.Size.Height / 2;
            var collisionPre =  (gapXpre < 0 && gapYpre < 0);
            
            // post movement
            var postdiff = (entityA.Location + movementA) - (entityB.Location + movementB);
            var gapXpost = Math.Abs(postdiff.X) - entityA.Size.Width / 2 - entityB.Size.Width / 2;
            var gapYpost = Math.Abs(postdiff.Y) - entityA.Size.Height / 2 - entityB.Size.Height / 2;
            var collisionPost = (gapXpost < Math.Abs(velocity.X) && gapYpost < Math.Abs(velocity.Y));


            if (collisionPre || collisionPost)
            {
                movementA = MovementVector.Zero;
                movementB = MovementVector.Zero;
                if (prediff.X > 0) // A.X > B.X
                {
                    
                }
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

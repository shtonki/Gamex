using System;
using System.Collections.Generic;
using System.Drawing;
using Gamex.src.Util.Settingsx;
using Gamex.src.Util.Size;
using Gamex.src.Util.Coordinate;
using Gamex.src.XDGE;
using Gamex.src.GameModel;

namespace Gamex.src.GameModel.Entities
{

    public class GameEntity : Entity
    {
        public GameCoordinate Location { get; set; }


        public BoundingRectangle CollisionBounds { get; set; }
        public BoundingRectangle CollisionBoundsTranslated => new BoundingRectangle(Location.X, Location.Y, CollisionBounds.Width, CollisionBounds.Height);

        public bool Solid { get; set; }

        public bool Visible { get; set; } = true;
        public Sprites Sprite { get; set; }
        public GameSize Size { get; set; }
        public Color Tint { get; set; } = Color.White;

        public float BaseSpeed { get; set; } = 0.01f;
        public float Facing { get; private set; }
        public GameCoordinate MoveTo { get; set; }

        public event EventHandler<CollisionInfo> Collision;

        public GameEntity()
        {
            Location = new GameCoordinate(0, 0);
        }

        public GameEntity(Sprites sprite, GameSize size) : this()
        {
            Sprite = sprite;
            Size = size;
            CollisionBounds = new BoundingRectangle(Size.Width, Size.Height);
        }

        public void Move(MovementVector movementVector)
        {
            Location = Location.Add(movementVector.X, movementVector.Y);
        }

        public MovementVector CalculateMovementVector()
        {
            if (MoveTo == null) // if we're not going anywhere
            {
                return MovementVector.Zero;
            }

            if (Location.Distance(MoveTo) < 0.02f) // if we're close enough to where we are going
            {
                MoveTo = null;
                return MovementVector.Zero;
            }

            var heroCoordinate = Location;
            var diffCoordinate = MoveTo - heroCoordinate;
            var angleInRadians = Math.Atan2(diffCoordinate.X, diffCoordinate.Y);
            var angleInDegrees = angleInRadians * 180 / Math.PI;
            Facing = (float)angleInDegrees;

            var speed = BaseSpeed;

            var xd = speed * Math.Sin(angleInRadians);
            var yd = speed * Math.Cos(angleInRadians);
            return new MovementVector((float)xd, (float)yd);
        }


        public virtual void Draw(DrawAdapter drawAdapter)
        {
            if (Size == null) // hack
            {
                return;
            }

            if (!Visible)
            {
                return;
            }

            var glLocation = Location.ToGLCoordinate();

            drawAdapter.DrawImage(
                new ImageX(Tint, Sprite, (GLSize)Size),
                glLocation, 
                Facing,
                DrawAdapter.DrawMode.Centered);

            if (Settings.Tree.Debug.ShowSize && Solid)
            {
                drawAdapter.TracePolygon(
                    Color.Fuchsia,
                    glLocation - new GLCoordinate(Size.Width/2, Size.Height/2),
                    0,
                    CollisionBounds.Edges);
            }

            if (Settings.Tree.Debug.ShowMoveTo && MoveTo != null)
            {
                var moveToGLCoord = MoveTo.ToGLCoordinate();
                drawAdapter.FillRectangle(
                    Color.Gold, 
                    moveToGLCoord.X, 
                    moveToGLCoord.Y,
                    0.01f,
                    0.01f
                    );
            }
        }

        public virtual void Step()
        {
        }

        public virtual void Collide(GameEntity other, CollisionInfo collisionInfo)
        {
            Collision?.Invoke(this, collisionInfo);
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }
    }

    public class BoundingRectangle
    {
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public IEnumerable<GLCoordinate> Edges => new GLCoordinate[]
        {
            new GLCoordinate(X+0    , Y+0     ), 
            new GLCoordinate(X+Width, Y+0     ), 
            new GLCoordinate(X+Width, Y+Height), 
            new GLCoordinate(X+0    , Y+Height), 
        };

        public BoundingRectangle(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

    }

    public class MovementVector
    {
        public float X { get; }
        public float Y { get; }

        public MovementVector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static MovementVector Zero => new MovementVector(0, 0);

        public static MovementVector operator -(MovementVector v1, MovementVector v2)
        {
            return new MovementVector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public override string ToString()
        {
            return String.Format("<X:{0} Y:{1}>", X, Y);
        }
    }
}

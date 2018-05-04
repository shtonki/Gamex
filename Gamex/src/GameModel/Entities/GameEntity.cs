using System;
using System.Drawing;
using Gamex.src.Util;
using Gamex.src.Util.Size;
using Gamex.src.Util.Coordinate;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel.Entities
{
    public class GameEntity : Entity
    {
        public GameCoordinate Location
        {
            get { return _Location; }
            set
            {
                //update the offset of the collision bounds
                //if it gets out of what this code is probably why
                if (_Location != null && value != null && CollisionBounds != null)
                {
                    var diff = value - _Location;
                    CollisionBounds.Offset(diff.X, diff.Y);
                }
                _Location = value;
            }
        }

        public bool Solid { get; set; }

        public Polygon CollisionBounds { get; set; }

        public bool Visible { get; set; } = true;
        public Sprites Sprite { get; set; }
        public GameSize Size { get; set; }
        public Color Tint { get; set; } = Color.White;

        public float BaseSpeed { get; set; } = 0.01f;
        public float Facing { get; private set; }
        public GameCoordinate MoveTo { get; set; }


        private GameCoordinate _Location { get; set; }

        public event EventHandler<xd> Collision;

        public GameEntity()
        {
            Location = new GameCoordinate(0, 0);
        }

        public GameEntity(Sprites sprite, GameSize size) : this()
        {
            CollisionBounds = Bounds.MakeRectangle(size.Width, size.Height);
            Sprite = sprite;
            Size = size;
        }

        public Vector CalculateMovementVector()
        {
            if (MoveTo == null) // if we're not going anywhere
            {
                return Vector.Zero;
            }

            if (Location.Distance(MoveTo) < 0.02f) // if we're close enough to where we are going
            {
                MoveTo = null;
                return Vector.Zero;
            }

            var heroCoordinate = Location;
            var diffCoordinate = MoveTo - heroCoordinate;
            var angleInRadians = Math.Atan2(diffCoordinate.X, diffCoordinate.Y);
            var angleInDegrees = angleInRadians * 180 / Math.PI;
            Facing = (float)angleInDegrees;

            var speed = BaseSpeed;

            var xd = speed * Math.Sin(angleInRadians);
            var yd = speed * Math.Cos(angleInRadians);
            return new Vector((float)xd, (float)yd);
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

            if (Settings.SettingsTree.Debug.ShowSize && Solid)
            {
                drawAdapter.TraceRectangle(
                    Color.Fuchsia, 
                    glLocation.X - Size.Width/2, 
                    -glLocation.Y - Size.Height/2,
                    Size.Width, Size.Height);
                ;
            }

            if (Settings.SettingsTree.Debug.ShowMoveTo && MoveTo != null)
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

        public virtual void Collide(GameEntity other, CollisionRecord collisionRecord)
        {
            Collision?.Invoke(this, new xd(this, other, collisionRecord));
        }

        protected abstract class Bounds
        {
            private Polygon polygon;

            public static Polygon MakeRectangle(float width, float height)
            {
                return new Polygon(new []
                {
                    new Vector(0, 0),
                    new Vector(width, 0),
                    new Vector(width, height),
                    new Vector(0, height),
                });
            }
        }

        public struct xd
        {
            public GameEntity Self { get; }
            public GameEntity Other { get; }
            public CollisionRecord CollisionRecord { get; }

            public xd(GameEntity self, GameEntity other, CollisionRecord collisionRecord)
            {
                Self = self;
                Other = other;
                CollisionRecord = collisionRecord;
            }
        }
    }
}

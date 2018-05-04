using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Coordinate;

namespace Gamex.src.GameModel
{
    public class Zoomer
    {
        public float CurrentZoom { get; set; } = 1;

        private float MinZoom { get; } = 0.4f;
        private float MaxZoom { get; } = 1.8f;
        private float ZoomStepSize { get; } = 0.2f;

        public void ZoomIn()
        {
            if (CurrentZoom - ZoomStepSize < MinZoom) CurrentZoom = MinZoom;
            else CurrentZoom -= ZoomStepSize;
        }

        public void ZoomOut()
        {
            if (CurrentZoom + ZoomStepSize > MaxZoom) CurrentZoom = MaxZoom;
            else CurrentZoom += ZoomStepSize;
        }
    }

    public interface Camera
    {
        GameCoordinate Location { get; }
        Zoomer Zoom { get; }
    }

    public class FollowCamera : Camera
    {
        public Zoomer Zoom { get; } = new Zoomer();

        private GameEntity Following;

        public FollowCamera(GameEntity following)
        {
            Following = following;
        }

        public GameCoordinate Location
        {
            get
            {
                if (Following?.Location == null) return new GameCoordinate(0, 0);
                return Following.Location;
            }
        }
    }

    public class StaticCamera : Camera
    {
        public GameCoordinate Location { get; }
        public Zoomer Zoom { get; } = new Zoomer();

        public StaticCamera(GameCoordinate origin)
        {
            Location = origin;
        }
    }
}
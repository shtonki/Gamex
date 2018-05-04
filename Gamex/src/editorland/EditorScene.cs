using System;
using System.Collections.Generic;
using System.Linq;
using Gamex.src.GameModel;
using Gamex.src.GameModel.Entities;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.editorland
{
    class EditAction
    {
        ActionType ActionType;
        int EntityID;

        public EditAction(ActionType actionType, int entityId)
        {
            ActionType = actionType;
            EntityID = entityId;
        }

        public EditAction(ActionType actionType, EditorEntity e)
        {
            ActionType = actionType;
            EntityID = e.id;
        }
    }

    enum ActionType
    {
        Rotated,
        Selected,
        Moved,
        Created,
    }

    class EditorScene : Scene
    {
        public UIEntity EntityWindow { get; }
        public List<EditAction> EditActions;
        public Camera Camera = new StaticCamera(new GameCoordinate(0, 0));

        public EditorScene()
        {
            EntityWindow = new UIEntity(Sprites.Brick1, new GLSize(0.1f, 1.8f));
            EntityWindow.Location = new GLCoordinate(0.8f, -0.9f);

#if false
            int i = 0;
            foreach (var sprite in Enum.GetValues(typeof(Sprites)).Cast<Sprites>())
            {
                EditorEntity e = new EditorEntity(sprite, new GameSize(0.2f, 0.2f));
                e.Location = new GLCoordinate(0, 0.2f*i);
                EntityWindow.AddChild(e);
                i++;
            }
#endif
        }

        private void SetupWindows()
        {
        }

        private UIEntity SetEntityWindow()
        {
            UIEntity e = new UIEntity(Sprites.Brick1, new GLSize(0.2f, 2f));
            e.Location = new GLCoordinate(1f-0.2f/2, 0);
            return e;
        }

        private void PopulateWindows()
        {
            var allSprites = Enum.GetValues(typeof(Sprites));
            int i = 0;
            foreach (Sprites s in allSprites)
            {
                EditorEntity e = new EditorEntity(s, new GLSize(0.2f, 0.2f));
                e.Location = new GLCoordinate(0.2f, 0.2f * i);

                EntityWindow.AddChild(e);
                i++;
            }
        }


        public override void Draw(DrawAdapter adapter)
        {
            EntityWindow.Draw(adapter);
        }


        protected override void SetupInputBindings()
        {
        }
    }
}
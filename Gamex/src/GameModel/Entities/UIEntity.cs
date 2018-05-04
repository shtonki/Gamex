using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Util;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel.Entities
{
    class UIEntity : Entity
    {
        public GLCoordinate Location { get; set; }
        public Sprites Sprite { get; set; }
        public GLSize Size { get; set; }
        public List<UIEntity> Children { get; }= new List<UIEntity>();
        public UIEntity Parent { get; private set; }

        public UIEntity(Sprites sprite, GLSize size)
        {
            Sprite = sprite;
            Size = size;
            Location = new GLCoordinate(0, 0);
        }
        
        public virtual void Step()
        {
            
        }

        public void AddChild(UIEntity e)
        {
            if (e.Parent != null) throw new GameBrokenException("parenting issue");

            e.Parent = this;
            Children.Add(e);
        }

        public virtual void Draw(DrawAdapter drawAdapter)
        {
            drawAdapter.DrawImage(
                new ImageX(Sprite, Size),
                Location,
                0,
                DrawAdapter.DrawMode.TopLeft);

            if (Children.Count == 0)
            {
                return;
            }

            drawAdapter.PushMatrix();
            drawAdapter.Translate(Location);

            foreach (var child in Children)
            {
                child.Draw(drawAdapter);
            }

            drawAdapter.PopMatrix();
        }

        public void Remove(UIEntity e)
        {
            //todo
            if (Children.Contains(e))
            {
                Children.Remove(e);
            }
        }

        public void RemoveAll()
        {
            //todo
        }
    }
}
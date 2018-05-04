using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.editorland;
using Gamex.src.Util.Size;
using Gamex.src.XDGE;

namespace Gamex.src.GameModel.Entities
{
    class EditorEntity : UIEntity, ISelectable
    {
        private static int entityID = 0;
        public int id;

        public EditorEntity(Sprites sprite, GLSize size) : base(sprite, size)
        {
            this.id = entityID++;
        }

        public void OnClick()
        {
            throw new NotImplementedException();
        }
    }
}

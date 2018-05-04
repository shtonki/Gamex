using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Controller.Graphics;
using Gamex.src.editorland;

namespace Gamex.src.Controller
{
    static class EditorController
    {
        private static EditorScene editor;

        public static void InitEditor()
        {
            editor = new EditorScene();
            GraphicsController.ActiveScene = editor;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;

namespace Cubo3
{
    class WMFileNameEditor: System.Windows.Forms.Design.FileNameEditor
    {
        protected override void InitializeDialog(System.Windows.Forms.OpenFileDialog openFileDialog)
        {
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Jpg (*.jpg)|*.jpg|Jpeg(*.jpeg)|*.jpeg|Bitmap(*.bmp)|*.bmp|Png(*.png)|*.png";
            openFileDialog.Title = "Seleccionar textura";
        }
    }
}

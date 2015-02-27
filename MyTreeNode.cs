using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cubo3
{
    public class MyTreeNode :TreeNode
    {
        public WMTipoElemento item;
        public Boolean infLock=false;
        public Boolean supLock = false;
        public int childSelect=0;
        public MyTreeNode(WMTipoElemento objecto,String nome):base(nome)
        {

            item = objecto;
            if (objecto is WMAssembly)
            {
                this.ImageIndex = 0;
                this.SelectedImageIndex = 0;
            }
            else
            {
                this.ImageIndex = 1;
                this.SelectedImageIndex = 1;
            }
            this.Checked = item.compilar;
        }
        public Boolean isPeca()
        {
            return item is WMPeca;
        }
    
    }
}
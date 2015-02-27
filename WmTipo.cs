using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    public abstract class WmTipo
    {
        public string nomeClassAs;
        public string path;
        public abstract void criarActionScript();
        public static void gerarAS3()
        {
            WMTipoPeca.gerarAs3Files();
            WMTipoAssembly.gerarAs3Files();
        }
    }

}

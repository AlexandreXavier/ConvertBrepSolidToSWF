using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;

namespace Cubo3
{
    public class WMTipoAssembly : WmTipo
    {
        //Lista com todos os tipos de assembly diferentes
        public static List<WMTipoAssembly> TipoAssembly = new List<WMTipoAssembly>();
        //Lista de todos os assemblys instancias deste tipo
        public List<WMAssembly> mesmoTipo = new List<WMAssembly>();
        //Guarda o caminho do assembly
        
       
        public WMTipoAssembly(String iamPath, String nomeClassAs)
        {
            this.path = iamPath;
            this.nomeClassAs = nomeClassAs;
        }

        public static WMTipoAssembly addTipoAssembly(String iamPath, String nome)
        {
            WMTipoAssembly tipoAssemblyRes;
            int indexRes = verificarExistencia(iamPath);
            try
            {
                if (indexRes == -1)
                {
                    String nomeClassAs;
                    nomeClassAs="AP_"+WmServicos.getNomeClassePrincipal(iamPath);
                    nomeClassAs = WmServicos.limpaString(nomeClassAs);
                    tipoAssemblyRes = new WMTipoAssembly(iamPath, nomeClassAs);
                    TipoAssembly.Add(tipoAssemblyRes);
                
                }
                else
                {
                    tipoAssemblyRes = TipoAssembly[indexRes];
                }
                return tipoAssemblyRes;
            }
            catch (Exception erro)
            {
                String message = erro.Message;
            }
            return null;
        }

        public static int verificarExistencia(String iamPath)
        {
            int res = -1;
            for (int i = 0; i < TipoAssembly.Count; i++)
            {
                if (TipoAssembly[i].path.Equals(iamPath))
                {
                    res = i;
                    break;
                }
            }
            return res;
        }

        public override void criarActionScript()
        {
            Console.WriteLine("Começou assembly principal:" + this.nomeClassAs);
            SaveAssemblyPrincipal temp = new SaveAssemblyPrincipal(nomeClassAs);
            Console.WriteLine("Terminou assembly principal:" + this.nomeClassAs);
        }
        public static void gerarAs3Files()
        {
            for (int i = 0; i < WMTipoAssembly.TipoAssembly.Count; i++)
            {
                //Thread temp = new Thread(new ThreadStart(WMTipoAssembly.TipoAssembly[i].criarActionScript));
                //temp.Start();
                //frmCubo.thread.Add(temp);
                WMTipoAssembly.TipoAssembly[i].criarActionScript();
            }
        }
    }
}


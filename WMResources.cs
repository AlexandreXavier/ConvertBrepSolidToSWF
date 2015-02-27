using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    class WMResources
    {
        private static Random rNum = new Random();
        public static List<WMresource> lista = new List<WMresource>();
        public static WMresource addResource(String path){
            Boolean existePath=false;
            Boolean existeNome=false;
            String nomeFicheiro= System.IO.Path.GetFileName(path);
            int res=-1;
            WMresource temp=null;
            for (int i=0; i < lista.Count; i++)
            {
                if(lista[i].path.Equals(path)){
                    existePath=true;
                    res=i;
                    break;
                }
                if(lista[i].nomeFinal.Equals(nomeFicheiro)){
                    existeNome = true;
                }
            }
            if (existePath)
            {
                temp=lista[res];
            }
            if (existeNome)
            {
                nomeFicheiro = nomeFicheiro + rNum.Next(0, 999);
            }
            if (temp == null)
            {
                temp=new WMresource(path, nomeFicheiro);
                lista.Add(temp);
            }
            if (!temp.carregado)
            {
                temp.copyFile();
            }
            //lista.Add(temp);
            return temp;
        }
        public static void removeResource(WMresource resource)
        {
            resource.delFile();
            lista.Remove(resource);
        }
    }
}

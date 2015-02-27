using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    public class WMresource
    {
        public List<WMTipoElemento> items = new List<WMTipoElemento>();
        public String path;
        public String nomeFinal;
        public Boolean carregado = false;
        public WMresource(String path, String nomeFinal)
        {
            this.path = path;
            this.nomeFinal = nomeFinal;
        }
        
        
        public Boolean copyFile()
        {
            Boolean res = true;
            try{
                String pathDest = System.IO.Path.Combine(System.IO.Path.Combine(Config.pathProjecto,"resources"),nomeFinal);
                if(System.IO.File.Exists(path)){
                    System.IO.File.Delete(pathDest);
                }
                System.IO.File.Copy(path,pathDest);
            }catch(Exception erro)
            {
                res=false; 
            }
            carregado = res;
            return res;
        }
        public Boolean delFile()
        {
            Boolean res = true;
            try
            {
                System.IO.File.Delete(System.IO.Path.Combine(System.IO.Path.Combine(Config.pathProjecto, "resources"), nomeFinal));

            }
            catch (Exception erro)
            {
                res = false;
            }
            return res;
        }

        public Boolean removeItem(WMTipoElemento item)
        {            
            //int res = -1;
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (items[i].caminho.Equals(caminho))
            //    {
            //        res = 1;
            //        break;
            //    }
            //}
            items.Remove(item);
            if (items.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string getResourcePath()
        {
            return @"resources\" + nomeFinal;
        }
    }
}

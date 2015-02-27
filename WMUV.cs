using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    public class WMUV
    {

        public double U;
        public double V;

        public WMUV(double u, double v)
        {
            setUV(u, v);
        }
        public void setU(double u)
        {
            this.U = Math.Round(u, Config.nCasaDecimaisUV);
        }
        public void setV(double v)
        {
            this.V = Math.Round(v, Config.nCasaDecimaisUV);
        }
        public void setUV(double u, double v)
        {
            setU(u);
            setV(v);
        }
        // Compara os UV 
        public bool Compara(WMUV UV)
        {
            bool functionReturnValue = false;
            if (UV.U == U & UV.V == V)
            {
                functionReturnValue = true;
            }
            else
            {
                functionReturnValue = false;
            }
            return functionReturnValue;

        }
        public override String ToString()
        {
            String temp = "uv(" + U + "?" + V + ");";
            temp += Environment.NewLine;
            temp = temp.Replace(',', '.');
            temp = temp.Replace('?', ',');
            temp.Trim();
            return temp;
        }

    } 
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Class que vai defenir os vertices 
namespace Cubo3
{
    public class WMVertice
    {
        //public static double xMin;
        //public static double xMax;
        //public static double yMin;
        //public static double yMax;
        //public static double zMin;
        //public static double zMax;
        //public static Boolean primeiroX = true;
        //public static Boolean primeiroY = true;
        //public static Boolean primeiroZ = true;
        //public static WMVertice centro;
        
        //public static FileStream ficheiro=new FileStream(Config.pathProjecto + "vertices.txt", FileMode.Create);
        //public static StreamWriter escrever= new StreamWriter(ficheiro);


        public double X;
        public double Y;
        public double Z;

        public WMVertice(double x, double y, double z)
        {
            setXYZ(x, y, z);
            //escrever.WriteLine("" + this.X + ";" + this.Y + ";" + this.Z);
        }
        public void setX(double x)
        {
            this.X = Math.Round(x, Config.nCasaDecimaisVertices);
            //if (primeiroX)
            //{
            //    xMax = this.X;
            //    xMin = this.X;
            //    primeiroX = false;
            //}
            //else if (this.X > xMax)
            //{
            //    xMax = this.X;
            //}
            //else if (this.X < xMin)
            //{
            //    xMin = this.X;
            //}
        }
        public void setY(double y)
        {
            this.Y = Math.Round(y, Config.nCasaDecimaisVertices);
            //if (primeiroY)
            //{
            //    yMax = this.Y;
            //    yMin = this.Y;
            //    primeiroY = false;
            //}
            //else if (this.Y > yMax)
            //{
            //    yMax = this.Y;
            //}
            //else if (this.Y < yMin)
            //{
            //    yMin = this.Y;
            //}
        }
        public void setZ(double z)
        {
            this.Z = Math.Round(z, Config.nCasaDecimaisVertices);
            //if (primeiroZ)
            //{
            //    zMax = this.Z;
            //    zMin = this.Z;
            //    primeiroZ = false;
            //}
            //else if (this.Z > zMax)
            //{
            //    zMax = this.Z;
            //}
            //else if (this.Z < zMin)
            //{
            //    zMin = this.Z;
            //}      
        }
        public void setXYZ(double x, double y,double z)
        {
            setX(x);
            setY(y);
            setZ(z);
        }
        // Compara os vertices 
        public bool Compara(WMVertice Vertice)
        {
            bool functionReturnValue = false;
            if (Vertice.X == X && Vertice.Y == Y && Vertice.Z == Z)
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
            String temp = "v(" + X + "?" + Y + "?" + Z + ");";
            temp += Environment.NewLine;
            temp = temp.Replace(',', '.');
            temp = temp.Replace('?', ',');
            temp.Trim();
            return temp;
        }
        //public String xToAs()
        //{
        //    return X.ToString().Replace('.', ',');
        //}
        //public String yToAs()
        //{
        //    return Y.ToString().Replace('.', ',');
        //}
        //public String zToAs()
        //{
        //    return Z.ToString().Replace('.', ',');
        //}
        //public static void calcularCentroMassa()
        //{
        //    centro = new WMVertice((xMax + xMin) / 2, (yMax + yMin) / 2, (zMax + zMin) / 2);
        //    escrever.WriteLine("Centro="+centro.X + ";" + centro.Y + "," + centro.Z);
        //}
        public void somarVertice(WMVertice t)
        {
            this.X += t.X;
            this.Y += t.Y;
            this.Z += t.Z;
        }
        public void subVertice(WMVertice t)
        {
            this.X -= t.X;
            this.Y -= t.Y;
            this.Z -= t.Z;
        }
        public WMVertice negVertice()
        {
            return new WMVertice(-this.X, -this.Y, -this.Z);
        }
    } 
}

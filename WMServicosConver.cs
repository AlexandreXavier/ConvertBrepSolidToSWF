using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    class WMServicosConver
    {
        public static String arrayToString(Array transf)
        {
            String res = ""+Math.Round((Double)transf.GetValue(0), Config.nCasaDecimaisMatrizTrans);
            for (int i = 1; i < 12; i++)
            {
                res += "?" + Math.Round((Double)transf.GetValue(i), Config.nCasaDecimaisMatrizTrans);
            }
            res = res.Replace(",", ".");
            res = res.Replace("?", ",");
            return res; 
        }
        
        public static List<int> converterStringEmArray(String caminho)
		{
            char[] splitter = { '|' };
		    Array Temp = caminho.Split(splitter);
			
            
            return converterArrayEmArrayNumerico(Temp);

		}
        public static Array converterMatrixStringEmArray(String matrixString)
        {

            //Inventor.Matrix copia = WMServicosMatrix.transGeo.CreateMatrix();

            matrixString = matrixString.Replace(".", ",");
            String[] splitter = { "\\n" ,"\t\t"};
            Array Temp = matrixString.Split(splitter,System.StringSplitOptions.RemoveEmptyEntries);
            Array array = new Double[16];
            for (int i = 0; i < 16; i++)
            {
                array.SetValue((double.Parse((String)Temp.GetValue(i))), i);

            }

            //Array temp2;
            //List<double> temp2;

              //for(int i =0; i<Temp.Length;i++){
              //    temp2=WMServicosConver.converterArrayEmArrayNumerico(((String)Temp.GetValue(i)).Split(splitter2, System.StringSplitOptions.RemoveEmptyEntries));
              //    for (int j = 0; j < 4; j++)
              //    {
              //        copia.set_Cell(i,j,
              //    }

              //}


            return array;
        }
        public static List<int> converterArrayEmArrayNumerico(Array ar)
		{
            List<int> temp = new List<int>(ar.Length);
            for (int i = 0; i < ar.Length; i++)
			{
                temp.Add(int.Parse((String)ar.GetValue(i)));
			}
            return temp;
		}
        public static List<double> converterArrayEmArrayNumericoDouble(Array ar)
        {
            List<double> temp = new List<double>(ar.Length);
            for (int i = 0; i < ar.Length; i++)
            {
                temp.Add(double.Parse((String)ar.GetValue(i)));
            }
            return temp;
        }
        public static string numToAs3(Double t){
            
            return Math.Round(t,Config.nCasaDecimaisVertices).ToString().Replace(',', '.');
        }
        public static Double as3ToNum(String t)
        {
            return Double.Parse(t.Replace('.', ','));
        }


    }
}

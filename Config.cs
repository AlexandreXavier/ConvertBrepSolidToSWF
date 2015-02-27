using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Cubo3
{
    public static class Config
    {
        public static String pathHome = @"c:\temp\";
        public static String pathProjecto = "c:\\temp\\temp3\\";
        public static String pathTrabalho = "c:\\temp\\temp3\\";
        public static String projectNome;
        public static String caminhoSwf;
        public static int versao = 1;
        public static int versao1 = 1;
        public static int versao2 = 2;

        public static string INSTAL_PV3D = @"C:\temp\temp3\teste-pv20\";
        public static string INSTAL_CLASS = @"C:\Program Files\FlashDevelop\Library\AS3\classes";
        public static string TEMPLATE_COLOR = "#FFFFFF";
        public static int TEMPLATE_RATE = 30;
        public static int TEMPLATE_HEIGHT = 600;
        public static int TEMPLATE_WIDTH = 800;
        public static string sdkExe = String.Format("C:\\temp\\temp3\\teste-pv20\\flex_sdk_3\\bin\\mxmlc.exe");

        public static int nCasaDecimaisMatrizTrans = 5;
        public static int nCasaDecimaisUV = 6;
        public static int nCasaDecimaisVertices = 6;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace Cubo3
{
    class WmServicos
    {
        public static System.Array MinPoint = new double[3];
        public static System.Array MaxPoint = new double[3];
        public static WMVertice centro;
        public static Flash.External.ExternalInterfaceProxy proxy;
        public static  AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
        public static String limpaString(String texto)
        {
            String res = Regex.Replace(texto, @"[!£#@$§%&/{()=}?'»«´`~^ºª*+¨:.;,ç><]", "");
            res = res.Replace(@"\", "");
            res = res.Replace("\"", "");
            res = res.Replace("-", "");
            res = res.Replace("[", "");
            res = res.Replace("]", "");
            res = res.Replace("|", "_");
            return res;
        }
        
        public static WMVertice calcularCentro()
        {
            return new WMVertice(((double)MaxPoint.GetValue(0) + (double)MinPoint.GetValue(0)) / 2, ((double)MaxPoint.GetValue(1) + (double)MinPoint.GetValue(1)) / 2, ((double)MaxPoint.GetValue(2) + (double)MinPoint.GetValue(2)) / 2);
        }
        public static WMVertice inventorPointToWMvertice(Inventor.Point ponto){
            return new WMVertice(ponto.X, ponto.Y, ponto.Z);
        }
        public static void criarTemplate()
        {
            StreamWriter StrmXML;
            string caminho = Config.pathProjecto + Config.projectNome + "xml.xml";
            FileStream ficheiro = new FileStream(caminho, FileMode.OpenOrCreate, FileAccess.Write);
            StrmXML = new StreamWriter(ficheiro);
            // StrmXML.WriteLine(("") + Environment.NewLine);
            StrmXML.WriteLine(("<?xml version='1.0' encoding='utf-8'?>"));
            StrmXML.WriteLine(("<flex-config>"));
            StrmXML.WriteLine(("<compiler>"));
            StrmXML.WriteLine(("<source-path append='true'>"));
            StrmXML.WriteLine(("<path-element>" + Config.pathProjecto + "</path-element>"));
            StrmXML.WriteLine(("<path-element>" + Config.INSTAL_PV3D + "</path-element>"));
            StrmXML.WriteLine(("<path-element>" + Config.INSTAL_CLASS + "</path-element>"));
            StrmXML.WriteLine(("</source-path>"));
            StrmXML.WriteLine(("</compiler>"));
            StrmXML.WriteLine(("<file-specs>"));
            StrmXML.WriteLine(("<path-element>" + Config.pathProjecto + @"Main.as" + "</path-element>"));
            StrmXML.WriteLine(("</file-specs>"));
            StrmXML.WriteLine(("<default-background-color>" + Config.TEMPLATE_COLOR + "</default-background-color>"));
            StrmXML.WriteLine(("<default-frame-rate>" + Config.TEMPLATE_RATE + "</default-frame-rate>"));
            StrmXML.WriteLine(("<default-size>"));
            StrmXML.WriteLine((" <width>" + Config.TEMPLATE_WIDTH + "</width>"));
            StrmXML.WriteLine((" <height>" + Config.TEMPLATE_HEIGHT + "</height>"));
            StrmXML.WriteLine(("</default-size>"));
            StrmXML.WriteLine(("<metadata />"));
            StrmXML.WriteLine(("</flex-config>"));
            // Gravar o ficheiro 
            StrmXML.Close();
        }
        public static void compilar()
        {
            FileStream ficheiro;
            StreamWriter escrever;
            String exe = String.Format("{0}{1}{2}{3}", Config.pathProjecto, "", Config.projectNome, ".bat \"");
            String path = Path.Combine(Config.pathProjecto, Config.projectNome + ".bat");
            ficheiro = new FileStream(@path, FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escrever.Write(Config.sdkExe + " -load-config+=" + "\"" + Config.pathProjecto + Config.projectNome + "xml.xml\"" + " -output " + "\"" + Config.pathProjecto + Config.projectNome + ".swf\"");
            escrever.Close();
            Process exeProcess = new Process();
            exeProcess.StartInfo.UseShellExecute = false;
            exeProcess.StartInfo.RedirectStandardInput = true;
            exeProcess.StartInfo.RedirectStandardOutput = true;
            exeProcess.StartInfo.RedirectStandardError = true;
            exeProcess.StartInfo.StandardOutputEncoding = Encoding.Default;
            exeProcess.StartInfo.StandardErrorEncoding = Encoding.Default;
            exeProcess.StartInfo.CreateNoWindow = true;
            exeProcess.StartInfo.FileName = Config.pathProjecto + Config.projectNome + ".bat";
            exeProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            

            exeProcess.Start();
            exeProcess.WaitForExit();
            String Saida = exeProcess.StandardOutput.ReadToEnd();
            String Erro = exeProcess.StandardError.ReadToEnd();
            exeProcess.ExitCode.ToString();
            exeProcess.Close();
            Config.caminhoSwf = Config.pathProjecto + Config.projectNome + ".swf";
        }
        public static void verificarDirectorio()
        {
            //Create the directory to work the file
            if (Directory.Exists(Config.pathTrabalho + Config.projectNome))
            {
            }
            else
            {
                Directory.CreateDirectory(Config.pathTrabalho + Config.projectNome);
            }
            Config.pathProjecto = Config.pathTrabalho + Config.projectNome + "\\";
            String caminhoRe = System.IO.Path.Combine(Config.pathProjecto, "resources");
            if(!Directory.Exists(caminhoRe))
            {
                Directory.CreateDirectory(caminhoRe);
            }
        }
        public static String getNomeClassePrincipal(String fullFileName)
        {
            String nomeTemp = fullFileName;
            int idx1;
            int idx2;
            idx1 = nomeTemp.LastIndexOf('\\') + 1;
            idx2 = nomeTemp.IndexOf('.');
            //Nome do ficheiro
            nomeTemp = (nomeTemp.Substring(idx1, idx2 - idx1)).Replace(" ", "_");
            return nomeTemp;
        }
        public static string getFullIndexPath(TreeViewEventArgs e)
        {

            TreeNode temp = e.Node;
            if (e.Node.Level != 0)
            {
                String path = "";
                while (temp.Level != 0)
                {
                    path = temp.Index + path;
                    if (temp.Parent.Level != 0)
                    {
                        path = "|" + path;
                    }
                    temp = temp.Parent;

                }
                return path;
            }
            return "";
        }
        public static void cleanThreads()
        {
            foreach (Thread t in frmCubo.thread)
            {
                while (t.ThreadState != System.Threading.ThreadState.Stopped)
                {
                }
            }
            frmCubo.thread.Clear();
        }


    }
}

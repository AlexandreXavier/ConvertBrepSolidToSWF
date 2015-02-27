using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class SaveAssemblyPreview
    {
        FileStream ficheiro;
        StreamWriter escrever;
        String nomeAssembly;
        String importacoes="";
        String corpo="";
        String transformacao = "";
        String nomeClasseAs = "";
        String nomeClassBase="";
        public SaveAssemblyPreview(String nomeAssembly,String nomeClasseAs,String nomeClasseBase,Array matrixTrans)
        {
            this.nomeAssembly=nomeAssembly;
            this.nomeClasseAs = nomeClasseAs;
            this.nomeClassBase=nomeClasseBase;
            ficheiro = new FileStream(Config.pathProjecto + nomeClasseAs + ".as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escreverTransformacao(matrixTrans);
            escreverCabecalho();
        }
        
        //Criar o cabeçalho conforme a versão
        public void escreverCabecalho()
        {

            escrever.Write(@"package {
import org.papervision3d.core.proto.*;
import org.papervision3d.core.*;
import org.papervision3d.materials.*;
import org.papervision3d.objects.*;
import org.papervision3d.core.math.Matrix3D;
import org.papervision3d.events.InteractiveScene3DEvent;
import Assembly; ");
            
        }
        public void fecharFicheiro()
        {
            escrever.Write(importacoes);
            criarClass();
            escrever.Write(corpo);
            escrever.WriteLine("addChilds();");
            escrever.WriteLine("addArames();");
            escrever.WriteLine(transformacao);
            escrever.WriteLine("}" + Environment.NewLine + "}" + Environment.NewLine + "}");
            escrever.Close();
        }

        public void escreverPeca(String nomeClassePeca)
        {
            corpo += "addPeca(new " + nomeClassePeca + "(pecas.length,this));" + Environment.NewLine;
            importacoes += "import " + nomeClassePeca + ";" + Environment.NewLine;
        }
        public void escreverAssembly(String nomeClasseAssembly)
        {
            corpo += "addAssembly(new " + nomeClasseAssembly + "(pecas.length,this.level+1,this));" + Environment.NewLine;
            importacoes += "import " + nomeClasseAssembly + ";" + Environment.NewLine;
        }
        public void escreverTransformacao(Array matrixTrans)
        {
            transformacao = "copyTransform(WMServicos.toMatrix(" + WMServicosConver.arrayToString(matrixTrans) + "));";
        }
        public void criarClass()
        {
           
                escrever.WriteLine(("public class " + nomeClasseAs + " extends "+nomeClassBase) + Environment.NewLine);
                escrever.WriteLine(("{") + Environment.NewLine);
                escrever.WriteLine(("public function " + nomeClasseAs + "( index:int, level:int, pai:Assembly=null)") + Environment.NewLine);
                escrever.WriteLine(("{") + Environment.NewLine);
                escrever.WriteLine("super(\""+ nomeAssembly+"\" , index, level, pai);");
            
        }

    }
}

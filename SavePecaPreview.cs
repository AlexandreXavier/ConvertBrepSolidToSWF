using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class SavePecaPreview
    {

        FileStream ficheiro;
        StreamWriter escrever;
        String nomePeca;
        string nomeClasseTipo;
        String transformacao;
        Array matrixTrans;
        String nomeClasseAs = "";
        public SavePecaPreview(String nomePeca, String nomeClasseAs,String nomeClasseTipo,Array matrixTrans)
        {
            this.nomePeca = nomePeca;
            this.nomeClasseTipo = nomeClasseTipo;
            this.nomeClasseAs = nomeClasseAs;
            this.matrixTrans = matrixTrans;
            ficheiro = new FileStream(Config.pathProjecto + nomeClasseAs + ".as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escreverCabecalho();
            escreverTransformacao();
            fecharFicheiro();
        }
        public void escreverCabecalho()
        {
            escrever.WriteLine(@"package{
import org.papervision3d.core.math.Matrix3D;
import org.papervision3d.core.proto.*;
import org.papervision3d.core.*;
import org.papervision3d.materials.*;
import org.papervision3d.objects.*;
import org.papervision3d.events.InteractiveScene3DEvent;
import Assembly;
public class "+this.nomeClasseAs+@" extends "+nomeClasseTipo+@" {
public function "+nomeClasseAs+@" (index:int,pai:Assembly ){
super("+"\""+this.nomePeca+"\""+@",index,pai);                    
");
        }
        public void escreverTransformacao()
        {
            transformacao = "copyTransform(WMServicos.toMatrix(" + WMServicosConver.arrayToString(matrixTrans) + "));";
        }
        public void fecharFicheiro()
        {
            escrever.WriteLine(transformacao);
            escrever.WriteLine("this.geometry.ready = true;" + Environment.NewLine + "}" + Environment.NewLine + "}" + Environment.NewLine + "}");
            escrever.Close();
        }

    }
}

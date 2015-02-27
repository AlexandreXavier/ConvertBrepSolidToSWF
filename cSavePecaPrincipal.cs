using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace Cubo3
{
    class cSavePecaPrincipal
    {
        FileStream ficheiro;
        StreamWriter escrever;
        String nomePeca;
        public cSavePecaPrincipal(String nomePeca)
        {
            this.nomePeca = nomePeca;
            ficheiro = new FileStream(Config.pathProjecto + nomePeca + ".as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escreverCabecalho();
        }
        public void escreverCabecalho()
        {
            escrever.Write(@"package{
import org.papervision3d.core.math.Matrix3D;
import org.papervision3d.core.proto.*;
import org.papervision3d.core.*;
import org.papervision3d.materials.*;
import org.papervision3d.objects.*;
import org.papervision3d.events.InteractiveScene3DEvent;
import Assembly;
");
            escrever.WriteLine("public class " + nomePeca + " extends Peca {");
            escrever.WriteLine(@"public static var mesmoTipo: Array=new Array();
	public static  var todoIgual:Boolean=false;
");
            escrever.WriteLine("public function " + nomePeca + "( nome:String,index:int,n11:Number,n12:Number,n13:Number,n14:Number, n21:Number,n22:Number,n23:Number,n24:Number, n31:Number,n32:Number,n33:Number,n34:Number,pai:Assembly,material:MaterialObject3D=null ){");
            //falta o material
            escrever.WriteLine(@"super(nome,index,pai,material);                    
");

        }
        public void fecharFicheiro()
        {
            escrever.Write(@"copyTransform(WMServicos.toMatrix(n11, n12, n13, n14, n21, n22, n23, n24, n31, n32, n33, n34));
this.geometry.ready = true;
mesmoTipo.push(this);
");
            escrever.Close();
        }
        public void escreverCorpo(String text)
        {
            escrever.Write(text);
        }
    }
}

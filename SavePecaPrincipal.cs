using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace Cubo3
{
    class SavePecaPrincipal
    {
        FileStream ficheiro;
        StreamWriter escrever;
        String nomePeca;
        public SavePecaPrincipal(String nomePeca)
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
            escrever.WriteLine("public function " + nomePeca + "( nome:String,index:int,pai:Assembly,material:MaterialObject3D=null ){");
            //falta o material
            escrever.WriteLine(@"super(nome,index,pai,material);                    
");

        }
        public void fecharFicheiro()
        {
            escrever.Write(@"
verts.push(centro);
//pai.addArame(arame);
mesmoTipo.push(this);
this.addChild(arame);
this.addChild(arame);
this.addChild(arame);
//this.arame.visible=true;
//this.visible=false;
}
}
}
");
            escrever.Close();
        }
        public void escreverCorpo(String text)
        {
            escrever.Write(text);
        }
    }
}

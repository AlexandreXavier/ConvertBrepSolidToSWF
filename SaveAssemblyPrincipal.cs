using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace Cubo3
{
    class SaveAssemblyPrincipal
    {

        FileStream ficheiro;
        StreamWriter escrever;
        String nomeAssembly;
        public SaveAssemblyPrincipal(String nomeAssembly)
        {
            this.nomeAssembly = nomeAssembly;
            ficheiro = new FileStream(Config.pathProjecto + nomeAssembly + ".as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escreverCabecalho();
            criarClass();
            fecharFicheiro();
        }
        public void escreverCabecalho()
        {
            escrever.WriteLine(@"package {
import org.papervision3d.core.proto.*;
import org.papervision3d.core.*;
import org.papervision3d.materials.*;
import org.papervision3d.objects.*;
import org.papervision3d.core.math.Matrix3D;
import org.papervision3d.events.InteractiveScene3DEvent;
import Assembly;
");

        }
        public void criarClass()
        {
            escrever.WriteLine("public class " + nomeAssembly + " extends Assembly {");
            escrever.WriteLine(@"public static var mesmoTipo: Array=new Array();
public static var tudoIgual:Boolean = false;
");
            escrever.WriteLine("public function " + nomeAssembly + "( nome:String, index:int, level:int,pai:Assembly = null){");

            escrever.WriteLine(@"super(nome, index,level, pai);
mesmoTipo.push(this);");

        }
        public void fecharFicheiro()
        {
            escrever.WriteLine(@"
}
}
}
");
            escrever.Close();
        }
    }
}

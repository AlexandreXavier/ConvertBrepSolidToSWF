using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class fichVista
    {
        FileStream ficheiro;
        StreamWriter escrever;
        public fichVista()
        {
        }
        public void run()
        {
            ficheiro = new FileStream(Config.pathProjecto + "Vista.as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escrever.WriteLine(@"package  
{
	import org.papervision3d.core.math.Matrix3D;
	import org.papervision3d.core.proto.MaterialObject3D;
	public class Vista 
	{
		public static var vistas:Array = new Array();
		public var visible:Boolean;
		public var material:MaterialObject3D;
		public var matrix:Matrix3D;
        public var nome:String = '';
		public function Vista(visibilidade:Boolean=true,material:MaterialObject3D=null,matrix:Matrix3D=null,nome:String='')
		{
			this.material = material;
			this.visible = visibilidade;
			this.matrix = matrix;
            this.nome=nome;
		}
		
	}
}");
            escrever.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class fichPeca
    {
        FileStream ficheiro;
        StreamWriter escrever;
        public fichPeca()
        {
        }
        public void run()
        {
            ficheiro = new FileStream(Config.pathProjecto + "Peca.as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);

            escrever.WriteLine(@"package {
	import org.papervision3d.core.*;
	import org.papervision3d.core.proto.*;
	import org.papervision3d.core.geom.*;
	import org.papervision3d.core.geom.renderables.Triangle3D;
	import org.papervision3d.core.geom.renderables.Vertex3D;
    import org.papervision3d.core.geom.renderables.Line3D;	
    import org.papervision3d.core.math.NumberUV;
	import org.papervision3d.materials.*;
	import org.papervision3d.materials.special.LineMaterial;
	import Assembly;
	import org.papervision3d.events.InteractiveScene3DEvent;
    import flash.external.ExternalInterface;
	public class Peca extends TriangleMesh3D
	{
		public var verts :Array;
		public var faceAr:Array;
		public var uvs :Array;
        public var pai:Assembly;
		public var materialInicial:MaterialObject3D;
		public var selected:Boolean = false;
		public var index: int;
		public var level:int;
		public var caminho:String;
        public var centro:Vertex3D=new Vertex3D(0,0,0);
		public var arame:Lines3D = new Lines3D(new LineMaterial(0x0000ff));
		public var geometria:GeometryObject3D;
		public var geometriaNula:GeometryObject3D = new GeometryObject3D();
		public var estado:Boolean = true;
        public var vistas:Array=new Array();
		public function v(x:Number,y:Number,z:Number):void {
			verts.push(new Vertex3D(x,y,z));
		}
		public function uv(u:Number,v:Number):void {
			uvs.push(new NumberUV(u,v));
		}
		public function f(vn0:int, vn1:int, vn2:int, uvn0:int, uvn1:int,uvn2:int):void {
			faceAr.push( new Triangle3D(this, [verts[vn0],verts[vn1],verts[vn2] ], null, [uvs[uvn0],uvs[uvn1],uvs[uvn2]] ) );
		}
		public function edge(v1:Number,v2:Number):void {
		    arame.addLine(new Line3D(arame, new LineMaterial(0x000000), 0.1, verts[v1], verts[v2]));
		}
		public function Peca(nome:String,index:int,pai:Assembly,materialTipo:MaterialObject3D=null)
		{
			super( materialTipo, new Array(), new Array(), nome);
			verts = this.geometry.vertices;
			faceAr= this.geometry.faces;
			uvs   = new Array();
			this.pai = pai;
            this.geometria = this.geometry;
            materialInicial=this.material;
            this.material.interactive=true;
			this.index = index;
			this.level = pai.level + 1;;
			this.caminho = WMServicos.calculateCaminho(level,index,pai);
			this.addEventListener(InteractiveScene3DEvent.OBJECT_CLICK, umClick);
			this.addEventListener(InteractiveScene3DEvent.OBJECT_DOUBLE_CLICK, doisClick);
		}
		public function mudarArame():void {
			if (estado) {
				this.geometry = geometriaNula;
				estado = false;
				this.arame.visible=true;
			} else {
				this.geometry = geometria;
				estado = true;
				this.arame.visible = false;
			}
		}
		public function umClick(e:InteractiveScene3DEvent):void {
			WMServicos.selectUmClick(this);
            ExternalInterface.call('selectInTree', this.caminho);
		}
		public function doisClick(e:InteractiveScene3DEvent):void {
			WMServicos.selectDoisClick(this);
		}
		public function setMaterial(matTemp:MaterialObject3D):void
		{
            matTemp.interactive = true;			
            this.material = matTemp;
		}
		public function setMaterialInicial():void
		{
			this.material = materialInicial;
		}
		public function changeMaterial(matTemp:MaterialObject3D):void
		{
			this.setMaterial(matTemp);
			materialInicial = matTemp;
		}
		public function getCentro():Vertex3D {
			return centro;
		}
		public function esconderMostrar(setV:String=null):void {
			//Main.debug.appendText('@peca esc ' + setV+'@');			
            if(setV==null){
				this.visible = !this.visible;
                //Main.debug.appendText('@peca null @');
			} else {
                //Main.debug.appendText('@peca not null@ ');					
                if (setV.toLocaleLowerCase() == 'true') {
					this.visible = true;
				} else {
					this.visible = false;
				}	
			}		
		}
		public function guardarVista():void {
			vistas.push(new Vista( this.visible,this.material,null,''));
          //  Main.debug.appendText('gVista \n');
		}
		public function usarVista(i:Number):void {
            //Main.debug.appendText('@peca usarvista peca inicio@\n ');			
            var vista:Vista = (vistas[i] as Vista);
			changeMaterial(vista.material);
			this.visible = vista.visible;
            //Main.debug.appendText('@peca usarvista peca fim@\n ');
		}
        public function apagarVista(i:Number):void {
	        vistas.splice(i, 1);
	    }
	}
}
");
            escrever.Close();
        }
    }
}

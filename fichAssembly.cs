using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class fichAssembly
    {
        FileStream ficheiro;
        StreamWriter escrever;
        public fichAssembly()
        {
        }
        public void run()
        {
            ficheiro = new FileStream(Config.pathProjecto + "Assembly.as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escrever.WriteLine(@"package {
import flash.display.*;
import flash.filters.*;
import flash.display.Stage;
import flash.events.*;
import org.papervision3d.core.*;
import org.papervision3d.core.math.Matrix3D;
import org.papervision3d.core.proto.*;
import org.papervision3d.core.geom.*;
import org.papervision3d.scenes.Scene3D;
import org.papervision3d.cameras.Camera3D;
import org.papervision3d.objects.*;
import org.papervision3d.objects.DisplayObject3D;
import org.papervision3d.materials.*;
import org.papervision3d.core.geom.renderables.Vertex3D;
import org.papervision3d.core.geom.renderables.Line3D;
public class Assembly extends DisplayObject3D {
		public var pecas:Array=new Array();
        public var arames:Array=new Array();
		public var pai:Assembly; 
		public var selected:Boolean = false;
		public var index:int;
		public var level:int;
		public var caminho:String;
		public var disCentro:DisplayObject3D = DisplayObject3D.ZERO;
        public var vistas:Array=new Array();
		public function Assembly(nome:String,index:int,level:int,pai:Assembly=null) {
			this.name = nome;
			this.pai = pai;
			this.level = level;
			this.index = index;
			this.caminho = WMServicos.calculateCaminho(level,index, pai);	
            addChild(disCentro, 'centro');
		}
		public function addPeca(peca:Peca):void {
			pecas.push(peca);
		}
		public function addAssembly(assembly:Assembly):void{
			pecas.push(assembly);
		}
		public function addArame(arame:Lines3D):void{
			arames.push(arame); 
		}
		public function addChilds():void {
			for (var i:int = 0; i < pecas.length; i++) {
				addChild(pecas[i]);
			}
		}
        public function addArames():void {
			for (var i:int = 0; i < arames.length; i++) {
				addChild(arames[i]);
			}
		}
		public function setMaterial(material:MaterialObject3D):void
		{
			for (var i:int = 0; i < pecas.length; i++) {
				pecas[i].setMaterial(material);
			}
		}
		public function setMaterialInicial():void
		{
				for (var i:int = 0; i < pecas.length; i++) {
				pecas[i].setMaterialInicial();
				}
		}
		public function changeMaterial(material:MaterialObject3D):void
		{
				for (var i:int = 0; i < pecas.length; i++) {
				pecas[i].changeMaterial(material);
			}
		}
		public function getCentro():Vertex3D {
			return new Vertex3D(disCentro.x, disCentro.y, disCentro.z);
		}
		public function mudarArame():void {
		    for (var i:int = 0; i < pecas.length; i++) {
			    pecas[i].mudarArame();
			}	
		}
		public function esconderMostrar(setV:String=null):void {
            var res:Boolean;			
          //  Main.debug.appendText('@Assembly esc ' + setV+'@');
			if(setV==null){
			//	Main.debug.appendText('@assembly null @');	
            //    this.visible = !this.visible;
            res = !this.visible;
			} else {
			//	Main.debug.appendText('@assembly not null@ ');
                if (setV.toLocaleLowerCase() == 'true') {
					//this.visible = true;
                       res = true;
				} else {
					//this.visible = false;
                    res = false;
				}	
			}
			for ( var i:int = 0; i < pecas.length;i++ ){
		//		Main.debug.appendText('@'+i+'@');	
                //pecas[i].esconderMostrar(''+this.visible);
                pecas[i].esconderMostrar(''+res);
			}
		}
	public function guardarVista():void {
        //Main.debug.appendText('gVista ass\n');		
        if (level == 0) {
            var m:Matrix3D=Matrix3D.clone(this.transform);
			vistas.push(new Vista(this.visible, null, m,''));
		} else {
			vistas.push(new Vista(this.visible));
		}
		for (var i:Number = 0 ; i<pecas.length; i++) {
			pecas[i].guardarVista();
		}
	}
	public function usarVista(j:Number):void {
       // Main.debug.appendText('@peca usarvista ass inicio@\n ');		
        var vista:Vista = (vistas[j] as Vista);
		if (level == 0) {
            //Main.debug.appendText('@Assembly pai@\n ');
			copyTransform(Matrix3D.clone(vista.matrix));
 			//copyPosition(Matrix3D.clone(vista.matrix));
			//transform.copy(Matrix3D.clone(vista.matrix));
		}
		this.visible = vista.visible;
		for (var i:Number = 0 ; i<pecas.length; i++) {
			pecas[i].usarVista(j);
		}
        //Main.debug.appendText('@peca usarvista ass Fim@\n ');	
	}
	public function apagarVista(j:Number):void {
		vistas.splice(j, 1);
		for (var i:Number = 0 ; i<pecas.length; i++) {
			pecas[i].apagarVista(j);
		}
	}
	}
}
");
            escrever.Close();
        }
    }
}

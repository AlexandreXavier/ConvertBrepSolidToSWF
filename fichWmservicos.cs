using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    class fichWmservicos
    {
        FileStream ficheiro;
        StreamWriter escrever;
        public fichWmservicos()
        { }
        public void run()
        {
            ficheiro = new FileStream(Config.pathProjecto + "WMServicos.as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escrever.WriteLine(@"package {
	import org.papervision3d.core.proto.MaterialObject3D;
	import org.papervision3d.materials.*;
	import org.papervision3d.core.math.Matrix3D;
    import org.papervision3d.objects.DisplayObject3D;
	
	public class WMServicos {
		public static  var searchedObject:Object;
		public static  var selectedObject:Object;
		public static var selectAll:Boolean=false;
        public static var appReady:Boolean =false;
        
        public static function toMatrix(n11:Number, n12:Number, n13:Number, n14:Number, n21:Number, n22:Number, n23:Number, n24:Number, n31:Number, n32:Number, n33:Number, n34:Number):Matrix3D
		{
			var t:Array = new Array(12);
			t[0] = n11;
			t[1] = n12;
			t[2] = n13;
			t[3] = n14;
			t[4] = n21;
			t[5] = n22;
			t[6] = n23;
			t[7] = n24;
			t[8] = n31;
			t[9] = n32;
			t[10] = n33;
			t[11] = n34;
			return new Matrix3D(t);
		}
		public static function getTypeOfObject(objecto:Object):int {
			//-1 é null
			//0 é peça
			//1 é assembly
			var res:int=-1;
			if (objecto is Assembly) {
				res=1;
			} else if (objecto is Peca) {
				res=0;
			}
			return res;
		}
		public static function esconderMostrar(objecto:Object):void {
			objecto.visible=!objecto.visible;
		}
		public static function getObjecto(caminho:Array):void {
			var res:Object=Main.rootAssembly;
			for (var i:int=0;i< caminho.length; i++) {
				res=(res  as  Assembly).pecas[caminho[i]];
			}
			searchedObject = res;
            //Main.debug.appendText('\n obj get');
		}
		public static function converterStringEmArray(caminho:String):Array
		{
			var ar:Array = caminho.split('|');
			converterArrayEmArrayNumerico(ar);
			return ar
		}
		public static function converterArrayEmArrayNumerico(ar:Array):void
		{
			for (var i:int = 0; i < ar.length; i++)
			{
				ar[i] = Number(ar[i]);
			}
			
		}
		public static function deSelect():void {
			if(selectedObject!=null){
			selectedObject.setMaterialInicial();
			selectedObject.selected = false;
			selectedObject= null;
			}
		}
		public static function select(objecto:Object):void {
			deSelect();
            var corAleatoria:MaterialObject3D = new ColorMaterial(Math.random() * (0xffffff));
			selectedObject = objecto;
			selectedObject.setMaterial(corAleatoria);
			objecto.selected = true;
		}
		public static function selectUmClick(objecto:Object):void {
				if (objecto.selected)
				{
					objecto.selected = false;
					select(selectedObject.pai);
					if (selectAll)
					{
						selectAllMesmoTipo();
					}
				} else {
					if (selectAll)
					{
						deselectAllMesmoTipo();					
					}
					deSelect();
					select(objecto);
					if (selectAll)
					{
						selectAllMesmoTipo();
					}				
				}
		}
		public static function selectDoisClick(objecto:Object):void {
			if (objecto.selected)
			{
				if(objecto.pai.selected){
					if (selectAll)
					{
						deselectAllMesmoTipo();
					}
					deSelect();
					select(objecto);
				}
			} else {
				if (selectAll)
					{
						deselectAllMesmoTipo();
					}
				deSelect();
				select(objecto);
				if (selectAll)
				{
					selectAllMesmoTipo();
				}
			}
		}
		public static function selectAllMesmoTipo():void {
			var matTemp:MaterialObject3D = new ColorMaterial(Math.random() * (0xffffff));
			matTemp.interactive = true;
			for (var i:int = 0; i < selectedObject.mesmoTipo.length; i++) {
				selectedObject.mesmoTipo[i].setMaterial(matTemp);
			}
			selectedObject.tudoIgual = true;
		}
		public static function deselectAllMesmoTipo():void {
			for (var i:int = 0; i < selectedObject.mesmoTipo.length; i++) {
				if(selectedObject.mesmoTipo[i]!=selectedObject){
				selectedObject.mesmoTipo[i].setMaterialInicial();
				}
			}
			selectedObject.tudoIgual = false;
			
		}

		public static function calculateCaminho(level:int,index:int,pai:Assembly=null):String
		{	
			var caminho:String;
			if (level > 1)
			{
				caminho = pai.caminho+'|'+index;
			} else if (level==1)
			{
				caminho = '' + index;
			} else {
				caminho = '';
			}
			return caminho;
		}
		public static function changeMaterialWire():void {
			var defMaterial :MaterialObject3D = new WireframeMaterial();
			defMaterial.lineColor   = 0xFFFFFF;
			defMaterial.lineAlpha   = 1;
			defMaterial.fillColor   = 0x000000;
			defMaterial.fillAlpha   = 1;
			defMaterial.doubleSided = false;
			defMaterial.interactive = true;
			searchedObject.changeMaterial(defMaterial);
			
		}
		public static function changeMaterialColor(cor:int):void {
			//Main.debug.appendText('\n change 1 fase');
//Main.debug.appendText('\n'+cor);
            var defMaterial :MaterialObject3D = new ColorMaterial(cor, 1, true);
//			Main.debug.appendText('\n change 2 fase');
			searchedObject.changeMaterial(defMaterial);
//			Main.debug.appendText('\n change final');
		}
		public static function changeMaterialImagem(path:String):void {
			//var defMaterial :MaterialObject3D = new BitmapFileMaterial(path, false);
			//defMaterial.interactive = true;
			//defMaterial.doubleSided = true;
			//defMaterial.fillAlpha = 0.2;
			//searchedObject.changeMaterial(defMaterial);
			var defMaterial :MaterialObject3D = new BitmapFileMaterial(path, false);
			defMaterial.interactive = true;
			defMaterial.doubleSided = true;
			defMaterial.fillAlpha = 0.2;
			searchedObject.changeMaterial(defMaterial);
			searchedObject.alpha = 0.2;
		}

		public static function changeAlpha(alpha:Number):void {
			//Main.debug.appendText('\n alpha '+alpha);
            //Main.debug.appendText('\n fillalpha '+(searchedObject as DisplayObject3D).material.fillAlpha);
			(searchedObject as DisplayObject3D).material.fillAlpha = alpha;
          //  Main.debug.appendText('\n fillalpha '+(searchedObject as DisplayObject3D).material.fillAlpha);

			//(searchedObject as DisplayObject3D).alpha = alpha;
		}
	}
}");
            escrever.Close();
        }
    }
}

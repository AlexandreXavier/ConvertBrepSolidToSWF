package {
	import flash.display.*;
	import flash.filters.*;
	import flash.display.Stage;
	import flash.events.*;
    	import org.papervision3d.core.*;
	import org.papervision3d.core.proto.*;
	import org.papervision3d.core.geom.*;
	import org.papervision3d.scenes.Scene3D;
	import org.papervision3d.cameras.Camera3D;
	import org.papervision3d.objects.*;
	import org.papervision3d.objects.DisplayObject3D;
	import org.papervision3d.materials.*;


	public class Assembly extends DisplayObject3D {
		public var pecas:Array=new Array();
		public var nome:String;
		public function Assembly(nome:String) {
			this.nome = nome;
		}
		public function getPecas():Array {
			return pecas;
		}
		public function addPeca(peca:Peca):void {
			pecas.push(peca);
		}
		public function addAssembly(assembly:Assembly):void{
			pecas.push(assembly);
		}
		public function removePeca(nome:String ):void {
			var res:Peca = pesquisar(nome);			
			pecas.splice(pecas.indexOf(res), 0);
			
		}
		public function removePeca(index:int):void {
			pecas.splice(index, 1);
		}
		public function pesquisar(nome:String ):Peca {
			var res:Peca = null;
			for (i:int; i < pecas.length; i++) {
				if(pecas instanceof pecaClass){
					if ((pecas[i] as PecaClass).nome = nome) {
						res = pecas[i];
					}
				} else {
					var temp:AssemblyClass = (pecas[i] as AssemblyClass);
					res=temp.pesquisar(nome);
				}
			}
			return res;
		}
		public function pesquisarIndex(nome:String ):int {
			var res:int = -1;
			for (i:int; i < pecas.length; i++) {
				if(pecas instanceof pecaClass){
					if ((pecas[i] as PecaClass).nome = nome) {
						res= i;
					}
				} else {
					var temp:AssemblyClass = (pecas[i] as AssemblyClass);
					res=temp.pesquisarIndex(nome);
				}
			}
			return res;
		}
		public function addChilds():void {
			for (i:int = 0; i < pecas.length; i++) {
				addChild(pecas[i]);
			}
		}
		public function pesquisarAssemComPeca(nome:String):AssemblyClass {
			var res:AssemblyClass = null;
			for (i:int; i < pecas.length; i++) {
				if(pecas instanceof pecaClass){
					if ((pecas[i] as PecaClass).nome = nome) {
						res = pecas[i];
					}
				} else {
					var temp:AssemblyClass = (pecas[i] as AssemblyClass);
					res=temp.pesquisar(nome);
				}
			}
			return res;
		}
		
		
	}
}
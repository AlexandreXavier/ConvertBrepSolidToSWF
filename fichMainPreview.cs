using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cubo3
{
    static class fichMainPreview
    {
        static FileStream ficheiro;
        static StreamWriter escrever;

        public static void criarFicheiro(String nomeClasseAs,String nome)
        {
            Double c = 200 * Math.Tan(Math.PI / 6);
            WMVertice centro = WmServicos.calcularCentro();
            Double zCamera = Math.Max(Math.Max(120 * ((double)WmServicos.MaxPoint.GetValue(0) - (double)WmServicos.MinPoint.GetValue(0)) / c, 120 * ((double)WmServicos.MaxPoint.GetValue(1) - (double)WmServicos.MinPoint.GetValue(1)) / c), 150 * Math.Abs(((double)WmServicos.MaxPoint.GetValue(2))) / 100);
            ficheiro = new FileStream(Config.pathProjecto + "Main.as", FileMode.Create);
            escrever = new StreamWriter(ficheiro);
            escrever.WriteLine(@"package {
	//Flash imports
	import flash.display.*;
	import flash.filters.*;
	import flash.display.Stage;
	import flash.events.*;
	import flash.display.Sprite; // To extend this class
	import flash.events.Event; // To work out when a frame is entered.
    import flash.external.ExternalInterface;
	import org.papervision3d.core.math.Matrix3D;
    import org.aswing.*;

	//Papervision Imports
	import org.papervision3d.scenes.Scene3D;// We'll need at least one scene
	import org.papervision3d.cameras.*;// Import all types of camera
	import org.papervision3d.render.*;// And we need a renderer
	import org.papervision3d.view.Viewport3D;// We need a viewport
	import org.papervision3d.materials.*;
	import org.papervision3d.events.InteractiveScene3DEvent;
    import org.papervision3d.objects.primitives.Sphere;
	import org.papervision3d.core.geom.Lines3D;
	import org.papervision3d.core.geom.renderables.Vertex3D;
    import org.papervision3d.core.geom.renderables.Line3D;

	import org.papervision3d.materials.special.LineMaterial;
    import org.papervision3d.objects.primitives.Plane;


	import " + nomeClasseAs+ ";");
            escrever.WriteLine(@"public class Main extends Sprite {
		public var viewport:Viewport3D;// The Viewport
		public var renderer:BasicRenderEngine;// Rendering engine
        public var pecaSelect:Peca;
        private var assemblySelect:Assembly   
		//public var renderer:LazyRenderEngine;
		// -- Scenes -- //
		public var default_scene:Scene3D;// A Scene
		// -- Cameras --//
		public var default_camera:DebugCamera3D;// A Camera
        public var centro:Sphere = new Sphere(new ColorMaterial(0xff0000, 1, false),0.0001,2,2);
        private var appReady:Boolean;
        public static var debug:JTextArea=new JTextArea('', 10, 10);
        public var planoOrigem:Plane;
		public var box:Lines3D = new Lines3D(new LineMaterial(0x0000ff));
		public var xMx:Number;
		public var xMn:Number;
		public var yMx:Number;
		public var yMn:Number;
		public var zMx:Number;
		public var zMn:Number;
		public var v1:Vertex3D;
		public var v2:Vertex3D;
		public var v3:Vertex3D;
		public var v4:Vertex3D;
		public var v5:Vertex3D;
		public var v6:Vertex3D;
        public var v7:Vertex3D;
		public var v8:Vertex3D;
		public var vistas:Array=new Array();
        public var flag:Boolean=false;
        public var m1:Matrix3D;
        public var m2:Matrix3D;
        public static var rootAssembly:Assembly=new " + nomeClasseAs + "(0,0);");
            escrever.Write(@"public function init(vpWidth:Number = 800, vpHeight:Number = 600):void {
			initPapervision(vpWidth, vpHeight);// Initialise papervision
			init3d();// Initialise the 3d stuff..
			init2d();// Initialise the interface..
			initEvents();// Set up any event listeners..
            initExternalInterface();
            }");
            

            escrever.Write(@"       public function initExternalInterface():void
		{
			//debug.appendText('CallBack  \n');
            ExternalInterface.addCallback(" + "\"setAppReady\", " + @"setAppReady);

			"+"ExternalInterface.addCallback(\"selectObject\", selectObject);"+ @"

			ExternalInterface.addCallback(" + "\"esconderObject\", esconderObject);"+ @"
			ExternalInterface.addCallback('changeMaterialColor', changeMaterialColor);
			ExternalInterface.addCallback('changeMaterialWire', changeMaterialWire);
			ExternalInterface.addCallback('changeMaterialImagem', changeMaterialImagem);
            ExternalInterface.addCallback('getMatriz', getMatriz);
            ExternalInterface.addCallback('changeAlpha', changeAlpha);
            ExternalInterface.addCallback('setVista',setVista);
            ExternalInterface.addCallback('guardarVista',guardarVista);
            ExternalInterface.addCallback('apagarVista',apagarVista);
            //debug.appendText('CallBack fim \n');

		}

        public function getMatriz():String {
            //debug.appendText('get matriz \n');          
            return rootAssembly.transform.toString();
            //debug.appendText('get matriz fim \n');
		}
		public function setVista(i:Number):void {
            debug.scrollToBottomLeft();		
            var m:Matrix3D= (vistas[i] as Matrix3D);
            default_camera.transformView();            
                        
            default_camera.copyTransform(m);
            
            default_camera.transformView();
            rootAssembly.usarVista(i);

		}
		public function guardarVista():void {
            
            vistas.push(Matrix3D.clone(default_camera.transform));
            debug.scrollToBottomLeft();       
            rootAssembly.guardarVista();
            

		}
        
		public function apagarVista(i:Number):void {
			rootAssembly.apagarVista(i);
		}

        public function changeAlpha(caminho:String, alpha:String):void {
            //debug.appendText('\n Material Alpha'+Number(alpha));
            //debug.appendText('\n Material Alpha'+alpha);
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
			WMServicos.changeAlpha(Number(alpha));
            //debug.appendText('\n Fim Material Alpha');
		}

        public function changeMaterialColor(caminho:String, cor:int):void {
            //debug.appendText('\n Material Color');
            //debug.appendText('\n Color='+cor);
            //debug.appendText('\n Caminho'+caminho);					
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
			WMServicos.changeMaterialColor(cor);
            //debug.appendText('\n Fim Material Color');


		}
		public function changeMaterialWire(caminho:String):void {
			//debug.appendText('\n Material Wire');
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
			WMServicos.changeMaterialWire();
            //debug.appendText('\n Fim Material Wire');
		}
		public function changeMaterialImagem(caminho:String, caminhoImagem:String):void {
			//debug.appendText('\n Material Imagem');
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
    		WMServicos.changeMaterialImagem(caminhoImagem);
            //debug.appendText('\n Fim  Material Imagem');
		}
		public function selectObject(caminho:String):void {
            //debug.appendText('\n selectObject');			
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
			WMServicos.select(WMServicos.searchedObject);
            //debug.appendText('\n selectObject end');	
		}
		public function esconderObject(caminho:String,setV:String):void {
			//debug.appendText('\n esconder'+caminho);
            WMServicos.getObjecto(WMServicos.converterStringEmArray(caminho));
			WMServicos.searchedObject.esconderMostrar(setV);
            //debug.appendText('\n Fim esconder'+caminho);
		}
		public function setAppReady(t:Boolean):void
		{
           appReady = t;
		}");

            escrever.Write(@"		
		public function initPapervision(vpWidth:Number, vpHeight:Number):void {
			// Here is where we initialise everything we need to
			// render a papervision scene.
			viewport = new Viewport3D(vpWidth, vpHeight,false,true);
			// The viewport is the object added to the flash scene.
			// You 'look at' the papervision scene through the viewport
			// window, which is placed on the flash stage.
			addChild(viewport);// Add the viewport to the stage.
			// Initialise the rendering engine.
			renderer = new BasicRenderEngine();
			// -- Initialise the Scenes -- //
			default_scene = new Scene3D();
			// -- Initialise the Cameras -- //
			default_camera = new DebugCamera3D(viewport);// The argument passed to the camera
			// is the object that it should look at. I've passed the scene object
			// so that the camera is always pointing at the centre of the scene.
		}

		public function Main() {

            
            init();
           // WMServicos.appReady = ExternalInterface.call('initReady');
           // debug.appendText('resultado='+WMServicos.appReady.toString()+'\n');            
            
            


            
        }
		public function init3d():void {
			
		xMx=" + WMServicosConver.numToAs3((double)WmServicos.MaxPoint.GetValue(0)-WmServicos.centro.X) + @";
		xMn=" + WMServicosConver.numToAs3((double)WmServicos.MinPoint.GetValue(0)-WmServicos.centro.X) + @";
		yMx=" + WMServicosConver.numToAs3((double)WmServicos.MaxPoint.GetValue(1) - WmServicos.centro.Y) + @";
		yMn=" + WMServicosConver.numToAs3((double)WmServicos.MinPoint.GetValue(1) - WmServicos.centro.Y) + @";
		zMx=" + WMServicosConver.numToAs3((double)WmServicos.MaxPoint.GetValue(2) - WmServicos.centro.Z) + @";
		zMn=" + WMServicosConver.numToAs3((double)WmServicos.MinPoint.GetValue(2) - WmServicos.centro.Z) + @";
        
		v1 = new Vertex3D(xMn, yMn, zMn);
		v2 = new Vertex3D(xMx, yMn, zMn);
		v3 = new Vertex3D(xMx, yMn, zMx);
		v4 = new Vertex3D(xMn, yMn, zMx);
		v5 = new Vertex3D(xMn, yMx, zMn);
		v6 = new Vertex3D(xMx, yMx, zMn);
		v7 = new Vertex3D(xMx, yMx, zMx);
		v8 = new Vertex3D(xMn, yMx, zMx);
				
		    planoOrigem = new Plane(new WireframeMaterial(0x000000), 2 * (xMx - xMn), 2 * (yMx - yMn));
			planoOrigem.y = yMn -1;
			box = new Lines3D(new LineMaterial(0x0000ff));
			var v1v2:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v1, v2);
			var v2v3:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v2, v3);
			var v3v4:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v3, v4);
			var v4v1:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v4, v1);
			
			var v5v6:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v5, v6);
			var v6v7:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v6, v7);
			var v7v8:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v7, v8);
			var v8v5:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v8, v5);
			
			var v1v5:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v1, v5);
			var v2v6:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v2, v6);
			var v3v7:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v3, v7);
			var v4v8:Line3D = new Line3D(box, new LineMaterial(0x000000), 3, v4, v8);
			
			
			box.addLine(v1v2);
			box.addLine(v2v3);
			box.addLine(v3v4);
			box.addLine(v4v1);
			box.addLine(v5v6);
			box.addLine(v6v7);
			box.addLine(v7v8);
			box.addLine(v8v5);
			box.addLine(v1v5);
			box.addLine(v2v6);
			box.addLine(v3v7);
			box.addLine(v4v8);
            

            centro.x =" + /*WMServicosConver.numToAs3(centro.X)*/"0" +@";
			centro.y ="+/*WMServicosConver.numToAs3(centro.Y)*/"0"+@";
			centro.z ="+/*WMServicosConver.numToAs3(centro.Z)*/"0"+@";
			default_camera.z =-"+WMServicosConver.numToAs3(zCamera)+ @";
            
            default_scene.addChild(rootAssembly);	
            default_scene.addChild(planoOrigem );		
            default_scene.addChild(centro);
			default_scene.addChild(box);
            default_camera.x = centro.x;
			default_camera.y = centro.y;
			default_camera.lookAt(centro);

		}
		public function init2d():void {
			// This function should create all of the 2d items
			// that will be overlayed on your papervision project.
			// User interfaces, Heads up displays etc.
			" + "var t:JFrame = new JFrame(this, \"teste\", false);"+ @"
			t.setSizeWH(150, 500);
		    t.show();
            t.getContentPane().append(debug);
		}

		public function initEvents():void {
			// This function makes the onFrame function get called for
			// every frame.
            addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);
			addEventListener(Event.ENTER_FRAME, onEnterFrame);
			// This line of code makes the onEnterFrame function get
			// called when every frame is entered.
            //addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);
		}
        protected function keyDownHandler(event:KeyboardEvent):void 
		{
           //debug.appendText('key down');
			switch( event.keyCode ) 
			{
				case 'M'.charCodeAt():
                rootAssembly.mudarArame();
				break;
				case 'N'.charCodeAt():
				//rootAssembly.yaw(10);
				rootAssembly.localRotationX+=10;			
				break;	
				case 'B'.charCodeAt():
				rootAssembly.localRotationX-=10;
				//rootAssembly.yaw(-10);
				break;
				case 'J'.charCodeAt():
				rootAssembly.localRotationY+=10;
				//rootAssembly.pitch(10);
				break;	
				case 'H'.charCodeAt():
				rootAssembly.localRotationY-=10;
				//rootAssembly.pitch(-10);
				break;
				case 'U'.charCodeAt():
				rootAssembly.localRotationZ+=10;
				//rootAssembly.roll(10);
				break;	
				case 'Y'.charCodeAt():
				rootAssembly.localRotationZ-=10;
				//rootAssembly.roll(-10);
				break;
				case 'V'.charCodeAt():
				//rootAssembly.yaw(10);
				rootAssembly.rotationX+=10;			
				break;	
				case 'C'.charCodeAt():
				rootAssembly.rotationX-=10;
				//rootAssembly.yaw(-10);
				break;
				case 'L'.charCodeAt():
				rootAssembly.rotationY+=10;
				//rootAssembly.pitch(10);
				break;	
				case 'K'.charCodeAt():
				rootAssembly.rotationY-=10;
				//rootAssembly.pitch(-10);
				break;
				case 'O'.charCodeAt():
				rootAssembly.rotationZ+=10;
				//rootAssembly.roll(10);
				break;	
				case 'I'.charCodeAt():
				rootAssembly.rotationZ-=10;
				//rootAssembly.roll(-10);
				break;
				
			}
		}
		public function processFrame():void {
			// Process any movement or animation here.

		}
		public function onEnterFrame( ThisEvent:Event ):void {
			//We need to render the scene and update anything here.
			processFrame();
			renderer.renderScene(default_scene, default_camera, viewport);
		}
}
}
");
            escrever.Close();
        }

    }
}

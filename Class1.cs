﻿                    escrever.Write(@"//versão2");
                    escrever.Write(@"package {
	//Flash imports
	import flash.display.*;
	import flash.filters.*;
	import flash.display.Stage;
	import flash.events.*;
	import flash.display.Sprite; // To extend this class
	import flash.events.Event; // To work out when a frame is entered.
	
	//Papervision Imports
	import org.papervision3d.scenes.Scene3D;// We'll need at least one scene
	import org.papervision3d.cameras.*;// Import all types of camera
	import org.papervision3d.render.*;// And we need a renderer
	import org.papervision3d.view.Viewport3D;// We need a viewport
	import org.papervision3d.materials.*;
	import org.papervision3d.events.InteractiveScene3DEvent;
	import " + WMTipoAssembly.TipoAssembly[0].nomeClassAs + ";");
                    escrever.WriteLine(@"public class Main extends Sprite {
		static public  var SCREEN_WIDTH  :int ="+Config.TEMPLATE_WIDTH+ @";
        static public  var SCREEN_HEIGHT :int ="+Config.TEMPLATE_HEIGHT+ @";
        public var viewport:Viewport3D;// The Viewport
		public var renderer:BasicRenderEngine;// Rendering engine
		// -- Scenes -- //
		public var default_scene:Scene3D;// A Scene
		// -- Cameras --//
		public var default_camera:DebugCamera3D;// A Camera
		public var rootAssembly:Assembly=new" + WMTipoAssembly.TipoAssembly[0].nomeClassAs + "(\"" + WMTipoAssembly.TipoAssembly[0].assemblyMesmoTipo[0].nome + "\",1,0,0,0,0,1,0,0,0,0,1,0);");
                    escrever.WriteLine(@"public function init():void {
			initPapervision(SCREEN_WIDTH, SCREEN_HEIGHT);// Initialise papervision
			init3d();// Initialise the 3d stuff..
			init2d();// Initialise the interface..
			initEvents();// Set up any event listeners..
		}
		protected function initPapervision(vpWidth:Number, vpHeight:Number):void {
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
		}
		public function init3d():void {
			default_scene.addChild(rootAssembly);
		}
		protected function init2d():void {
			// This function should create all of the 2d items
			// that will be overlayed on your papervision project.
			// User interfaces, Heads up displays etc.
		}

		protected function initEvents():void {
			// This function makes the onFrame function get called for
			// every frame.
			addEventListener(Event.ENTER_FRAME, onEnterFrame);
			// This line of code makes the onEnterFrame function get
			// called when every frame is entered.
		}

		protected function processFrame():void {
			// Process any movement or animation here.

		}
		protected function onEnterFrame( ThisEvent:Event ):void {
			//We need to render the scene and update anything here.
			processFrame();
			renderer.renderScene(default_scene, default_camera, viewport);
		}


	}
} ");
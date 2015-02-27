package
{
import org.papervision3d.core.*;
import org.papervision3d.core.proto.*;
import org.papervision3d.core.geom.*;

import flash.display.BitmapData;

public class Peca extends Mesh3D

{
public var verts :Array;
public var faceAr:Array;
public var uvs :Array;
public var nome:String;
private function v(x:Number,y:Number,z:Number):void
{
verts.push(new Vertex3D(x,y,z));
}
private function uv(u:Number,v:Number):void
{
uvs.push(new NumberUV(u,v));
}
private function f(vn0:int, vn1:int, vn2:int, uvn0:int, uvn1:int,uvn2:int):void
{
	faceAr.push( new Face3D( [verts[vn0],verts[vn1],verts[vn2] ], null, [uvs[uvn0],uvs[uvn1],uvs[uvn2]] ) );
}
public function Peca( vertices:Array, uv:Array, faces:Array,material:MaterialObject3D=null, initObject:Object=null )
{
super( material, new Array(), new Array(), null, initObject );
verts = this.geometry.vertices;
faceAr= this.geometry.faces;
uvs   = new Array();

}

public function esconder() :void{
	this.visible = false;
}
public function mostrar():void {
	this.visible = true;
}

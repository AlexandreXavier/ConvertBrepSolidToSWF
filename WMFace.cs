using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    public class WMFace
    {
        public int v1;
        public int v2;
        public int v3;
        public int uv1;
        public int uv2;
        public int uv3;
        private List<WMVertice> vertices = new List<WMVertice>();
        private List<WMUV> uvs = new List<WMUV>();
        public WMFace(int v1, int v2, int v3, int uv1, int uv2, int uv3, List<WMVertice> V, List<WMUV> uvs)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.uv1 = uv1;
            this.uv2 = uv2;
            this.uv3 = uv3;
            vertices.Add(V[v1]);
            vertices.Add(V[v2]);
            vertices.Add(V[v3]);
            this.uvs.Add(uvs[uv1]);
            this.uvs.Add(uvs[uv2]);
            this.uvs.Add(uvs[uv3]);
        }
        public override String ToString()
        {
            String temp = "f(" + v1 + "?" + v2 + "?" + v3 + "?" + uv1 + "?" + uv2 + "?" + uv3 + ");";
            temp += Environment.NewLine;
            temp = temp.Replace(',', '.');
            temp = temp.Replace('?', ',');
            temp.Trim();
            return temp;
        }
    }
}

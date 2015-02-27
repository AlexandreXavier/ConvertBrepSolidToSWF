using System;
using System.Collections.Generic;
using System.Text;

//Class que vai defenir os Edges
namespace Cubo3
{
    public class WMEdge
    {
        public int IndEdge;//Index do segmento de recta AB
        public int A;//Index do vertice que define o ponto A
        public int B;//Index do vertice que define o ponto B
       
        //Index do Edge que é contituido por o Index dos vertices A a B 
        public WMEdge(int indedge, int a, int b)
        {
            setIndex(indedge, a, b);
        }
        public void setIndEdge(int indedge)
        {
            this.IndEdge = indedge;
        }
        public void setA(int a)
        {
            this.A = a;
        }
        public void setB(int b)
        {
            this.B = b;
        }
        public void setIndex(int indedge, int a, int b)
        {
            setIndEdge(indedge);
            setA(a);
            setB(b);
        }

        public override String ToString()
        {
            String temp = "edge("+ A + "," + B + ");";
            return temp;
        }

       
    }
}

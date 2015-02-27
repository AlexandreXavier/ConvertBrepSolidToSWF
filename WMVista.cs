using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Cubo3
{
    public class WMVista
    {
        public static List<String> listaVista = new List<string>();
        public double alpha = 1;
        public Boolean visible = true;
        public Color cor = Color.Empty;
        public String ficheiroTextura = "";
        public WMresource localizacaoTextura;
        public string tipoCompilacao = "Malha";
        public Inventor.Matrix matLocal = WMServicosMatrix.transGeo.CreateMatrix();

        public WMVista() { }

        public WMVista(string tipoCompilacao, Boolean visible, Color cor, double alpha)
        {
            this.alpha = alpha;
            this.cor = cor;
            this.visible = visible;
            this.tipoCompilacao = tipoCompilacao;

        }
        public WMVista(string tipoCompilacao, Boolean visible, WMresource localizacaoTextura)
        {
            this.visible = visible;
            this.tipoCompilacao = tipoCompilacao;
            this.localizacaoTextura = localizacaoTextura;
        }
        public WMVista(string tipoCompilacao, Boolean visible)
        {
            this.tipoCompilacao = tipoCompilacao;
            this.visible = visible;
        }
        public WMVista(string tipoCompilacao, Boolean visible, Color cor, double alpha, WMresource localizacaoTextura)
        {
            this.alpha = alpha;
            this.cor = cor;
            this.visible = visible;
            this.tipoCompilacao = tipoCompilacao;
            this.localizacaoTextura = localizacaoTextura;

        }
        public void setMatriz(Inventor.Matrix temp)
        {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++)
                {
                    matLocal.set_Cell(i, j, temp.get_Cell(i, j));
                }    
            }
        }
        public WMVista copiar()
        {
            return new WMVista(tipoCompilacao, visible, cor, alpha, localizacaoTextura);
        }
       
    }
}

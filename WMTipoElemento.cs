using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
namespace Cubo3
{
    public abstract class WMTipoElemento
    {
        
        public String nome = "";
        public String caminho = "";
        public WMAssembly pai;
        public int index;
        public Array matrixTrans = new Double[12];
        public Inventor.Matrix matGlobal = WMServicosMatrix.transGeo.CreateMatrix();

        public Inventor.Matrix matLocal = WMServicosMatrix.transGeo.CreateMatrix();
        public Boolean compilar = true;
        //public double alpha = 1;
        //public Boolean visible = true;
        //public Color cor = Color.Empty;
        //public String ficheiroTextura="";
        //public WMresource localizacaoTextura;
        //public string tipoCompilacao="Malha";
        public Inventor.ComponentOccurrence ocurrencia; //ocurencia relativa a este assembly
        public String nomeClassAs;
        public WMVertice centroMassa;
        public List<WMVertice> rangeBox = new List<WMVertice>(2);
        public Inventor.Vector vectorTransTemp = WMServicosMatrix.transGeo.CreateVector(0, 0, 0);
        public int corTeste = 0;
        public List<WMVista> vistas = new List<WMVista>();
        public WMVista vistaActual = new WMVista();
        public void calcularCentroMassa()
        {
            
            centroMassa = new WMVertice((rangeBox[1].X + rangeBox[0].X) / 2, (rangeBox[1].Y + rangeBox[0].Y) / 2, (rangeBox[1].Z + rangeBox[0].Z) / 2);
        }
        
        public abstract void setRangeBox();
    


        public abstract MyTreeNode criarNo();
        public abstract void criarActionScript();

        public static WMTipoElemento objectoSeleccionado;
        public static WMTipoElemento ObjectoSeleccionado
        {
            get { return objectoSeleccionado; }
            
        }
            

        public void setCaminho()
        {
            //cria o caminho baseado no pai
            if (pai.caminho.Equals(""))
            {
                this.caminho = "" + index;
            }
            else
            {
                this.caminho = this.pai.caminho + "|" + index;
            }

        }
        public void setNomeClassAs(String tipo)
        {
            this.nomeClassAs = tipo + caminho;
           nomeClassAs=WmServicos.limpaString(nomeClassAs);
            
        }
       // public abstract void centrarEixos();
        public void addTranslation(WMVertice t)
        {
            this.vectorTransTemp.X += t.X;
            this.vectorTransTemp.Y += t.Y;
            this.vectorTransTemp.Z += t.Z; 
        }
        public void definirVectorMatrix()
        {
            this.matrixTrans = WMServicosMatrix.matrixToArray(matLocal);
        }
        [CategoryAttribute("Compilar")]
        public Boolean Compilar
        {
            get { return compilar; }
            set
            {
                MyTreeNode temp = frmCubo.formulario.getItemFromTree(caminho);
                compilar = value;
                temp.Checked = compilar;
            }
        }
        
        //private int CorTeste
        //{
        //    get { return corTeste; }
        //    set
        //    {
        //        corTeste = value;
        //        WmServicos.proxy.Call("changeMaterialColor", caminho, corTeste);
        //    }

        //}
        [CategoryAttribute("Propriedades do objecto")]
        public String Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                System.Windows.Forms.TreeView tre = frmCubo.formulario.getTreeList();
                MyTreeNode temp = frmCubo.formulario.getItemFromTree(caminho);
                tre.LabelEdit = true;
                tre.BeginUpdate();
                temp.BeginEdit();
                temp.Name = nome;

                temp.EndEdit(false);
                tre.Update();
                tre.EndUpdate();

                tre.LabelEdit = false;
                // frmCubo.formulario.criarTree();
                // frmCubo.formulario.selectInTree(temp.item.caminho);
                //.getItemFromTree(caminho);
            }
        }
        //[CategoryAttribute("Propriedades do objecto"),EditorAttribute(typeof(System.Windows.Forms.Design.), typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(false)]
        public double Alpha
        {
            get { return vistaActual.alpha*100; }
            set
            {
                 vistaActual.alpha = value/100;
                 WmServicos.proxy.Call("changeAlpha", caminho, vistaActual.alpha.ToString().Replace(',', '.'));
                  
            }
        }
       [Browsable(false)]
        public Color Cor
        {
            get
            {
                return vistaActual.cor;
            }
            set
            {
                try
                {
                    if (!vistaActual.cor.Equals(value))
                    {
                        vistaActual.cor = value; 
                        WmServicos.proxy.Call("changeMaterialColor", caminho, colorRGB());
                    }
                }
                catch (Exception erro)
                {
                    int teste = 9;
                }
            }
        }
        //[EditorAttribute(typeof(WMFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(false)]
        public string FicheiroTextura
        {
            get
            {
                return vistaActual.ficheiroTextura;
            }
            set
            {

                if (!vistaActual.ficheiroTextura.Equals(value))
                {
                    limparFicheiro();
                    vistaActual.localizacaoTextura = WMResources.addResource(value);
                    vistaActual.localizacaoTextura.items.Add(this);
                    vistaActual.ficheiroTextura = value;
                    WmServicos.proxy.Call("changeMaterialImagem", caminho, @"resources\" + vistaActual.localizacaoTextura.nomeFinal);
                }
                
            }
        }

        [TypeConverter(typeof(TipoDeCompilcao))]
        public String TipoCompilacao
        {
            get
            {
                return vistaActual.tipoCompilacao;
            }
            set
            {
                if (value.Equals("Imagem"))
                {
                    limparCor();
                }
                else if (value.Equals("Cor"))
                {
                    limparFicheiro();
                }
                else
                {
                    limparCor();
                    limparFicheiro();
                    int f = 9;
                    int g = 9;
                    try
                    {
                        WmServicos.proxy.Call("changeMaterialWire", caminho);
                        //int t = 9 / (f-g);
                    }
                    catch (Exception erro)
                    {
                        int teste = 9;
                    }
                }
                vistaActual.tipoCompilacao = value;
            }
        }
        public void limparCor()
        {
            vistaActual.cor = Color.Empty;
        }
        public void limparFicheiro()
        {
            if (vistaActual.ficheiroTextura != null)
            {
                if (vistaActual.localizacaoTextura != null && vistaActual.localizacaoTextura.removeItem(this))
                {
                    
                   //WMResources.removeResource(localizacaoTextura);
                }
                vistaActual.localizacaoTextura = null;
                vistaActual.ficheiroTextura = "";
            }

        }
        public int colorRGB()
        {
            //return string.Concat("#", (cor.ToArgb() & 0x00FFFFFF).ToString("X6"));
            return vistaActual.cor.ToArgb() & 0x00ffffff;
        }

        [CategoryAttribute("Propriedades do objecto")]
        public Boolean Visible
        {
            get { return vistaActual.visible; }
            set
            {
                vistaActual.visible = value;

            }
        }
        public abstract void guardarVista();
      
        public void usarVista(int i)
        {
            WMVista temp = vistas[i];
            vistaActual = temp;
            //call flash vista
            
        }

    }
}




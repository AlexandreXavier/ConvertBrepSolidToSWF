using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

//using System.IO;

namespace Cubo3
{
    public class WMPeca:WMTipoElemento
    {
        public Inventor.SurfaceBody WMoSurfaceBody;// a superficie da peca
        public WMTipoPeca tipo;
        public static int teste = 0;
        public Boolean carregado = false;
        #region Construtor
        public WMPeca(Inventor.ComponentOccurrence ocurrencia,WMAssembly pai)
        {
            
            //pecas normais
            this.WMoSurfaceBody = ocurrencia.SurfaceBodies[1];
            //int i = WMoSurfaceBody.Faces[0].TextureMaps.Count;
            this.ocurrencia = ocurrencia;
            
            //define o pai
            this.pai = pai;
            this.index = pai.elementos.Count;
            this.nome = ocurrencia.Name;
            //set caminho
            setCaminho();
            setNomeClassAs("P");
            this.tipo = WMTipoPeca.addTipoPeca(ocurrencia);
            //tipo.run2(WMoSurfaceBody);
            //Matrizes de transformação
            this.matGlobal = WMServicosMatrix.getMatGlobal(this.ocurrencia);
            this.matLocal = WMServicosMatrix.getMatLocal(pai, matGlobal);
            if (ocurrencia.Type == Inventor.ObjectTypeEnum.kComponentOccurrenceObject)
            {
            }
            if (!tipo.carregado)
            {

                if (ocurrencia.Type.ToString().Equals("kComponentOccurrenceObject"))
                {
                    tipo.run2(WMoSurfaceBody);
                    tipo.carregarGeo();
                    this.carregarGeo();
                    this.carregado = true;
                }
            }   else {
                this.carregarGeo();
                this.carregado = true;
            }
            //adiciona a ref as pecas do mesmo tipo(mesma geomtria)
            this.tipo.mesmoTipo.Add(this);

            //if (!tipo.carregado)
            //{
            //    if (ocurrencia.Type.ToString().Equals("kComponentOccurrenceObject"))
            //    {
            //        tipo.run2(WMoSurfaceBody);
            //        //tipo.run();
            //        tipo.carregarGeo();
            //    }
            //}
            //else {
            //    this.carregarGeo();
            //    this.carregado = true;
            //}
            
        }
        public WMPeca(Inventor.SurfaceBody oSurfaceBody, int index, WMAssembly pai, String iptPath, String displayName)
        {
            //peca unica
            this.WMoSurfaceBody = oSurfaceBody;
            this.ocurrencia = null;
            this.tipo = WMTipoPeca.addTipoPeca(iptPath);
            tipo.mesmoTipo.Add(this);
            this.pai = pai;
            this.index = index;
            this.nome = displayName;
            //caminho
            setCaminho();
            setNomeClassAs("P");
            //adiciona a ref as pecas do mesmo tipo(mesma geomtria)
            this.tipo.mesmoTipo.Add(this);
            
            //Matrizes
            this.matGlobal = WMServicosMatrix.getMatGlobal(this.ocurrencia);
            this.matLocal = WMServicosMatrix.getMatLocal(pai, matGlobal);
            this.matrixTrans = WMServicosMatrix.matrixToArray(matLocal);
        }
        
        #endregion

        public void carregarGeo()
        {
            setRangeBox();
            calcularCentroMassa();
            Inventor.Vector temp = WMServicosMatrix.transGeo.CreateVector(tipo.centroMassa.X, tipo.centroMassa.Y, tipo.centroMassa.Z);
            temp.TransformBy(matLocal);
            vectorTransTemp.AddVector(temp);     
        }
        public override void criarActionScript()
        {
            try
            {
                Console.WriteLine("Começou peca:" + this.nome);
                SavePecaPreview temp = new SavePecaPreview(nome, nomeClassAs, tipo.nomeClassAs, matrixTrans);
                Console.WriteLine("Começou peca:" + this.nome);
            }
            catch (Exception error)
            {
                Console.WriteLine("Bloqueou peca:" + this.nome);
            }
        }
        public override MyTreeNode criarNo()
        {
            return new MyTreeNode(this, this.nome);
        }
        public override void setRangeBox()
        {
            rangeBox.Add(WmServicos.inventorPointToWMvertice(WMoSurfaceBody.RangeBox.MinPoint));
            rangeBox.Add(WmServicos.inventorPointToWMvertice(WMoSurfaceBody.RangeBox.MaxPoint));
        }
        public override void guardarVista()
        {
            vistas.Add(vistaActual);
            vistaActual = vistaActual.copiar();
        }
    }
}

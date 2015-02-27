using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Cubo3
{

    public class WMAssembly : WMTipoElemento 
    {
        //occurrencias de peças e assemblys
        List<Inventor.ComponentOccurrence> ocurrencias = new List<Inventor.ComponentOccurrence>();
        public WMTipoAssembly tipo; //tipo de assembly em causa
        public List<WMTipoElemento> elementos = new List<WMTipoElemento>();     
        
        #region Construtor Raiz
       
        public WMAssembly(Inventor.ComponentOccurrences Oocurrencias, String iamPath, String displayName,Inventor.ComponentDefinition componentDefinition)
        {
            this.rangeBox.Add(WmServicos.inventorPointToWMvertice(componentDefinition.RangeBox.MinPoint));
            this.rangeBox.Add(WmServicos.inventorPointToWMvertice(componentDefinition.RangeBox.MaxPoint));
            WmServicos.MaxPoint.SetValue(componentDefinition.RangeBox.MaxPoint.X,0);
            WmServicos.MaxPoint.SetValue(componentDefinition.RangeBox.MaxPoint.Y, 1);
            WmServicos.MaxPoint.SetValue(componentDefinition.RangeBox.MaxPoint.Z, 2);
            WmServicos.MinPoint.SetValue(componentDefinition.RangeBox.MinPoint.X, 0);
            WmServicos.MinPoint.SetValue(componentDefinition.RangeBox.MinPoint.Y, 1);
            WmServicos.MinPoint.SetValue(componentDefinition.RangeBox.MinPoint.Z, 2);
           
           
            this.calcularCentroMassa();
            WmServicos.centro = this.centroMassa;
            //Cria o assembly contentor o raiz
            //adiciona as ocurrencias que contem num vector
            foreach (Inventor.ComponentOccurrence occurTemp in Oocurrencias)
            {
                this.ocurrencias.Add(occurTemp);
                
            }
            //Guarda variaveis de identificação
            this.caminho = "";
            this.nome = displayName;
            setNomeClassAs("A");
            tipo = WMTipoAssembly.addTipoAssembly(iamPath, this.nome);
            tipo.mesmoTipo.Add(this);
            //calculo das matrizes
            this.matGlobal = WMServicosMatrix.getMatGlobal(this.ocurrencia);
            this.matLocal = WMServicosMatrix.getMatLocal(pai, matGlobal);
            
            Inventor.Vector temp=WMServicosMatrix.transGeo.CreateVector(0,0,0);
            matLocal.SetTranslation(temp,false);
            this.matrixTrans = WMServicosMatrix.matrixToArray(matLocal);
            //carregar dados de todos os seus elementos
           // Thread threadTemp = new Thread(new ThreadStart(carregarDados));
            //threadTemp.Start();
            //frmCubo.thread.Add(threadTemp);

            //carregarDados();
            try
            {
                carregarDados();
            }
            catch (Exception erro)
            {
                int teste = 9;
            }
            
        }
        #endregion
        #region Construtor Assembly correspondente a uma peca
        public WMAssembly(Inventor.SurfaceBody sFB, String iptPath, String displayName)
        {
            //Construtor do assembly caso o ficheiro seja uma peca ipt
            this.caminho = "";
            this.nome = "soPeca";
            //calcular matrizes
            this.matGlobal = WMServicosMatrix.getMatGlobal(this.ocurrencia);
            this.matLocal = WMServicosMatrix.getMatLocal(pai, matGlobal);
//            this.matrixTrans = WMServicosMatrix.matrixToArray(matLocal);
  
            setNomeClassAs("A");
            //carregar dados
            tipo = WMTipoAssembly.addTipoAssembly("\\soPeca.iam", nome);
            tipo.mesmoTipo.Add(this);

            elementos.Add(new WMPeca(sFB, elementos.Count, this, iptPath, displayName));
            //WmElementoAssem tempElemento = new WmElementoAssem();
            //tempElemento.tipo = tempPeca.tipoPeca;
            //tempElemento.matrix = tempPeca.matrixTrans;
            //tempElemento.nome = tempPeca.nome;
            //tipoAssembly.elementos.Add(tempElemento);

        }
        #endregion
        #region Construtor subassembly e pecas
        public WMAssembly(Inventor.ComponentOccurrence ocurrencia, WMAssembly pai)
        {
            //subassembly e varias peças
            foreach (Inventor.ComponentOccurrence occurTemp in ocurrencia.SubOccurrences)
            {
                ocurrencias.Add(occurTemp);
            }
            //identificação do elemento
            this.nome = ocurrencia.Name;
            String iamPath = ocurrencia.ReferencedFileDescriptor.FullFileName;
            this.pai = pai;
            this.index = pai.elementos.Count;
            setCaminho();
            setNomeClassAs("A");
            this.ocurrencia = ocurrencia;
            //this.setCentroMassa();
            this.setRangeBox();
            this.calcularCentroMassa();
            tipo = WMTipoAssembly.addTipoAssembly(iamPath, this.nome);
            tipo.mesmoTipo.Add(this);
            //calcular matrizes
            this.matGlobal = WMServicosMatrix.getMatGlobal(this.ocurrencia);
            this.matLocal = WMServicosMatrix.getMatLocal(pai, matGlobal);
            Inventor.Vector temp = WMServicosMatrix.transGeo.CreateVector(centroMassa.X, centroMassa.Y,centroMassa.Z);
            temp.TransformBy(matLocal);
            vectorTransTemp.AddVector(temp);
            //this.matrixTrans = WMServicosMatrix.matrixToArray(matLocal);
            //carregar dados
            //Thread threadTemp = new Thread(new ThreadStart(carregarDados));
            //threadTemp.Start();
            //frmCubo.thread.Add(threadTemp);
            //threadTemp.Join();
            try
            {
                carregarDados();
            }
            catch (Exception erro)
            {
                int teste = 9;
            }
        }
        #endregion

        #region Metodos do Assembly

        //Metodo que vai carregar o contentor com os dados do inventor para a aplicação
        public void carregarDados()
        {
            Console.WriteLine("Adcionado o assembly " + nome);
            foreach (Inventor.ComponentOccurrence occurTemp in ocurrencias)
            {
                Inventor.Matrix temp = WMServicosMatrix.transGeo.CreateMatrix();
                //Determina se a ocurrencia é um assembly ou uma part
                if (occurTemp.DefinitionDocumentType == Inventor.DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    if (WMPeca.teste == 6)
                    {
                        int teste = 9;
                    }
                    WMAssembly tempAssemb = new WMAssembly(occurTemp, this);
                    tempAssemb.addTranslation(centroMassa.negVertice());
                    temp.SetToIdentity();
                    temp.SetTranslation(tempAssemb.vectorTransTemp, false);
                    tempAssemb.matLocal.MultiplyBy(temp);
                    tempAssemb.definirVectorMatrix();
                    elementos.Add(tempAssemb);
                }
                else if (occurTemp.DefinitionDocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject)
                {
                    WMPeca tempPeca= new WMPeca(occurTemp, this);
                    tempPeca.addTranslation(centroMassa.negVertice());
                    temp.SetToIdentity();
                    temp.SetTranslation(tempPeca.vectorTransTemp,false);
                    tempPeca.matLocal.MultiplyBy(temp);
                    tempPeca.definirVectorMatrix(); //possivel erro
                    elementos.Add(tempPeca);

                }
            }

        }
        public override void criarActionScript()
        {
            //ja tenho que enviar o nome da classe e o tipo para ele o estender a classe tipo e definir a sua nova classe
            //é aqui que tenho que inserir as matrix
            Console.WriteLine("Começou Ãssembly:" + this.nome);
            SaveAssemblyPreview temp = new SaveAssemblyPreview(this.nome,this.nomeClassAs,this.tipo.nomeClassAs,this.matrixTrans);
            
            for (int i = 0; i < elementos.Count; i++)
            {
                elementos[i].criarActionScript();
                if (elementos[i] is WMPeca)
                {
                    temp.escreverPeca(((WMPeca)elementos[i]).nomeClassAs);
                }
                else if (elementos[i] is WMAssembly)
                {
                   temp.escreverAssembly(((WMAssembly)elementos[i]).nomeClassAs);
   
                }
            }
            temp.fecharFicheiro();
            Console.WriteLine("terminou assembly:" + this.nome);
        }
        public override MyTreeNode criarNo()
        {
            MyTreeNode no = new MyTreeNode(this, this.nome);
            for (int i = 0; i < elementos.Count; i++)
            {
                no.Nodes.Add(elementos[i].criarNo());
                no.childSelect++;
            }
            return no;

        }
        #endregion
        public override void setRangeBox()
        {
            rangeBox.Add(WmServicos.inventorPointToWMvertice(ocurrencia.RangeBox.MinPoint));
            rangeBox.Add(WmServicos.inventorPointToWMvertice(ocurrencia.RangeBox.MaxPoint));         
        }

        public override void guardarVista()
        {
            if (this.pai == null)
            {
                WMVista.listaVista.Add(frmCubo.formulario.textBox2.Text.Equals("") ? ""+WMVista.listaVista.Count : frmCubo.formulario.textBox2.Text);
                frmCubo.formulario.listBox1.DataSource = WMVista.listaVista.ToArray();
                frmCubo.formulario.listBox1.Enabled = true;
                frmCubo.formulario.listBox1.Refresh();
                //frmCubo.formulario.cm.Refresh();
                Inventor.Matrix copia = WMServicosMatrix.transGeo.CreateMatrix();
                String temp = (String)WmServicos.proxy.Call("getMatriz", null);
                //temp = WMServicosConver.converterMatrixStringEmArray(temp);
                Array temp2 = WMServicosConver.converterMatrixStringEmArray(temp);
                //WMServicosMatrix.matrixToArray(copia);
                copia.PutMatrixData(ref temp2);
                //int teste = 9;
                
            }
            vistas.Add(vistaActual);
            vistaActual = vistaActual.copiar();
            for (int i = 0; i < elementos.Count; i++)
            {
                elementos[i].guardarVista();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;

namespace Cubo3
{
    public class WMTipoPeca : WmTipo
    {
        public static List<WMTipoPeca> TipoPecas = new List<WMTipoPeca>(); //todos os tipos de peças
        public List<WMPeca> mesmoTipo = new List<WMPeca>(); //Pecas do mesmo tipo
        public static List<Inventor.SurfaceBody> surfs = new List<Inventor.SurfaceBody>();
        public Inventor.SurfaceBody WMoSurfaceBody;
        public double WMBestTolerance;//variavel que encontra a melhor tolerancia para cada body
        private List<WMVertice> vertices = new List<WMVertice>();
        private List<WMFace> faces = new List<WMFace>();
        private List<WMUV> uvs = new List<WMUV>();
        private List<int> indVertices = new List<int>();//vector que transforma o index do vector com repetidos um index do vector sem repetidos
        private List<int> indUV = new List<int>();//vector que transforma o index do vector com repetidos um index do vector sem repetidos
        private int nIndF;
        private System.Array adVertexCoords = new double[0];
        private System.Array adNormalVectors = new double[0];
        private System.Array aiVertexIndices = new int[0];
        private System.Array uvTexture = new double[0];
        private int iVertexCount;
        private int iFacetCount;
        private System.Array adVertexCoords1 = new double[0];
        private System.Array adNormalVectors1 = new double[0];
        private System.Array aiVertexIndices1 = new int[0];
        private System.Array uvTexture1 = new double[0];
        private int iVertexCount1;
        private int iFacetCount1;
        private System.Array adVertexCoords2 = new double[0];
        private System.Array adNormalVectors2 = new double[0];
        private System.Array aiVertexIndices2 = new int[0];
        private System.Array uvTexture2 = new double[0];
        private int iVertexCount2;
        private int iFacetCount2;
        private Inventor.ApprenticeServerComponent objapprenticeServerApp = new Inventor.ApprenticeServerComponentClass();
        private Inventor.ComponentDefinition ocurrencia;
        public WMVertice centroMassa;
        public List<WMVertice> rangeBox = new List<WMVertice>(2);
        private List<WMEdge> edges = new List<WMEdge>();
        public Boolean carregado = false;

        //public WMTipoPeca(String iptPath, String nomeClassAs)
        //{
        //    this.path = iptPath;
        //    this.nomeClassAs = nomeClassAs;
        //    Console.WriteLine("Peca " + nomeClassAs);
        //    //this.run();
        //    Thread temp = new Thread(new ThreadStart(this.run));
        //    temp.Start();
        //    frmCubo.thread.Add(temp);
        //    temp.Join();
        //}

        public WMTipoPeca(String iptPath, String nomeClassAs)
        {
            this.path = iptPath;
            this.nomeClassAs = nomeClassAs;
            Console.WriteLine("Peca " + nomeClassAs);
            //this.run();
            //Thread temp = new Thread(new ThreadStart(this.run));
            //temp.Start();
            //frmCubo.thread.Add(temp);
            //temp.Join();
        }

        //adiciona o tipo de peça caso não exista
        //devolve sempre o tipo de peça 
        public static WMTipoPeca addTipoPeca(Inventor.ComponentOccurrence occur)
        {
            String iptPath = occur.ReferencedFileDescriptor.FullFileName;
            WMTipoPeca tipoPecaRes;
            int indexRes = verificarExistencia(iptPath);
            if (indexRes == -1)
            {
                String nomeClassAs = "PP_" + WmServicos.getNomeClassePrincipal(iptPath);
                nomeClassAs = WmServicos.limpaString(nomeClassAs);
                //tipoPecaRes = new WMTipoPeca(iptPath, nomeClassAs);
                tipoPecaRes = new WMTipoPeca(iptPath,nomeClassAs);
                TipoPecas.Add(tipoPecaRes);
            }
            else
            {
                tipoPecaRes = TipoPecas[indexRes];
            }

            return tipoPecaRes;
        }
        
        public static WMTipoPeca addTipoPeca(String iptPath)
        {
            WMTipoPeca tipoPecaRes;
            int indexRes = verificarExistencia(iptPath);
            if (indexRes == -1)
            {
                String nomeClassAs = "PP_" + WmServicos.getNomeClassePrincipal(iptPath);
                nomeClassAs = WmServicos.limpaString(nomeClassAs);
                tipoPecaRes = new WMTipoPeca(iptPath, nomeClassAs);

                TipoPecas.Add(tipoPecaRes);
            }
            else
            {
                tipoPecaRes = TipoPecas[indexRes];
            }
            return tipoPecaRes;
        }

        public void run()
        {
            objapprenticeServerApp = new Inventor.ApprenticeServerComponentClass();       
            adVertexCoords = new double[0];
        adNormalVectors = new double[0];
        aiVertexIndices = new int[0];
        uvTexture = new double[0];
        iVertexCount=new int();
        iFacetCount = new int();
            Inventor.ApprenticeServerDocument objapprenticeServerDocument;
            objapprenticeServerDocument = objapprenticeServerApp.Open(path);
            //objapprenticeServerApp.Close();
            WMoSurfaceBody = objapprenticeServerDocument.ComponentDefinition.SurfaceBodies[1];
            
           WMBestTolerance = this.getBestTolerance();
           this.carregado = true;
            //WMBestTolerance = 1;
            
            WMoSurfaceBody.CalculateFacetsAndTextureMap(WMBestTolerance, out iVertexCount, out iFacetCount, out adVertexCoords, out adNormalVectors, out aiVertexIndices, out uvTexture);
            int tetes = 9;
            this.getVertex();
            this.getEdges(); 
            this.getUV();
            this.getFaces();
           ocurrencia=objapprenticeServerDocument.ComponentDefinitions[0];
            
            setRangeBox();
           calcularCentroMassa();
            
           for (int i = 0; i < vertices.Count; i++)
           {
               vertices[i].subVertice(centroMassa);
           }
           Console.WriteLine("Peca " + nomeClassAs +"  terminou");
            
        }
        public void run2(Inventor.SurfaceBody surfaceBody)
        {

            objapprenticeServerApp = new Inventor.ApprenticeServerComponentClass();
            adVertexCoords = new double[0];
            adNormalVectors = new double[0];
            aiVertexIndices = new int[0];
            uvTexture = new double[0];
            iVertexCount = new int();
            iFacetCount = new int();
            
            WMoSurfaceBody = null;
            Inventor.SurfaceBody sft;
                Inventor.ApprenticeServerDocument objapprenticeServerDocument;
                objapprenticeServerDocument = objapprenticeServerApp.Open(path);
                WMoSurfaceBody = objapprenticeServerDocument.ComponentDefinition.SurfaceBodies[1];
                objapprenticeServerApp.Close();
                objapprenticeServerDocument.Close();
           // objapprenticeServerDocument.Thumbnail
               // setRangeBox();
                //calcularCentroMassa();
               // surfs.Add(surfaceBody);
                //Inventor.SurfaceBody teste2 = ((Inventor.SurfaceBodyProxy)surfaceBody).NativeObject;
                //String type = surfaceBody.get_Volume(100).ToString();
                //if (surfaceBody is Inventor.SurfaceBodyProxy)
                //{
                //    int teste = 9;
                //}
                //else {
                //    int teste = 9;
                //}
                //Inventor.SurfaceBody temp = WMoSurfaceBody;
               // WMoSurfaceBody = surfaceBody;
                WMBestTolerance = this.getBestTolerance();
                //WMBestTolerance = 1;
               
                //WMoSurfaceBody.CalculateFacetsAndTextureMap(WMBestTolerance, out iVertexCount, out iFacetCount, out adVertexCoords, out adNormalVectors, out aiVertexIndices, out uvTexture);
                //WMBestTolerance = this.getBestTolerance(teste2);
                //surfaceBody.CalculateFacetsAndTextureMap(WMBestTolerance, out iVertexCount1, out iFacetCount1, out adVertexCoords1, out adNormalVectors1, out aiVertexIndices1, out uvTexture1);
                WMoSurfaceBody=surfaceBody.ComponentDefinition.SurfaceBodies[1];
                    WMoSurfaceBody.CalculateFacetsAndTextureMap(WMBestTolerance, out iVertexCount, out iFacetCount, out adVertexCoords, out adNormalVectors, out aiVertexIndices, out uvTexture);
                    int teste = 9;   
            this.getVertex();
             this.getEdges();
                this.getUV();
                this.getFaces();
                setRangeBox();
                calcularCentroMassa();

                for (int i = 0; i < vertices.Count; i++)
                {
                    vertices[i].subVertice(centroMassa);
                }
                Console.WriteLine("Peca " + nomeClassAs + "  terminou");
                this.carregado = true;


        }
        public void carregarGeo()
        {
            lock (mesmoTipo)
            {
                for (int i = 0; i < mesmoTipo.Count; i++)
                {
                    mesmoTipo[i].carregarGeo();
                    mesmoTipo[i].carregado = true;
                }
            }
        }
        public static int verificarExistencia(String iptPath)
        {
            int res = -1;
            for (int i = 0; i < TipoPecas.Count; i++)
            {
                if (TipoPecas[i].path.Equals(iptPath))
                {
                    res = i;
                    break;
                }
            }
            return res;
        }

        private double getBestTolerance()
        {
            //Chose the best tolerance of the part 
            int ToleranceCount;
            int ToleranceCountedges;

            System.Array ExistingTolerances = new double[0];
            System.Array ExistingTolerancesegdes = new double[0];

            //double[] ExistingTolerances;
          
            this.WMoSurfaceBody.GetExistingFacetTolerances(out ToleranceCount, out ExistingTolerances);
            this.WMoSurfaceBody.GetExistingStrokeTolerances(out ToleranceCountedges, out ExistingTolerancesegdes);
            int teste = 9;
            for (int j = 0; j <= ToleranceCount - 1; j++)
            {
                if (j == 0)
                {
                    WMBestTolerance = (double)ExistingTolerances.GetValue(j);
                }
                else if ((double)ExistingTolerances.GetValue(j) < WMBestTolerance)
                {
                    WMBestTolerance = (double)ExistingTolerances.GetValue(j);
                }
            }
            return WMBestTolerance;
        }

        private double getBestTolerance(Inventor.SurfaceBody surface)
        {
            //Chose the best tolerance of the part 
            int ToleranceCount;
            int ToleranceCountedges;

            System.Array ExistingTolerances = new double[0];
            System.Array ExistingTolerancesegdes = new double[0];

            //double[] ExistingTolerances;

            surface.GetExistingFacetTolerances(out ToleranceCount, out ExistingTolerances);
            surface.GetExistingStrokeTolerances(out ToleranceCountedges, out ExistingTolerancesegdes);
            int teste = 9;
            for (int j = 0; j <= ToleranceCount - 1; j++)
            {
                if (j == 0)
                {
                    WMBestTolerance = (double)ExistingTolerances.GetValue(j);
                }
                else if ((double)ExistingTolerances.GetValue(j) < WMBestTolerance)
                {
                    WMBestTolerance = (double)ExistingTolerances.GetValue(j);
                }
            }
            return WMBestTolerance;
        }

        //Calcular os vertices do body
        private void getVertex()
        {
            
            // Create a instance of class Vertice
            WMVertice tempV;
            bool WMRepetido = false;
            //cria os diversos vertices
            for (int i = 0; i < adVertexCoords.Length / 3; i++)
            {
                tempV = new WMVertice((double)adVertexCoords.GetValue(3 * i), (double)adVertexCoords.GetValue(3 * i + 1), (double)adVertexCoords.GetValue(3 * i + 2));
                // cria o primeiro pois não é possível ele ser repetido
                if (i == 0)
                {
                    vertices.Add(tempV);
                    indVertices.Add(i);
                }
                else
                {
                    int j;
                    //verifica se o vertice já existe
                    for (j = 0; j < vertices.Count; j++)
                    {
                        WMRepetido = false;
                        if (tempV.Compara(vertices[j]) == true)
                        {
                            WMRepetido = true;
                            break;
                        }
                    }
                    //Se o vertice não for repetido cria um e adiciona o a sua nova posição ao vector de conversão
                    if (WMRepetido == false)
                    {
                        vertices.Add(tempV);
                        indVertices.Add(vertices.Count - 1);
                    }
                    //Se o vertice for repetido ele apenas adiciona o index ao vector de conversão
                    else
                    {
                        indVertices.Add(j);
                    }
                }
            }
            //FileStream ficheiro = new FileStream(Config.pathProjecto +"vert.txt", FileMode.Create);
            //StreamWriter escrever = new StreamWriter(ficheiro);
            //for (int i = 0; i < vertices.Count; i++)
            //{
            //    escrever.WriteLine(vertices[i].X + ";" + vertices[i].Y + ";" + vertices[i].Z);
            //}
            //escrever.Close();
        }

        //Calcular os UV do body
        //Atenção não esta igual ao vertices???
        private void getUV()
        {
        
                        //calcular os extremos dos uvs
            double UVmaxX=0;
            double UVmaxY=0;
            double UVminX=0;
            double UVminY=0;
            
            // Create a instance of class Vertice

            WMUV tempUV;
            bool WMRepetido = false;
            //cria os diversos vertices
            for (int i = 0; i < uvTexture.Length / 2; i++)
            {
                tempUV = new WMUV((double)uvTexture.GetValue(2 * i), (double)uvTexture.GetValue(2 * i + 1));
                // cria o primeiro pois não é possível ele ser repetido
                if (i == 0)
                {
                    uvs.Add(tempUV);
                    indUV.Add(i);
                    UVmaxX = tempUV.U;
                    UVminX = tempUV.U;
                    UVmaxY = tempUV.V;
                    UVminY = tempUV.V;
                }
                else
                {
                    if (tempUV.U > UVmaxX)
                    {
                        UVmaxX = tempUV.U;
                    }
                    else if (tempUV.U < UVminX)
                    {
                        UVminX = tempUV.U;
                    }
                    if (tempUV.V > UVmaxY)
                    {
                        UVmaxY = tempUV.V;
                    }
                    else if (tempUV.V < UVminY)
                    {
                        UVminY = tempUV.V;
                    }
                    int j;
                    //verifica se o uv já existe
                    for (j = 0; j < uvs.Count; j++)
                    {
                        WMRepetido = false;
                        if (tempUV.Compara(uvs[j]) == true)
                        {
                            WMRepetido = true;
                            break;
                        }
                    }
                    //Se o uv não for repetido cria um e adiciona o a sua nova posição ao vector de conversão
                    if (WMRepetido == false)
                    {
                        uvs.Add(tempUV);
                        indUV.Add(uvs.Count - 1);
                    }
                    //Se o uv for repetido ele apenas adiciona o index ao vector de conversão
                    else
                    {
                        indUV.Add(j);
                    }
                }
            }
        
                    //Encontrar o valor da razão de semelhança 
                    double Seg1;
                    double Seg2;
                    double razao;

                    Seg1 = Math.Sqrt(Math.Pow((UVminX - UVmaxX), 2) + Math.Pow((UVminY - UVminY), 2));
                    Seg2 = Math.Sqrt(Math.Pow((UVminX - UVminX), 2) + Math.Pow((UVminY - UVmaxY), 2));

                    if (Seg1 > Seg2)
                    {
                        razao = Seg1;
                    }
                    else
                    {
                        razao = Seg2;
                    }
                    // Transformar coordenadas 
                    for (int i=0;i<uvs.Count;i++){
                        uvs[i].setUV((uvs[i].U - UVminX) / razao, (uvs[i].V - UVminY) / razao);
                    }
                    int teste = 9;
                
            
            
            
            
            
            
            
            
            
            
            
            //List<WMUV> UVT = new List<WMUV>(); //lista de todos os UV
            ////calcular os extremos dos uvs
            //double UVmaxX;
            //double UVmaxY;
            //double UVminX;
            //double UVminY;
            //Boolean WMRepetido = false;
            //List<int> numUV = new List<int>();
            //int inc = 0; //serve para colocar no index em que começa o uvs de uma face
            //// Create a instance of class UV
            //WMUV tempUV = new WMUV(0, 0);
            ////Save the UV in the object
            //for (int i = 0; i < uvTexture.Length; i += 2)
            //{
            //    UVT.Add(new WMUV((double)uvTexture.GetValue(i), (double)uvTexture.GetValue(i + 1)));
            //}
            ////Calculate the number of vertex in this face and is the same as number os UV
            ////calcula número de vertices por cada face da peça
            ////numUV[0] é o número faces por peça os restantes elementos são o número de vertices por cada face da peça
            //numUV = ContarV();
            ////ciclo que corre todas as faces
            //for (int i = 1; i <= numUV[0]; i++)
            //{
            //    try
            //    {
            //        //Encontrar o valor max e min para o U e V .para otimizar o mapeamento da face
            //        //Find the max and min value of U and V to optimize the UV from the face
            //        UVmaxX = UVT[inc].U;
            //        UVminX = UVT[inc].U;
            //        UVmaxY = UVT[inc].V;
            //        UVminY = UVT[inc].V;

            //        for (int j = inc + 1; j <= inc + numUV[i] - 1/*calcula o index do ultimo uv desta face-> posição inicial(inc) + numero de uv desta face (numUV[i]) - 1 (porque é um vector que começa em 0) */; j++)
            //        {
            //            if (UVmaxX < UVT[j].U)
            //            {
            //                UVmaxX = UVT[j].U;
            //            }
            //            else if (UVminX > UVT[j].U)
            //            {
            //                UVminX = UVT[j].U;
            //            }
            //            if (UVmaxY < UVT[j].V)
            //            {
            //                UVmaxY = UVT[j].V;
            //            }
            //            else if (UVminY > UVT[j].V)
            //            {
            //                UVminY = UVT[j].V;
            //            }
            //        }

            //        //Encontrar o valor da razão de semelhança 
            //        double Seg1;
            //        double Seg2;
            //        double razao;

            //        Seg1 = Math.Sqrt(Math.Pow((UVminX - UVmaxX), 2) + Math.Pow((UVminY - UVminY), 2));
            //        Seg2 = Math.Sqrt(Math.Pow((UVminX - UVminX), 2) + Math.Pow((UVminY - UVmaxY), 2));

            //        if (Seg1 > Seg2)
            //        {
            //            razao = Seg1;
            //        }
            //        else
            //        {
            //            razao = Seg2;
            //        }
            //        // Transformar coordenadas 
            //        for (int j = inc; j <= inc + numUV[i] - 1; j++)
            //        {
            //            UVT[j].setUV((UVT[j].U - UVminX) / razao, (UVT[j].V - UVminY) / razao);
            //        }
            //        //Passa para a proxima face. e a primeira posicao da actual(inc) mais o numero de uv que a actual tem (numUV[i])
            //        inc = inc + numUV[i];
            //    }
            //    catch (Exception erro)
            //    {
            //        string text = erro.Message;
            //    }
            //}
            ////construir vector de uv sem repetidos
            //for (int i = 0; i < UVT.Count; i++)
            //{
            //    tempUV = UVT[i];

            //    // Criar o primeiro UV que não pode ser repetido 
            //    if (i == 0)
            //    {
            //        uvs.Add(tempUV);
            //        indUV.Add(i);

            //    }
            //    else
            //    {
            //        try
            //        {
            //            int j;
            //            for (j = 0; j < uvs.Count; j++)
            //            {
            //                WMRepetido = false;
            //                if (tempUV.Compara(uvs[j]) == true)
            //                {
            //                    WMRepetido = true;
            //                    break;
            //                }
            //            }
            //            if (WMRepetido == false)
            //            {
            //                uvs.Add(tempUV);
            //                indUV.Add(uvs.Count - 1);
            //            }
            //            else
            //            {
            //                indUV.Add(j);
            //            }
            //        }
            //        catch (Exception erro)
            //        {
            //            string text2 = erro.Message;
            //        }
            //    }
            //}
        }

        //Calcular as faces
        private void getFaces()
        {
            //Create the index for the faces
            for (int i = 0; i < aiVertexIndices.Length - 2/*= li - 3*/; i += 3)
            {
                try
                {

                    
                    faces.Add(new WMFace(indVertices[(int)aiVertexIndices.GetValue(i) - 1], indVertices[(int)aiVertexIndices.GetValue(i + 1) - 1], indVertices[(int)aiVertexIndices.GetValue(i + 2) - 1], indUV[(int)aiVertexIndices.GetValue(i) - 1], indUV[(int)aiVertexIndices.GetValue(i + 1) - 1], indUV[(int)aiVertexIndices.GetValue(i + 2) - 1], this.vertices, this.uvs));
                }
                catch (Exception erro)
                {
                    String test = erro.Message;
                }
            }
        }
        // Get the number of vertex in a face (int)
        private List<int> ContarV()
        {
            // Associate the face
            int iVertexCountLOC;
            int iFacetCountLOC;
            Inventor.Face oFaceInt;
            System.Array adVertexCoordsLOC = new double[0];
            System.Array adNormalVectorsLOC = new double[0];
            System.Array aiVertexIndicesLOC = new int[0];
            //System.Array nUV = new int[100];
            List<int> nUV = new List<int>();
            nUV.Add(0);
            int i;

            nIndF = 1;
            i = 1;
            foreach (Inventor.Face oFacei in WMoSurfaceBody.Faces)
            {
                oFaceInt = WMoSurfaceBody.Faces[nIndF];
                // Define os vectores para esta face (Vertices) 
                oFacei.CalculateFacets(WMBestTolerance, out iVertexCountLOC, out iFacetCountLOC, out adVertexCoordsLOC, out adNormalVectorsLOC, out aiVertexIndicesLOC);

                nUV.Add(iVertexCountLOC);
                i = i + 1;
                nIndF = nIndF + 1;
            }
            nUV[0] = nIndF - 1;
            return nUV;
        }
        //Cria e devolve uma string com os vertices uvs e faces listadas
        private string listaVerticesUVsFaces()
        {
            String temp = "";

            //adiciona todos os vertices ( v(x,y,z) ) à string
            for (int i = 0; i < vertices.Count; i++)
            {
                temp += vertices[i].ToString();
            }
            //adiciona todos os uvs ( uv(u,v) ) à string
            for (int i = 0; i < uvs.Count; i++)
            {
                temp += uvs[i].ToString();
            }
            //adiciona todas as faces ( f(v1,v2,v3,uv1,uv2,uv3) ) à string
            for (int i = 0; i < faces.Count; i++)
            {
                temp += faces[i].ToString();
            }
            //adiciona todos os edges (edge(v1,v2)) à string
             for (int i = 0; i < edges.Count; i++)
            {
               temp += edges[i].ToString();
            }
            return temp;
        }
        //Cria o ficheiro de as3 deste tipo de peca
        public override void criarActionScript()
        {
            Console.WriteLine("Começou peca principal:" + this.nomeClassAs);
            SavePecaPrincipal temp = new SavePecaPrincipal(nomeClassAs);
            temp.escreverCorpo(listaVerticesUVsFaces());
            temp.fecharFicheiro();
            Console.WriteLine("Terminou peca principal:" + this.nomeClassAs);

        }
        //metodo estatico que vai criar todos os ficheiros as3 de todas os tipos de peca
        public static void gerarAs3Files()
        {
            for (int i = 0; i < WMTipoPeca.TipoPecas.Count; i++)
            {
                //Thread temp = new Thread(new ThreadStart(WMTipoPeca.TipoPecas[i].criarActionScript));
                //temp.Start();
                //frmCubo.thread.Add(temp);
                WMTipoPeca.TipoPecas[i].criarActionScript();
            }
        }
        public void calcularCentroMassa()
        {
            //setRangeBox();
            centroMassa = new WMVertice((rangeBox[1].X + rangeBox[0].X) / 2, (rangeBox[1].Y + rangeBox[0].Y) / 2, (rangeBox[1].Z + rangeBox[0].Z) / 2);
        }

        public void setRangeBox()
        {
            rangeBox.Clear();
            rangeBox.Add(WmServicos.inventorPointToWMvertice(WMoSurfaceBody.RangeBox.MinPoint));
            rangeBox.Add(WmServicos.inventorPointToWMvertice(WMoSurfaceBody.RangeBox.MaxPoint));

        }

        //Calcular os Edges do body
        public void getEdges()
        {

            //variaveis locais 
            System.Array adEdgeVertexCoords = new double[0];
            System.Array aiEdgeVertexIndices = new int[0];
            int iEdgeVertexCount;
            int iEdgeSegmentCount;
            int IndA;
            int IndB;
            WMVertice tempEdgeV;
            List<WMVertice> EdgeV = new List<WMVertice>();
            WMEdge tempEdge;
            List<int> indexTransf = new List<int>();
            //calcular os edges da surfacebody
            this.WMoSurfaceBody.CalculateStrokes(WMBestTolerance, out iEdgeVertexCount, out iEdgeSegmentCount, out adEdgeVertexCoords, out aiEdgeVertexIndices);

            //cria os diversos vertices dos edges numa variavel temporaria

            for (int i = 0; i < adEdgeVertexCoords.Length / 3; i++)
            {
                tempEdgeV = new WMVertice((double)adEdgeVertexCoords.GetValue(3 * i), (double)adEdgeVertexCoords.GetValue(3 * i + 1), (double)adEdgeVertexCoords.GetValue(3 * i + 2));
                EdgeV.Add(tempEdgeV);
            }
            //procurar os vertices dos edges nos vertices
            Boolean flag = false;
            for (int j = 0; j < EdgeV.Count; j++)
            {
                flag = false;
                for (int s = 0; s < vertices.Count; s++)
                {
                    if (EdgeV[j].Compara(vertices[s]))
                    {
                        indexTransf.Add(s);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    indexTransf.Add(vertices.Count);
                    vertices.Add(EdgeV[j]);
                }
            }
            for (int j = 0; j < aiEdgeVertexIndices.Length ; j+=2)
            {
                try
                {
                    IndA = indexTransf[(int)aiEdgeVertexIndices.GetValue(j)-1];
                    IndB = indexTransf[(int)aiEdgeVertexIndices.GetValue(j + 1)-1];

                    tempEdge = new WMEdge(j, IndA, IndB);
                    edges.Add(tempEdge);
                }
                catch (Exception er)
                {
                    int t = 0;
                }

            }
            //FileStream ficheiro = new FileStream(Config.pathProjecto + "edvert.txt", FileMode.Create);
            //StreamWriter escrever = new StreamWriter(ficheiro);
            //for (int i = 0; i < EdgeV.Count; i++)
            //{
            //    escrever.WriteLine(EdgeV[i].X + ";" + EdgeV[i].Y + ";" + EdgeV[i].Z);
            //}
            //escrever.Close();
            //FileStream ficheiro2 = new FileStream(Config.pathProjecto + "edvertinde.txt", FileMode.Create);
            //StreamWriter escrever2 = new StreamWriter(ficheiro2);
            //for (int i = 0; i < aiEdgeVertexIndices.Length; i+=2)
            //{
            //    escrever2.WriteLine(aiEdgeVertexIndices.GetValue(i)+";" +aiEdgeVertexIndices.GetValue(i+1));
            //}
            //escrever2.Close();

        }

    }
}

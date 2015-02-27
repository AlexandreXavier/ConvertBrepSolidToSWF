using System;
using System.Collections.Generic;
using System.Text;

namespace Cubo3
{
    class WMServicosMatrix
    {

        public static Inventor.TransientGeometry transGeo;
        public static Inventor.Matrix copyMatrix(Inventor.Matrix original)
        {
            Inventor.Matrix copia = transGeo.CreateMatrix();
            for (int linha = 1; linha < 4; linha++)
            {
                for (int coluna = 1; coluna < 5; coluna++)
                {
                    copia.set_Cell(linha, coluna, original.get_Cell(linha, coluna));
                }
            }
            return copia;
        }
        public static Inventor.Matrix getMatGlobal(Inventor.ComponentOccurrence ocurrencia)
        {

            Inventor.Matrix matGlobal = transGeo.CreateMatrix();

            if (ocurrencia != null)
            {

                matGlobal = copyMatrix(ocurrencia.Transformation);
            }
            else
            {
                matGlobal.set_Cell(1, 1, 1); matGlobal.set_Cell(1, 2, 0); matGlobal.set_Cell(1, 3, 0); matGlobal.set_Cell(1, 4, 0);
                matGlobal.set_Cell(2, 1, 0); matGlobal.set_Cell(2, 2, 1); matGlobal.set_Cell(2, 3, 0); matGlobal.set_Cell(2, 4, 0);
                matGlobal.set_Cell(3, 1, 0); matGlobal.set_Cell(3, 2, 0); matGlobal.set_Cell(3, 3, 1); matGlobal.set_Cell(3, 4, 0);
            }
            return matGlobal;

        }
        public static Inventor.Matrix getMatLocal(WMAssembly pai, Inventor.Matrix matGlobal)
        {
            Inventor.Matrix matLocal = WMServicosMatrix.copyMatrix(matGlobal);
            if (pai != null)
            {
                Inventor.Matrix matGlobalPai = WMServicosMatrix.copyMatrix(pai.matGlobal);
                matGlobalPai.Invert();
                matLocal.MultiplyBy(matGlobalPai);
            }
            return matLocal;
        }
        public static Array matrixToArray(Inventor.Matrix matrix)
        {
            Array array = new Double[12];
            matrix.GetMatrixData(ref array);
            return array;
        }
    }
}

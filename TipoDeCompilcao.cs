using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cubo3
{
    class TipoDeCompilcao:StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[]{"Malha","Cor","Imagem"});
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}


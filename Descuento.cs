using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_parcial_2_completa
{
    public abstract class Descuento
    {
        public abstract decimal AplicarDescuento(decimal pAporte);


    }

    public class DescuentoInternacional : Descuento
    {

        public override decimal AplicarDescuento(decimal pAporte)
        {
            return pAporte * 0.99m;
        }

    }


    public class DescuentoNacional : Descuento
    {

        public override decimal AplicarDescuento(decimal pAporte)
        {
            return pAporte * 0.95m;
        }

    }

    public class DescuentoProvincial : Descuento
    {

        public override decimal AplicarDescuento(decimal pAporte)
        {
            return pAporte * 0.90m;
        }

    }



}

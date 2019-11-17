using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA3
{
    /// <summary>
    /// Объект входного нейрона
    /// </summary>
    class InputNeuron
    {
        double value;

        public InputNeuron()
        {

        }

        public double Value //Получение значнения x
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        //temp
    }

    //new changes
    class Axon
    {
        double omega;

        public double Omega
        {
            get
            {
                return omega;
            }
            set
            {
                this.omega = value;
            }
        }
    }

    class HideNeuron
    {

        public HideNeuron()
        {

        }
    }

    class OutputNeutron
    {

    }
}

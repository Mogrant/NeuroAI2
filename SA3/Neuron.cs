using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA3
{

    class Axon
    {
        double omega = 1;
        Neuron connectedNeuron;

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
        } //Возврат или присвоение omega

        /// <summary>
        /// Конструктор объекта аксона
        /// </summary>
        /// <param name="hn">ссылка соединяемого нейрона</param>
        public Axon(Neuron hn)
        {
            this.connectedNeuron = hn;
        }

        /// <summary>
        /// Результат аксона
        /// </summary>
        public double Result
        {
            get
            {
                return (double)(connectedNeuron.Result * omega);
            }
        }

        public Neuron ConnectedNeuron
        {
            get
            {
                return connectedNeuron;
            }
        }

    }

    class Neuron
    {
        int index;

        List<Axon> axons = new List<Axon>();

        double resultActivationFunc = 0;
        double correctlyResult = 0;
        double errorValue = 0;

        public Neuron(int index)
        {
            this.index = index;
        }

        public void AddAxon( Neuron connectionNeuron )
        {
            axons.Add(new Axon(connectionNeuron));
        }

        public void AddAxon(Neuron connectionNeuron, double omega)
        {
            axons.Add(new Axon(connectionNeuron));
            axons[axons.Count - 1].Omega = omega;
        }

        private double SumOfAllAxons
        {
            get
            {
                double summ = 0;
                for (int i = 0; i < axons.Count; i++)
                {
                    summ += axons[i].Result;
                }
                return summ;
            }
        }

        public double CalcActicationFunc()
        {
            resultActivationFunc = Math.Tanh(SumOfAllAxons);
            //resultFunc = SumOfAllAxons;
            return resultActivationFunc;
        }

        public double CalcErrorValue()
        {
            Func<double, double> f = SumOfAllAxons => Math.Tanh(SumOfAllAxons);
            this.errorValue = correctlyResult - resultActivationFunc * derivative(f, SumOfAllAxons, 0.01);
            return this.errorValue;
        }

        private double derivative(Func<double, double> f, double x, double dx)
        {
            return (f(x + dx) - f(x)) / dx;
        }

        public double Result
        {
            get
            {
                return this.resultActivationFunc;
            }
            set
            {
                if (axons.Count == 0)
                {
                    this.resultActivationFunc = value;
                }

                //Добавить вывод ошибки
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
        }

        public double ResultCorrectly
        {
            get
            {
                return this.correctlyResult;
            }
            set
            {
                this.correctlyResult = value;
            }
        }

        public List<Axon> Axons
        {
            get
            {
                return axons;
            }
        }
    }

    class OutputNeutron
    {

    }
}

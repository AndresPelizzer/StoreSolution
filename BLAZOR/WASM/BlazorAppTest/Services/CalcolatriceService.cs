using System.ComponentModel.Design;

namespace BlazorAppTest.Services
{
    public class CalcolatriceService
    {
        public double calcolatrice(double numero1, double numero2, string operazione)
        {
            if (operazione == "+")
            {
                return numero1 + numero2;
            }
            else if (operazione == "-")
            {
                return numero1 - numero2;
            }
            else if (operazione == "*")
            {
                return numero1 * numero2;
            }
            else if (operazione == "/")
            {
                if (numero2 == 0)
                {
                    throw new DivideByZeroException("Non puoi dividere per zero!");


                }
                else
                {
                    return numero1 / numero2;
                }
            }
            else
            {
                return 0;
            }
        }
        public double somma(double numero1, double numero2)
        {
            return numero1 + numero2;
        }
        public double differenza(double numero1, double numero2)
        {
            return numero1 - numero2;
        }
        public double moltiplicazione(double numero1, double numero2)
        {
            return numero1 * numero2;
        }
        public double divisione(double numero1, double numero2)
        {
            if (numero2 == 0)
            
                throw new DivideByZeroException("Non puoi dividere per zero!");
            
            return numero1 / numero2; 



        }
    }
}


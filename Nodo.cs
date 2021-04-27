using System;
using System.Collections.Generic;
using System.Text;

namespace Bellman_Ford
{
    class Nodo
    {
        private int costo, precedente;
        public Nodo()
        {
            costo = int.MaxValue;
            precedente = -1;
        }
        //Funzioni che consentono la modifica dei valori
        public int Costo { get { return costo; } set { costo = value; } }
        public int Precedente { get { return precedente; } set { precedente = value; } }
    }
}

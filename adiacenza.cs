using System;
using System.Collections.Generic;
using System.Text;

namespace Bellman_Ford
{
    class adiacenza
    {
        private int costo, posizione, provenienza;
        public adiacenza(int c, int p)
        {
            costo = c;
            posizione = p;
        }
        public int Costo { get { return costo; } set { costo = value; } }
        public int Posizione { get { return posizione; } set { posizione = value; } }
        public int Provenienza { get { return provenienza; } set { provenienza = value; } }
    }

    class tabella
    {
        public List<tabella> colonna = new List<tabella>();
        public adiacenza valori = new adiacenza(-1, -1);
        public tabella()
        {

        }
    }
}

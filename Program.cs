using System;
using System.Collections.Generic;

namespace Bellman_Ford
{
    class Program
    {
        //matrice delle adiacenze
        public static List<List<int>> matrice = new List<List<int>>();
        //lista di nodi che mi servirà per sostituire la tabellina dei costi
        static List<List<Nodo>> routers = new List<List<Nodo>>();
        //vettore di interi che mi servirà per risalire il percorso da percorrere per arrivare da un router all'altro
        static List<int> percorso_router = new List<int>();
        static void Main(string[] args)
        {
            int nNodi, nIniziale, nFinale;
            //inserimento numero di nodi
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Desidera inserire la matrice a mano o da codice? (0 Mano - 1 Codice)");
            int scelta = Convert.ToInt32(Console.ReadLine());
            //inizializzo la matrice a con tutti -1 e la diagonale di 0
            InizializzazioneMatrice(nNodi);
            if (scelta == 0)
            {
                InserimentoArchi(nNodi);
            }
            else if (scelta == 1)
            {
                matrice[0][1] = 14; matrice[1][0] = 14;
                matrice[0][2] = 11; matrice[2][0] = 11;
                matrice[0][3] = 16; matrice[3][0] = 16;
                matrice[1][2] = 16; matrice[2][1] = 16;
                matrice[1][4] = 11; matrice[4][1] = 11;
                matrice[2][4] = 5; matrice[4][2] = 5;
                matrice[2][6] = 6; matrice[6][2] = 6;
                matrice[2][3] = 6; matrice[3][2] = 6;
                matrice[3][5] = 16; matrice[5][3] = 16;
                matrice[6][4] = 8; matrice[4][6] = 8;
                matrice[6][5] = 12; matrice[5][6] = 12;
            }
            else
            {
                Console.Clear();
                Main(args);
            }
            inizializza_routers(nNodi);
            while (!algoritmo(nNodi)) ;
            Console.Write("\nInserire il nodo iniziale: ");
            nIniziale = Convert.ToInt32(Console.ReadLine());
            Console.Write("Inserire il nodo finale: ");
            nFinale = Convert.ToInt32(Console.ReadLine());
            //vado avanti fino a quando la funzione algoritmo non restituirà un valroe true e quindi le modifiche nella lista di tipo nodo saranno finite
            Console.WriteLine("Per arrivare al router " + Convert.ToString(nFinale) + " dal router " + Convert.ToString(nIniziale) + " bisogna seguire il percorso\n");
            Salva_Percorso(nIniziale,nFinale);
            Stampa_Percorso(nFinale);
            Console.WriteLine("\ncosto : " + Convert.ToString(routers[nIniziale][nFinale].Costo) + "\n");
        }
        //metodo che inizializza la matrice delle adiacenze con la diagonale degli 0 e le restanti celle a valore -1
        private static void InizializzazioneMatrice(int nNodi)
        {
            //Ciclo che inizializza le liste con nNodi elementi al loro interno e crea la diagonale di 0
            for (int i = 0; i < nNodi; i++)
            {
                matrice.Add(new List<int>());
                for (int k = 0; k < nNodi; k++)
                {
                    if (i == k) matrice[i].Add(0);
                    else matrice[i].Add(-1);
                }
            }
        }
        //metodo che permette all'utente di inserire gli archi a mano
        private static void InserimentoArchi(int nNodi)
        {
            //Funzione che inserisce i costi dei collegamenti tra i nodi all'interno della matrice
            int indice = 1;
            for (int y = 0; y < nNodi; y++)
            {
                Console.WriteLine("Se non vi e' collegamento lasciare vuoto (premere invio)");
                int tempX = 0;
                for (int x = 0; x < nNodi; x++)
                {
                    if (tempX < indice) tempX++;
                    else
                    {
                        Console.WriteLine("Arco " + y.ToString() + " -> " + x.ToString());
                        string inserito = Console.ReadLine();
                        if (!string.IsNullOrEmpty(inserito))
                        {
                            int val = Convert.ToInt32(inserito);
                            matrice[y][x] = val;
                            matrice[x][y] = val;
                        }
                    }
                }
                Console.Clear();
                indice++;
            }
        }
        //metodo che genera la lista di tabelle che serviranno poi per l'esecuzione dell'algoritmo
       private static bool algoritmo(int nNodi)
       {
            bool controllo = true;
            int nuovo_costo;
            List<Nodo> appoggio = new List<Nodo>();
            //ripeto le operazioni di controllo del percorso per tutti i router della rete
            for (int c=0;c<nNodi;c++)
            {
                //il secondo ciclo mi serve per ripetere le operazioni di controllo contemporaneamente su ogni router
                for(int j = 0;j<nNodi;j++)
                {
                    //entro solo se so come raggiungere il router, quindi il costo è diverso da max int
                    if (routers[j][c].Costo != int.MaxValue)
                    {
                        //controllo tutta la riga della matrice delle adiacenze di ogni router e nel caso aggiorno i costi (solo se sono migliorativi)
                        for (int d = 0; d < nNodi; d++)
                        {
                            //entro solo se trovo un router a cui sono collegato direttamente
                            if (matrice[c][d] > 0)
                            {
                                //mi salvo il nuovo costo ipotetico in una variabile per comodità
                                nuovo_costo = matrice[c][d] + routers[j][c].Costo; //il nuovo costo ipotetico lo ottengo
                                                                                //sommando il costo per arrivare al nodo c + il costo preso dalla matrice delle adiacenze per arrivare al router in posizione d
                                                                                //se il costo è migliorativo aggiorno il costo, la provenienza e setto controllo a falso in quanto ho fatto una modifica
                                if (routers[j][d].Costo > nuovo_costo)
                                {
                                    routers[j][d].Precedente = c;
                                    routers[j][d].Costo = nuovo_costo;
                                    controllo = false;
                                }
                            }
                        }
                    }
                }
                stampa_matrice(nNodi);
                Console.WriteLine("\n\n\n");
            }
            return controllo;
       }
        //metodo per risalire al percorso a ritroso
        private static void Salva_Percorso(int nIniziale,int nFinale)
        {
            int precedente = nFinale;
            while (precedente != -1)
            {
                percorso_router.Add(precedente);
                precedente = routers[nIniziale][precedente].Precedente;
            }
        }
        //metodo per stampare il percorso non a ritroso
        private static void Stampa_Percorso(int nFinale)
        {
            int dim = percorso_router.Count - 1;
            for (int i = dim; i > 0; i--)
            {
                Console.WriteLine(percorso_router[i]);
            }
        }
        //metodo che inizializza la matrice di nodi mettendo tutti i costi a -1 eccetto la diagonale di 0
        private static void inizializza_routers(int nNodi)
        {
            for(int c=0;c<nNodi;c++)
            {
                routers.Add(new List<Nodo>());
                for(int d = 0;d<nNodi;d++)
                {
                    routers[c].Add(new Nodo());
                    if (c == d)
                        routers[c][d].Costo = 0;
                }
            }
        }
        private static void stampa_matrice(int nNodi)
        {
            for(int c=0;c<nNodi;c++)
            {
                for(int d=0;d<nNodi;d++)
                {
                    if (routers[c][d].Costo == int.MaxValue)
                        Console.Write("-1  ");
                    else
                        Console.Write(Convert.ToString(routers[c][d].Costo) + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}

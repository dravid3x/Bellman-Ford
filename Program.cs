using System;
using System.Collections.Generic;

namespace Bellman_Ford
{
    class Program
    {
        //matrice delle adiacenze
        public static List<List<int>> matrice = new List<List<int>>();
        //matrice di router (potrebbe non essere necessario)
        static List<List<Nodo>> routers = new List<List<Nodo>>();
        //lista che contiene delle liste di liste, che sarebbero tutte le varie tabelle delle provenienze
        static List<List<List<adiacenza>>> tabelle = new List<List<List<adiacenza>>>();
        //vettore di interi che mi servirà per risalire il percorso da percorrere per arrivare da un router all'altro
        static List<int> percorso_router = new List<int>();
        static void Main(string[] args)
        {
            int nNodi, nIniziale, nFinale;
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Desidera inserire la matrice a mano o da codice? (0 Mano - 1 Codice)");
            int scelta = Convert.ToInt32(Console.ReadLine());
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
            inizializzaTabelle(nNodi);
            /*for(int c= 0;c<nNodi;c++)
            {
                for (int i = 0; i < nNodi; i++)
                {
                    routers[c].Add(new Nodo());
                }//Inizializzazione della lista di nodi
            }*/

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
        private static void inizializzaTabelle(int nNodi)
        {
            List<tabella> test = new List<tabella>();
            test.Add(new tabella());

            for (int i = 0; i < nNodi; i++)
            {
                for(int x = 0; x < 3; x++)
                {
                    test[0].colonna.Add(new tabella());
                    for (int c = 0; c < 5; c++)
                    {
                        test[0].colonna[x].colonna.Add(new tabella());
                    }
                }
            }
            

            Console.WriteLine(test[0]);






            //creo tante tabelle quanti sono i router 
            for (int c = 0; c < nNodi; c++)
            {
                //inizializzo la prima riga della tabella con una cella vuota e poi i router adiacenti e il costo per arrivarci 
                tabelle[c][0].Add(new adiacenza(0, 0));
                for (int d = 0; d < nNodi; d++)
                {
                    //entro se c'è un router collegato direttamente
                    if (matrice[c][d] > 0)
                    {
                        //aggiugno una cella a cui passo il costo, contenuto nella matrice e il numero del router contenuto in d
                        tabelle[c][0].Add(new adiacenza(matrice[c][d], d));
                    }
                }
                int n = 0;
                //ora inizializzo le righe partendo da 1 e inserisco tutti i router, eccetto quello di partenza, tutti con costo 0 visto che non mi serve
                for (int i = 1; i < nNodi; i++)
                {
                    //se il router in considerazione non è quello di partenza lo aggiugno alla posizione 0 della riga
                    if (n == c)
                        n++;
                    tabelle[c][i].Add(new adiacenza(0, n));
                    //in caso contrario lo salto e vado al router successivo

                }
            }
        }


    }
}

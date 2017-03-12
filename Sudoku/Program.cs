using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudoku
{
    class Program
    {
        static int[,] ChargerGrille(int Nb)
        // Charge la grille précalculée n° Nb (où Nb est un nombre entre 1 et 20) et renvoie cette grille sous la forme d'un tableau de 9*9 entiers
        {
            string FileName = @"..\..\..\Grilles\Grille";
            if (Nb < 10) FileName += "0";
            FileName += Nb;
            int[,] Tab = new int[9, 9];

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    string line = sr.ReadLine();
                    string[] TabString = line.Split(' ');

                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            Tab[i, j] = int.Parse(TabString[i * 9 + j]);
                    return Tab;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }


        static int Saisie()
        {
            int nbre;Boolean verif = true;
            do
            {
                Console.WriteLine("Veuillez saisir un nombre entre 0 et 8");
                verif = Int32.TryParse(Console.ReadLine(), out nbre);
                if (nbre < 0 || nbre > 8)
                {
                    Console.WriteLine("Vous ne respectez pas les consignes. Appuyez sur une touche pour recommencer");
                    Console.ReadKey();
                }
                
            } while (nbre < 0 || nbre > 8 || !verif);

            return nbre;
            
        }

        static Boolean ChiffreValide(int a)
        {
            Boolean verifnbre = false;
            if (a >=1 && a <= 9)
            {
                Console.WriteLine("Votre entier est bien compris entre 1 et 9");
                verifnbre = true;
            }
            else
            {
                Console.WriteLine("Votre entier n'est pas compris entre 1 et 9");
            }
            return verifnbre;
        }

        static void Afficher(int [,] tab)
        {
        
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static string TraduireCoordonnees(int a, out string pos)
        {
            pos = "";
            if (a == 0){
                pos = "0,0";
            }
            else if (a == 1){
                pos = "0,3";
            }
            else if (a == 2){
                pos = "0,6";
            }
            else if(a == 3){
                pos = "3,0";
            }
            else if (a == 4){
                pos = "3,3";
            }
            else if (a == 5){
                pos = "3,6";
            }
            else if (a == 6){
                pos = "6,0";
            }
            else if (a == 7){
                pos = "6,3";
            }
            else if (a == 8){
                pos = "6,6";
            }
            return pos;
        }

        static List <int> LigneVersListe(int[,] tab, int b)
        {
            List <int> lignes = new List<int>();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (b == i)
                    {
                        lignes.Add(tab[i, j]);
                    }
                }
            }
            return lignes;
        }

        static List<int> ColonneVersListe(int[,] tab, int b)
        {
            List<int> colonne = new List<int>();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (b == j)
                    {
                        colonne.Add(tab[i, j]);
                    }
                }
            }
            return colonne;
        }

        static List<int> BlocVersListe(int [,] tab, int a){
            string b;
            string coo = TraduireCoordonnees(a, out b);
            List <int> bloc = new List<int>();
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab.Length; j++)
                {
                    if ((coo == "0,0") && (i <= 2 && j <= 2))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "0,3") && (i <= 2) && (j > 2 && j <= 5)){
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "0,6") && (i <= 2) && (j > 5 && j <= 8)){
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "3,0") && (i > 2 && i <= 5) && (j <= 2))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "3,3") && (i > 2 && i <= 5) && (j > 2 && j <= 5))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "3,6") && (i > 2 && i <= 5) && (j > 5 && j <= 8))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "6,0") && (i > 5 && i <= 8) && (j <= 2))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "6,3") && (i > 5 && i <= 8) && (j > 2 && j <= 5))
                    {
                        bloc.Add(tab[i, j]);
                    }
                    if ((coo == "6,6") && (i > 5 && i <= 8) && (j > 5 && j <= 8))
                    {
                        bloc.Add(tab[i, j]);
                    }
                }
            }
            return bloc;
        }

        static Boolean ListeCorrecte( List<int> entier)
        {
            Boolean verif = true;
            entier.Sort();
            int nbre = 1;
            foreach (int i in entier) { 
                 if (i != nbre)
                 {       
                    verif = false;
                 }
                 nbre++;
            }
            return verif;
        }

        static Boolean VerifierGrille(int[,] tab){
            List<int> bloc0 = new List<int>();
            List<int> bloc1 = new List<int>();
            List<int> bloc2 = new List<int>();
            List<int> bloc3 = new List<int>();
            List<int> bloc4 = new List<int>();
            List<int> bloc5 = new List<int>();
            List<int> bloc6 = new List<int>();
            List<int> bloc7 = new List<int>();
            List<int> bloc8 = new List<int>();
            Boolean verif = false;
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab.Length; j++)
                {
                    if (i <= 2 && j <= 2)
                    {
                        bloc0.Add(tab[i, j]);
                    }
                    if ((i <= 2) && (j > 2 && j <= 5))
                    {
                        bloc1.Add(tab[i, j]);
                    }
                    if ((i <= 2) && (j > 5 && j <= 8))
                    {
                        bloc2.Add(tab[i, j]);
                    }
                    if ((i > 2 && i <= 5) && (j <= 2))
                    {
                        bloc3.Add(tab[i, j]);
                    }
                    if ((i > 2 && i <= 5) && (j > 2 && j <= 5))
                    {
                        bloc4.Add(tab[i, j]);
                    }
                    if ((i > 2 && i <= 5) && (j > 5 && j <= 8))
                    {
                        bloc5.Add(tab[i, j]);
                    }
                    if ((i > 5 && i <= 8) && (j <= 2))
                    {
                        bloc6.Add(tab[i, j]);
                    }
                    if ((i > 5 && i <= 8) && (j > 2 && j <= 5))
                    {
                        bloc7.Add(tab[i, j]);
                    }
                    if ((i > 5 && i <= 8) && (j > 5 && j <= 8))
                    {
                        bloc8.Add(tab[i, j]);
                    }
                }
            }
            if (ListeCorrecte(bloc0) && ListeCorrecte(bloc1) && ListeCorrecte(bloc2) && ListeCorrecte(bloc3) && ListeCorrecte(bloc4) && ListeCorrecte(bloc5) && ListeCorrecte(bloc6) && ListeCorrecte(bloc7) && ListeCorrecte(bloc8))
            {
                verif = true; Console.WriteLine("Votre grille est bien conforme");
            }
            else { verif = false; Console.WriteLine("Votre grille n'est pas conforme"); }
            return verif;
        }

        static void Main(string[] args)
        {
            // Question 5
            // On vérifie que le chiffre est bien valide
            int nbre;
            Boolean verif = true;
            do
            {
                Console.WriteLine("Veuillez saisir un nombre entre 1 et 9");
                verif = Int32.TryParse(Console.ReadLine(), out nbre);
            } while (!verif);
            ChiffreValide(nbre);
            // Question 6
            // On initialise notre grille de sudoku
            int[,] Grille = ChargerGrille(9);
            // On l'affiche
            Afficher(Grille);
            // Question 7
            Random nrg = new Random();
            int bloc = nrg.Next(1, 9);
            Console.WriteLine("Numéro de Bloc : " + bloc);
            string b;
            string coo = TraduireCoordonnees(bloc, out b);
            Console.WriteLine("Le Bloc est le numéro : " + coo);
            // Question 8
            Console.WriteLine("***** Affichage du contenu de la ligne saisie ***** ");
            List<int> ligne = new List<int>();
            ligne = LigneVersListe(Grille, Saisie());
            Console.WriteLine("Le contenu de la ligne saisie est le suivant :");
            for (int i = 0; i < ligne.Count; i++)
            {
                Console.Write(ligne[i] + "\t");
            }
            Console.WriteLine();
            // Question 9
            Console.WriteLine("***** Affichage du contenu de la colonne saisie ***** ");
            List<int> colonne = new List<int>();
            colonne = ColonneVersListe(Grille, Saisie());
            Console.WriteLine("Le contenu de la colonne saisie est le suivant :");
            for(int i = 0; i < colonne.Count; i++)
            {
                Console.Write(colonne[i] + "\t");
            }
            Console.WriteLine();
            // Question 10
            Console.WriteLine("***** Affichage du contenu du bloc généré aléatoirement ***** ");
            List<int> contenubloc = new List<int>();
            contenubloc = BlocVersListe(Grille, bloc);
            Console.WriteLine("Le contenu du bloc est le suivant :");
            for (int i = 0; i < contenubloc.Count; i++)
            {
                Console.Write(contenubloc[i] + "\t");
            }
            Console.WriteLine();
            // Question 11
            Console.WriteLine("**** Vérification de la conformité d'une liste établie ****");
            List<int> listeentiers = new List<int> { 9, 5, 6, 3, 4, 2, 1, 8, 7};
            Console.Write("Liste de départ :");
            for (int i = 0; i < listeentiers.Count; i++)
            {
                Console.Write(listeentiers[i] + "\t");
            }
            Console.WriteLine();        
            Boolean verifliste = ListeCorrecte(listeentiers);
            if (verifliste == false) { Console.WriteLine("Liste non conforme"); }
            else { Console.WriteLine("Votre liste est conforme au règlement et contient les entiers de 1 à 9 une et une seule fois"); }
            // Question 12
            VerifierGrille(Grille);
            // Question 13
            int[,] Grille2 = ChargerGrille(20);
            VerifierGrille(Grille2); 
            Console.ReadKey();
        }
    }
}

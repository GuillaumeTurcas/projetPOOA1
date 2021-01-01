using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Jeu_de_la_vie
{
    class Program
    {
        static void Main(string[] args)
        {
            int L = 0;
            while (L < 3)
            {
                Console.WriteLine("===================="); Console.WriteLine("|Nombre de Lignes ?|"); Console.WriteLine("====================");
                L = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            int C = 0;
            {
                Console.WriteLine("======================"); Console.WriteLine("|Nombre de Colonnes ?|"); Console.WriteLine("======================");
                C = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }



            //Taux de remplissage
            double T = 0;
            while (T > 0.9 || T < 0.1)
            {
                Console.WriteLine("========================================================================");
                Console.WriteLine("|Donnez le taux de remplissage des cellules vivantes (entre 0,1 et 0,9)|");
                Console.WriteLine("========================================================================");
                T = Convert.ToDouble(Console.ReadLine());
                Console.Clear();
            }

            //Avec ou sans visualisation
            int Visualisation = -1;
            while (Visualisation != 1 && Visualisation != 2 && Visualisation != 3 && Visualisation != 4)
            {
                Console.WriteLine("  ............................................................................................");
                Console.WriteLine(" ----------------------------------------------------------------------------------------------");
                Console.WriteLine("|                                     Voulez-vous :                                            |");
                Console.WriteLine("|   1- Le jeu de la Vie Classique sans la visualisation intermédiaire des états futurs         |");
                Console.WriteLine("|   2- Le jeu de la Vie Classique avec la visualisation intermédiaire des états futurs         |");
                Console.WriteLine("|   3- Le jeu de la Vie Variante (deux populations) sans la visualisation intermédiaire [NEW]  |");
                Console.WriteLine("|   4- Le jeu de la Vie Variante (deux populations) avec la visualisation intermédiaire [NEW]  |");
                Console.WriteLine(" ----------------------------------------------------------------------------------------------");
                Console.WriteLine("  ............................................................................................");
                Visualisation = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            int[,] Grille = new int[L, C]; //Création de la grille de jeu

            Initialisation(Grille, T, Visualisation); //Initialisation de départ de la grille de jeu

            AfficherGrille(Grille); //Afficher une première fois la grille
            // L'affiche de la grille va se faire la règle suivante : || 0 = cellule morte . || 1 = cellule en vie # || 2 = cellule qui nait - ||3 = cellule qui meurt * ||

            LeJeuxDeLaVie(Grille, Visualisation);

            Console.ReadKey();
        }

        //Afficher la grille.........................................................
        static void AfficherGrille(int[,] Grille)
        {
            int L = Grille.GetLength(0);
            int C = Grille.GetLength(1);
            if (Grille == null)
            {
                Console.WriteLine("Erreur");
            }
            else
            {
                for (int i = 0; i < L; i++)
                {
                    //AfficherLigne(C); //Fonction enlevé pour des raisons pratiques (pour avoir des grandes matrices affichable), mais on peut la remettre facilement
                    int j = 0;
                    Console.WriteLine("");
                    while (j != C)
                    {

                        //Console.Write("| "); //Ligne de code à usage esthétique, peut être remis facilement

                        if (Grille[i, j] == 0) { Console.Write(". "); } //Cellule Morte
                        if (Grille[i, j] == 1) { Console.Write("# "); } //Cellule Vivante
                        if (Grille[i, j] == 2) { Console.Write("* "); } //Cellule qui va naitre
                        if (Grille[i, j] == 3) { Console.Write("- "); } //Cellule qui va mourir
                        if (Grille[i, j] == 11) { Console.Write("¤ "); } //Cellule Vivante Population 2

                        j++;
                        //Console.Write("|");
                    }
                }
                //AfficherLigne(C); //Pour réafficher les lignes, ca a été enlevé pour des raisons d'avoir des grilles plus grandes
            }
            Console.ReadKey();
            Console.Clear();
        }

        //Complémentaire à l'affichage du tableau (l'idée a été abandonné pour faire rentrer des grilles plus grandes sur l'écran, mais on conserve pour le remettre facilement)
        static void AfficherLigne(int C)
        {
            Console.WriteLine("");
            for (int i = 0; i < C; i++)
            {
                Console.Write("-----");
            }
            //L'utilité de cette fonction est uniquement esthétique
        }

        //.....................................................................................

        static int Taille(int[,] Grille, int Popul) //Compter le nombre de cellule vivante
        {
            int taille = 0;
            int L = Grille.GetLength(0);
            int C = Grille.GetLength(1);
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    if (Popul == 1 && Grille[i, j] == 1) //Pour compter la population 1
                    {
                        taille++;
                    }

                    if (Popul == 2 && Grille[i, j] == 11) //Pour compter la population 2
                    {
                        taille++;
                    }

                    if (Popul == 0) //Pour compter la population totale
                    {
                        if (Grille[i, j] == 11 || Grille[i, j] == 1)
                        {
                            taille++;
                        }
                    }
                }
            }
            return taille;
        }

        //Initialiser la grille Aléatoirement

        static void Initialisation(int[,] Grille, double T, int Visualisation)
        {
            int Repart = 0; //Permet de répartir équitablement au début les deux populations
            int L = Grille.GetLength(0);
            int C = Grille.GetLength(1);
            Random Generateur = new Random();
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    double Alea = Generateur.Next(0, 10);
                    if (Alea < T * 10)
                    {
                        if (Visualisation == 4 || Visualisation == 3) //Cas si l'utilisateur veut deux pupulations sur la grille
                        {
                            int Repart1 = Repart; //permet de créer une variable provisoire afin que Repart n'aille pas dans les deux boucles si Repart = 0
                            if (Repart == 0)
                            {
                                Grille[i, j] = 11; //créé une cellule de la génération 1
                                Repart1++;
                            }
                            else
                            {
                                Grille[i, j] = 1; //créé une cellule de la génération 2
                                Repart1--;
                            }
                            Repart = Repart1;
                        }
                        //Ainsi, une fois sur deux, la grille créera une cellule vivante de la population 1, et une fois sur deux, il créera une cellule vivante de la population 2
                        if (Visualisation == 1 || Visualisation == 2)
                        {
                            Grille[i, j] = 1; //Cas si l'utilisateur ne veut qu'une seule population sur la grille
                        }
                    }
                }
            }

        }

        //Jeu de la vie

        static void LeJeuxDeLaVie(int[,] Grille, int Visualisation)
        {
            Random Generateur = new Random();
            int G = 0; //Initialise la génération 0
            int L = Grille.GetLength(0);
            int C = Grille.GetLength(1);
            bool FinDuJeu = false;
            int[,] GrilleProv = new int[L, C]; //Création d'une matrice provisoire qui sert à être analyser, sans être modifier en même temps
            int[,] GrilleProv2 = new int[L, C]; //Création d'une matrice provisoire qui conserve la visualisation provisoire
            while (FinDuJeu == false)
            {
                G++;
                int taille = Taille(Grille, 0);
                if (taille == 0 || GrilleProv == Grille) //Fin du jeu si c'est stabilisé
                {
                    FinDuJeu = true;
                }
                else
                {
                    GrilleProv2 = CopieMatrice(Grille, GrilleProv2); //affichage intermédiaire
                    GrilleProv = CopieMatrice(Grille, GrilleProv);
                    for (int i = 0; i < L; i++)
                    {
                        for (int j = 0; j < C; j++)
                        {
                            int compteur11 = CompterAutourCellule(GrilleProv, i, j, C, L, 1, 1); //Compte le nombre de cellule vivante autour de la cellule au rang 1 de la population 1
                            int compteur21 = CompterAutourCellule(GrilleProv, i, j, C, L, 2, 1); //Compte le nombre de cellule vivante autour de la cellule au rang 2 de la population 1
                            int compteur12 = CompterAutourCellule(GrilleProv, i, j, C, L, 1, 2); //Compte le nombre de cellule vivante autour de la cellule au rang 1 de la population 2
                            int compteur22 = CompterAutourCellule(GrilleProv, i, j, C, L, 2, 2); //Compte le nombre de cellule vivante autour de la cellule au rang 2 de la population 2

                            //==========================================================================
                            if (GrilleProv[i, j] == 0) //si la cellule étudiée est morte
                            {
                                if (compteur11 == 3 || compteur12 == 3)
                                {
                                    GrilleProv2[i, j] = 2; //La cellule va naitre à la génératon suivante
                                    if (Visualisation < 3)
                                    {
                                        Grille[i, j] = 1; //Si elle est entourée de trois cellule vivantes, alors elle nait à la génération suivante
                                    }
                                    else//Cas pour deux populations
                                    {
                                        if (compteur11 == compteur12)
                                        {
                                            if (compteur21 > compteur22)
                                            {
                                                Grille[i, j] = 1;
                                            }
                                            if (compteur22 > compteur21)
                                            {
                                                Grille[i, j] = 11;
                                            }
                                            if (compteur22 == compteur21)
                                            {
                                                Grille[i, j] = 1 + (Generateur.Next(0, 1) * 10); //Bonus
                                            }
                                        }
                                        else
                                        {
                                            if (compteur12 == 3 || compteur12 == 2)
                                            {
                                                Grille[i, j] = 11;
                                            }
                                            else
                                            {
                                                Grille[i, j] = 1;
                                            }
                                        }
                                    }
                                }
                            }
                            if (GrilleProv[i, j] == 1 || GrilleProv[i, j] == 11) //Si la cellule est vivante
                            {
                                if (Visualisation == 1 || Visualisation == 2)
                                {
                                    if (compteur11 < 2)
                                    {
                                        GrilleProv2[i, j] = 3;
                                        Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                    }
                                    if (compteur11 > 3)
                                    {
                                        GrilleProv2[i, j] = 3;
                                        Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                    }
                                }
                                else
                                {
                                    if (compteur11 < 2 && compteur12 < 2)
                                    {
                                        GrilleProv2[i, j] = 3;
                                        Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                    }
                                    else
                                    {
                                        if (compteur11 > 3 || compteur12 > 3)
                                        {
                                            GrilleProv2[i, j] = 3;
                                            Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                        }
                                    }
                                }
                            }
                            if (GrilleProv[i, j] == 11) //Si la cellule est vivante de la population 2
                            {
                                if (compteur11 < 2)
                                {
                                    GrilleProv2[i, j] = 3;
                                    Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                }
                                if (compteur11 > 3)
                                {
                                    GrilleProv2[i, j] = 3;
                                    Grille[i, j] = 0; //Si elle est en sous population ou en surpopulation, alors elle meurt à la génération suivante
                                }
                            }
                        }
                        FinDuJeu = false;
                    }
                    Situation(Grille, Visualisation, GrilleProv2, G);
                    AfficherGrille(Grille);
                }
            }
            Console.Write("Fin du Jeu ..... Youpi");
        }

        static void Situation(int[,] Grille, int Visualisation, int[,] GrilleProv2, int G)
        {
            int T1 = Taille(Grille, 1);
            int T2 = Taille(Grille, 2);

            if (Visualisation == 2 || Visualisation == 4)
            {
                Console.Write("Nous sommes à la génération numéro " + G); //====NEW====
                if (Visualisation == 4) //Dans le cas des deux populations
                {
                    Console.Write(" et la taille de la population 1 est de " + T1 + " et celle de la population 2 est de " + T2 + " (Etape intermédiaire)");
                }

                else //Dans le cas où il n'y a qu'une seule population
                {
                    Console.Write(" et la taille de la populaiton est de " + T1 + " (Etape inetermédiaire)");
                }
                AfficherGrille(GrilleProv2);
            }
            if (Visualisation == 4) //Dans le cas des deux populations
            {
                Console.Write("Nous sommes à la génération numéro " + G);
                Console.Write(" et la taille de la population 1 est de " + T1 + " et celle de la population 2 est de " + T2);
            }

            else //Dans le cas où il n'y a qu'une seule population
            {
                Console.Write("Nous sommes à la génération numéro " + G);
                Console.Write(" et la taille de la populaiton est de " + T1);
            }
        }

        static int CompterAutourCellule(int[,] GrilleProv, int i, int j, int C, int L, int Rang, int Popul) //Compte le nombre de cellule vivante autour de la cellule sélectionnée
        {
            int Nombre = 0;
            for (int x = i - Rang; x < i + Rang + 1; x++)
            {
                for (int y = j - Rang; y < j + Rang + 1; y++)
                {
                    //Procédons avec les différents cas possibles
                    int tempX = x; //au cas où l'on change x pour le remettre comme on l'a eu pour ne pas avoir de problème
                    int tempY = y; //au cas où l'on change y pour le remettre comme on l'a eu pour ne pas avoir de problème
                    //Si la cellule étudiée se trouve dans l'extrémité de la matrice, alors :
                    if (x < 0) { x = L + x; }
                    if (x >= L) { x = x - L; }
                    if (y < 0) { y = C + y; }
                    if (y >= C) { y = y - C; }
                    int VOM = GrilleProv[x, y]; //VOM pour vivante ou morte, donc prend la valeure 0 ou 1
                    if (VOM == 1 && Popul == 1 && (x != i || y != j)) { Nombre++; } //Cas Population 1
                    if (VOM == 11 && Popul == 2 && (x != i || y != j)) { Nombre++; } //Cas Population 2
                    x = tempX;
                    y = tempY;
                }
            }
            return Nombre;
        }

        static int[,] CopieMatrice(int[,] Grille, int[,] GrilleProv) //Créer une matrice provisoire identique à la matrice d'origine
        {
            for (int i = 0; i < Grille.GetLength(0); i++)
            {
                for (int j = 0; j < Grille.GetLength(1); j++)
                {
                    GrilleProv[i, j] = Grille[i, j];
                }
            }
            return GrilleProv;
        }
        //Fin Youpi tralala
    }
}

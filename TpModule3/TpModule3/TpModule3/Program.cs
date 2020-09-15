using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TpModule3.BO;

namespace TpModule3
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
        static void Main(string[] args)
        {
            InitialiserDatas();

            var ListeDesAuteursenG = ListeAuteurs.Where(auteur => auteur.Nom.StartsWith(DisplayToUpper("g")));
            Console.WriteLine("Liste de prénoms d'auteur dont le nom commence par G");
            foreach (var auteur in ListeDesAuteursenG)
            {
                Console.WriteLine($"{auteur.Prenom}");
            }


            var AuteurAvecPlusdeLivres = ListeLivres.GroupBy(livre => livre.Auteur).OrderByDescending(livre => livre.Count()).FirstOrDefault().Key;
            Console.WriteLine("L'auteur ayant le plus de livre");
            Console.WriteLine(String.Format("nom: {0} prenom: {1}", AuteurAvecPlusdeLivres.Nom, AuteurAvecPlusdeLivres.Prenom));


            var NbmoyenDePage = ListeLivres.GroupBy(livre => livre.Auteur);
            foreach (var auteur in NbmoyenDePage)
            {
                Console.WriteLine($"auteur: {auteur.Key.Nom}");
                Console.WriteLine(string.Format("nbPage: {0} ", auteur.Average(l => l.NbPages)));
            }

            var LivreAvecPlusDePage = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine("Livre avec le plus grand nb de pages");
            Console.WriteLine(string.Format("titre: {0} auteur: {1} {2} nb pages: {3}", LivreAvecPlusDePage.Titre, LivreAvecPlusDePage.Auteur.Nom, LivreAvecPlusDePage.Auteur.Prenom, LivreAvecPlusDePage.NbPages));


            var MoyennedesFactures = ListeAuteurs.Average(auteur => auteur.Factures.Sum(m => m.Montant));

            Console.WriteLine(string.Format("la moyenne des factures s'élèvent à: {0} euros", MoyennedesFactures));

            var ListeDesAuteurs = ListeLivres.GroupBy(a => a.Auteur);
            foreach (var listeauteur in ListeDesAuteurs)
            {
                Console.WriteLine($"auteur: {listeauteur.Key.Nom}");
                foreach (var livre in listeauteur)
                {
                    Console.WriteLine(string.Format("   Titre: {0} ", livre.Titre));
                };
            }


            var listeOrdrealpha = ListeLivres.OrderBy(t => t.Titre);
            Console.WriteLine("liste des livres par ordre Alpha");
            foreach(var livre in listeOrdrealpha)
            {
                Console.WriteLine(string.Format("Titre: {0} {4}Synopsys: {1}{4}auteur: {2} {3}{4}", livre.Titre, livre.Synopsis, livre.Auteur.Nom, livre.Auteur.Prenom, Environment.NewLine));
            }

            var PageSup = ListeLivres.Average(nb => nb.NbPages);
            Console.WriteLine("");
            Console.WriteLine($"Page moyenne = {PageSup}");
            var ListeLivrepageSup = ListeLivres.Where(nb => nb.NbPages > PageSup);
            foreach (var livre in ListeLivrepageSup)
            {
                Console.WriteLine(string.Format("Titre: {0} {4}Synopsys: {1}{4}auteur: {2} {3}{4}", livre.Titre, livre.Synopsis, livre.Auteur.Nom, livre.Auteur.Prenom, Environment.NewLine));
            }

            var AuteurAvecmoinsdeLivres = ListeAuteurs.OrderBy(auteur => ListeLivres.Count(l => l.Auteur == auteur)).FirstOrDefault();
            Console.WriteLine("L'auteur ayant écrit le moins de livre");
            Console.WriteLine(String.Format("nom: {0} prenom: {1}", AuteurAvecmoinsdeLivres.Nom, AuteurAvecmoinsdeLivres.Prenom));

            Console.ReadKey();
        }

        private static string DisplayToUpper(string v1)
        {
            return v1.ToUpper();
        }
    }
}

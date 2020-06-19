using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace ProjetCheminCritique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _step;
        private List<Tache> _listeTaches;
        private bool _DataView;

        public MainWindow()
        {
            InitializeComponent();
        }

        public int Step { get => _step; }
        internal List<Tache> ListeTaches { get => _listeTaches; }

        private void Init(object sender, EventArgs e)
        {
            this.ChangeStep(1);
//            this.SecondaryButton.Visibility = Visibility.Hidden;
//#if DEBUG
//            this.SecondaryButton.Visibility = Visibility.Visible;
//#endif

        }

        public void ChangeStep(int step)
        {
            switch (step)
            {
                case 1:

                    this._step = 1;
                    this.DisplayArea.Text = "Le .csv inporté doit correspondre à cette syntaxe :\r\rA;Contenu de la tâche A;5;\rB;Contenu de la tâche B;1;A\rC;Contenu de la tâche C;3;B\r...";
                    this.DisplayArea.IsEnabled = false;
                    this.MainButton.Content = "Charger un fichier";
                    this.DisplayGrid.Visibility = Visibility.Hidden;
                    this.SecondaryButton.IsEnabled = false;
                    this.SecondaryButton.Content = "Vue \"DATA\"";
                    this.TerciButton.IsEnabled = false;
                    this.TerciButton.Content = "Chemin critique";

                    break;
                case 2:

                    this._step = 2;

                    this.MainButton.Content = "Charger un fichier";

                    this._DataView = false;
                    List<DisplayTache> DataSource = new List<DisplayTache>();
                    DataSource.Add(new DisplayTache(Tache.ToStringReference()));
                    foreach (var tache in ListeTaches)
                    {
                        string str = tache.ToString();
                        DataSource.Add(new DisplayTache(tache));
                    }
                    this.DisplayGrid.ItemsSource = DataSource;
                    //this.DisplayGrid.ItemsSource = this.ListeTaches;
                    this.DisplayGrid.Visibility = Visibility.Visible;

                    this.SecondaryButton.IsEnabled = true;
                    this.SecondaryButton.Content = "Vue \"DATA\"";

                    string display = string.Empty;
                    foreach (var tache in this.ListeTaches)
                    {
                        display += tache.ToString() + "\r";
                    }
                    this.DisplayArea.Text = display;

                    this.DisplayArea.IsEnabled = true;

                    this.TerciButton.IsEnabled = true;
                    this.TerciButton.Content = "Chemin critique";

                    break;
                default:
                    break;
            }
        }

        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    //MessageBox.Show($"{ofd.FileName}", "C'est ok", MessageBoxButton.OK);
                    this._listeTaches = this.GetTaches(File.ReadAllText(ofd.FileName, Encoding.UTF8));
                    this.ChangeStep(2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est apparue lors de l'ouverture du fichiers : {ex.Message}", "Erreur", MessageBoxButton.OK);
                }
            }
        }

        private void SecondaryButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._DataView)
            {
                List<DisplayTache> DataSource = new List<DisplayTache>();
                DataSource.Add(new DisplayTache(Tache.ToStringReference()));
                foreach (var tache in ListeTaches)
                {
                    string str = tache.ToString();
                    DataSource.Add(new DisplayTache(tache));
                }
                this.DisplayGrid.ItemsSource = DataSource;
                this.SecondaryButton.Content = "Vue \"DATA\"";
                this._DataView = false;
            }
            else
            {
                List<FullDisplayTache> DataSource = new List<FullDisplayTache>();
                foreach (var tache in ListeTaches)
                {
                    string str = tache.ToString();
                    DataSource.Add(new FullDisplayTache(tache));
                }
                this.DisplayGrid.ItemsSource = DataSource;
                this.SecondaryButton.Content = "Vue organisée";
                this._DataView = true;
            }
        }

        private void TerciButtonClick(object sender, RoutedEventArgs e)
        {
            string strResult = string.Empty;

            uint LvlMax = 0;
            foreach (var tache in this.ListeTaches)
            {
                if (tache.Niveau > LvlMax)
                {
                    LvlMax = tache.Niveau;
                }
            }

            for (int niveau = 0; niveau <= LvlMax; niveau++)
            {
                List<string> Resultats = new List<string>();

                foreach (var tache in this.ListeTaches)
                {
                    if (tache.Niveau == niveau && tache.DureeTotale == tache.DureeTotaleLarge)
                    {
                        Resultats.Add(tache.Lettre);
                    }
                }

                if (Resultats.Count == 1)
                {
                    strResult += Resultats[0] + ", ";
                }
                else if (Resultats.Count > 1)
                {
                    string subStr = string.Empty;
                    foreach (var lettre in Resultats)
                    {
                        subStr += lettre + "/";
                    }
                    subStr = subStr.Remove(subStr.Length - 1);
                    strResult += subStr + ", ";
                }
                else
                {
                    MessageBox.Show($"Aucun résultat trouvé pour le niveau {niveau}", "Erreur", MessageBoxButton.OK);
                    return;
                }

            }
            strResult = strResult.Remove(strResult.Length - 1);
            strResult = strResult.Remove(strResult.Length - 1);

            MessageBox.Show(strResult, "Chemin critique", MessageBoxButton.OK);
        }

        private List<Tache> GetTaches(string TachesInCSV)
        {
            List<Tache> Taches = new List<Tache>();

            try
            {
                //foreach (string line in TachesInCSV.Split("\r"))
                for (int nbLigne = 0; nbLigne < TachesInCSV.Split("\r").Length; nbLigne++)
                {
                    var line = TachesInCSV.Split("\r")[nbLigne];

                    //Console.WriteLine(line);
                    if (line != "\n")
                    {
                        var Split = line.Split(";").ToList();
                        Split.Add(string.Empty);

                        string Lettre;
                        string Desc;
                        uint Duree;
                        List<string> ListPre;

                        try
                        {
                            Lettre = Split[0].Trim();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Document parse error line {nbLigne} on the argument \"Lettre\" : {ex.Message}");
                        }

                        try
                        {
                            Desc = Split[1].Trim();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Document parse error line {nbLigne} on the argument \"Description\" : {ex.Message}");
                        }

                        try
                        {
                            Duree = Convert.ToUInt32(Split[2]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Document parse error line {nbLigne} on the argument \"Duree\" : {ex.Message}");
                        }

                        try
                        {
                            ListPre = Split[3].Split(",").ToList();
                            for (int i = 0; i < ListPre.Count; i++)
                            {
                                ListPre[i] = ListPre[i].Trim();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Document parse error line {nbLigne} on the argument \"ListePredecesseurs\" : {ex.Message}");
                        }

                        Taches.Add(new Tache(Lettre, Desc, Duree, ListPre));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            /* -------------------------------------------------- */

            bool rester = true;
            do
            {
                bool AllLevelsValidated = true;
                uint MaxLevel = 0;
                List<Tache> LeveledTaches = new List<Tache>();
                foreach (var tache in Taches)
                {
                    if (tache.Niveau == 0)
                    {
                        AllLevelsValidated = false;
                    }
                    else
                    {
                        LeveledTaches.Add(tache);
                        if (MaxLevel < tache.Niveau)
                        {
                            MaxLevel = tache.Niveau;
                        }
                    }
                }
                if (!AllLevelsValidated)
                {
                    if (MaxLevel == 0)
                    {
                        foreach (var tache in Taches)
                        {
                            if (tache.ListPredecesseurs[0] == string.Empty)
                            {
                                tache.Niveau = 1;
                            }
                        }
                    }
                    else
                    {
                        foreach (var tache in Taches)
                        {
                            uint match = 0;
                            foreach (var LeveledTache in LeveledTaches)
                            {
                                foreach (var pre in tache.ListPredecesseurs)
                                {
                                    if (pre == LeveledTache.Lettre)
                                    {
                                        match++;
                                    }
                                }
                            }
                            if (match == tache.ListPredecesseurs.Count && tache.Niveau == 0)
                            {
                                tache.Niveau = MaxLevel + 1;
                            }
                        }
                    }
                }
                else
                {
                    rester = false;
                }
            }
            while (rester);

            foreach (var tache in Taches)
            {
                tache.Niveau--;
            }

            /* -------------------------------------------------- */

            uint LvlMax = 0;

            foreach (var tache in Taches)
            {
                if (tache.Niveau > LvlMax)
                {
                    LvlMax = tache.Niveau;
                }
            }

            /* -------------------------------------------------- */

            foreach (var tache in Taches)
            {
                if (tache.Niveau == 0)
                {
                    tache.DureeTotale = tache.Duree;
                }
            }

            uint niveau = 1;

            do
            {
                foreach (var tache in Taches)
                {
                    if (tache.Niveau == niveau)
                    {
                        uint maxDuree = 0;
                        foreach (var tache2 in Taches)
                        {
                            foreach (var pre in tache.ListPredecesseurs)
                            {
                                if (pre == tache2.Lettre)
                                {
                                    if (maxDuree < tache2.DureeTotale)
                                    {
                                        maxDuree = tache2.DureeTotale;
                                    }
                                }
                            }
                        }

                        tache.DureeTotale = maxDuree + tache.Duree;

                    }
                }
                niveau++;
            }
            while (niveau <= LvlMax);

            /* -------------------------------------------------- */

            int ActualLevel = Convert.ToInt32(LvlMax);

            foreach (var tache in Taches)
            {
                if (tache.Niveau == ActualLevel)
                {
                    tache.DureeTotaleLarge = tache.DureeTotale;
                }
            }
            ActualLevel--;

            do
            {
                foreach (var tache in Taches)
                {
                    if (tache.Niveau == ActualLevel)
                    {
                        int newDureeTotaleLarge = -1;
                        foreach (var tache2 in Taches)
                        {
                            foreach (var pre in tache2.ListPredecesseurs)
                            {
                                if (pre == tache.Lettre)
                                {
                                    if (newDureeTotaleLarge > (tache2.DureeTotaleLarge - tache2.Duree) || newDureeTotaleLarge == -1)
                                    {
                                        newDureeTotaleLarge = Convert.ToInt32((tache2.DureeTotaleLarge - tache2.Duree));
                                    }
                                }
                            }
                        }

                        if (newDureeTotaleLarge == -1)
                        {
                            uint maxDureeTotaleLarge = 0;

                            foreach (var tache2 in Taches)
                            {
                                if (tache2.Niveau == LvlMax && tache2.DureeTotaleLarge > maxDureeTotaleLarge)
                                {
                                    maxDureeTotaleLarge = tache2.DureeTotaleLarge;
                                }
                            }

                            newDureeTotaleLarge = Convert.ToInt32(maxDureeTotaleLarge);
                        }

                        tache.DureeTotaleLarge = Convert.ToUInt32(newDureeTotaleLarge);

                    }
                }
                ActualLevel--;
            }
            while (ActualLevel >= 0);

            /* -------------------------------------------------- */

            return Taches;
        }

        private class DisplayTache
        {
            private string _taches;

            public DisplayTache(string tache)
            {
                this.Taches = tache;
            }

            public DisplayTache(Tache tache)
            {
                this.Taches = tache.ToString();
            }

            public string Taches { get => _taches; set => _taches = value; }
        }

        private class FullDisplayTache
        {
            private string _lettre;
            private string _desc;
            private uint _duree;
            private uint _dureeTotale;
            private uint _dureeTotaleLarge;
            private uint _niveau;
            private string _Predecesseurs;

            public FullDisplayTache(Tache tache)
            {
                Lettre = tache.Lettre;
                Description = tache.Desc;
                Duree = tache.Duree;
                DureeTotale = tache.DureeTotale;
                Marge = tache.DureeTotaleLarge;
                Niveau = tache.Niveau;
                string strPrede = string.Empty;
                foreach (var pre in tache.ListPredecesseurs)
                {
                    strPrede += pre + ", ";
                }
                strPrede = strPrede.Remove(strPrede.Length - 1);
                strPrede = strPrede.Remove(strPrede.Length - 1);

                Predecesseurs = strPrede;
            }

            public string Lettre { get => _lettre; set => _lettre = value; }
            public string Description { get => _desc; set => _desc = value; }
            public uint Duree { get => _duree; set => _duree = value; }
            public uint DureeTotale { get => _dureeTotale; set => _dureeTotale = value; }
            public uint Marge { get => _dureeTotaleLarge; set => _dureeTotaleLarge = value; }
            public uint Niveau { get => _niveau; set => _niveau = value; }
            public string Predecesseurs { get => _Predecesseurs; set => _Predecesseurs = value; }
        }
    }
}

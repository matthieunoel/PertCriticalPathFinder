using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ProjetCheminCritique
{

    public class Tache
    {
        private string _lettre;
        private string _desc;
        private uint _duree;
        private uint _dureeTotale;
        private uint _dureeTotaleLarge;
        private uint _niveau;
        private List<string> _listPredecesseurs;

        public Tache(string lettre, string desc, uint duree, List<string> listPredecesseurs)
        {
            Lettre = lettre;
            Desc = desc;
            Duree = duree;
            ListPredecesseurs = listPredecesseurs;
            _dureeTotale = 0;
            _dureeTotaleLarge = 0;
            _niveau = 0;
        }

        public string Lettre { get => _lettre; set => _lettre = value; }
        public string Desc { get => _desc; set => _desc = value; }
        public uint Duree { get => _duree; set => _duree = value; }
        public uint DureeTotale { get => _dureeTotale; set => _dureeTotale = value; }
        public uint DureeTotaleLarge { get => _dureeTotaleLarge; set => _dureeTotaleLarge = value; }
        public uint Niveau { get => _niveau; set => _niveau = value; }
        public List<string> ListPredecesseurs { get => _listPredecesseurs; set => _listPredecesseurs = value; }

        public override string ToString()
        {
            string Prede = string.Empty;
            foreach (var pre in this.ListPredecesseurs)
            {
                Prede += pre + ", ";
            }
            string medianStr = $"{this.Lettre} : {this.Desc} ({this.Duree}/{this.DureeTotale}/{this.DureeTotaleLarge}); {Prede}";
            medianStr = medianStr.Remove(medianStr.Length - 1);
            medianStr = medianStr.Remove(medianStr.Length - 1);
            return medianStr + $" - Nv{Niveau}";
        }

        public static string ToStringReference()
        {
            return "{Lettre} : {Description} ({Durée}/{Durée totale}/{Marge}); {Predecesseurs} - Nv{Niveau}";
        }
    }
}

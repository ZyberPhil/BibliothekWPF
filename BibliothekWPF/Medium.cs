namespace BibliothekWPF
{
    public class Medium
    {
        public string Titel { get; set; }
        public string Autor { get; set; }
        public int Erscheinungsjahr { get; set; }

        public virtual string Anzeigen()
        {
            return $"Titel: {Titel}, Autor: {Autor}, Erscheinungsjahr: {Erscheinungsjahr}";
        }
    }

    public class Buch : Medium
    {
        public string ISBN { get; set; }
        public string Genre { get; set; }

        public override string Anzeigen()
        {
            return $"{base.Anzeigen()}, ISBN: {ISBN}, Genre: {Genre}";
        }
    }

    public class Zeitschrift : Medium
    {
        public int Ausgabe { get; set; }
        public string Verlag { get; set; }

        public override string Anzeigen()
        {
            return $"{base.Anzeigen()}, Ausgabe: {Ausgabe}, Verlag: {Verlag}";
        }
    }
}
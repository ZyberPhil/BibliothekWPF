using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BibliothekWPF
{
    public partial class MainWindow : Window
    {
        private List<Medium> medienListe;

        public MainWindow()
        {
            InitializeComponent();
            medienListe = new List<Medium>();
        }

        private void MedienTypComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Buch
            if (MedienTypComboBox.SelectedIndex == 0)
            {
                ZusatzTextBlockIsbn.Text = "ISBN:";
                ZusatzTextBlockGenre.Text = "Genre:";
                ZusatzTextBlockIsbn.Visibility = Visibility.Visible;
                ZusatzTextBoxIsbn.Visibility = Visibility.Visible;
                ZusatzTextBlockGenre.Visibility = Visibility.Visible;
                ZusatzTextBoxGenre.Visibility = Visibility.Visible;
                ZusatzTextBlockAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBoxAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBlockVerlag.Visibility = Visibility.Collapsed;
                ZusatzTextBoxVerlag.Visibility = Visibility.Collapsed;
            }
            // Zeitschrift
            else if (MedienTypComboBox.SelectedIndex == 1)
            {
                ZusatzTextBlockAusgabe.Text = "Ausgabe:";
                ZusatzTextBlockVerlag.Text = "Verlag:";
                ZusatzTextBlockIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBoxIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBlockGenre.Visibility = Visibility.Collapsed;
                ZusatzTextBoxGenre.Visibility = Visibility.Collapsed;
                ZusatzTextBlockAusgabe.Visibility = Visibility.Visible;
                ZusatzTextBoxAusgabe.Visibility = Visibility.Visible;
                ZusatzTextBlockVerlag.Visibility = Visibility.Visible;
                ZusatzTextBoxVerlag.Visibility = Visibility.Visible;
            }
        }

        private void HinzufuegenButton_Click(object sender, RoutedEventArgs e)
        {
            string titel = TitelTextBox.Text;
            string autor = AutorTextBox.Text;
            int erscheinungsjahr = int.Parse(ErscheinungsjahrTextBox.Text);

            // Buch
            if (MedienTypComboBox.SelectedIndex == 0)
            {
                string isbn = ZusatzTextBoxIsbn.Text;
                string genre = ZusatzTextBoxGenre.Text;

                Buch buch = new Buch
                {
                    Titel = titel,
                    Autor = autor,
                    Erscheinungsjahr = erscheinungsjahr,
                    ISBN = isbn,
                    Genre = genre
                };
                medienListe.Add(buch);
                MedienListBox.Items.Add(buch.Anzeigen());
                EntfernenComboBox.Items.Add($"[Buch] {buch.Titel}");

                // Felder wieder ausblenden
                ZusatzTextBlockIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBoxIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBlockGenre.Visibility = Visibility.Collapsed;
                ZusatzTextBoxGenre.Visibility = Visibility.Collapsed;

                // ComboBox auf standard
                EntfernenComboBox.Text = "";
                EntfernenComboBox.SelectedIndex = -1;
            }

            // Zeitschrift
            else if (MedienTypComboBox.SelectedIndex == 1)
            {
                int ausgabe = int.Parse(ZusatzTextBoxGenre.Text);
                string verlag = ZusatzTextBoxVerlag.Text;

                Zeitschrift zeitschrift = new Zeitschrift
                {
                    Titel = titel,
                    Autor = autor,
                    Erscheinungsjahr = erscheinungsjahr,
                    Ausgabe = ausgabe,
                    Verlag = verlag
                };
                medienListe.Add(zeitschrift);
                MedienListBox.Items.Add(zeitschrift.Anzeigen());
                EntfernenComboBox.Items.Add($"[Zeitschrift] {zeitschrift.Titel}");

                // Felder wieder ausblenden
                ZusatzTextBlockAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBoxAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBlockVerlag.Visibility = Visibility.Collapsed;
                ZusatzTextBoxVerlag.Visibility = Visibility.Collapsed;

                // ComboBox auf standard
                EntfernenComboBox.Text = "";
                EntfernenComboBox.SelectedIndex = -1;

            }
        }

        private void EntfernenButton_Click(object sender, RoutedEventArgs e)
        {
            if (EntfernenComboBox.SelectedItem != null)
            {
                string selectedItem = EntfernenComboBox.SelectedItem.ToString();
                string titel = selectedItem.Substring(selectedItem.IndexOf(']') + 2);

                Medium zuEntfernen = medienListe.Find(m => m.Titel == titel);
                if (zuEntfernen != null)
                {
                    medienListe.Remove(zuEntfernen);
                    MedienListBox.Items.Remove(zuEntfernen.Anzeigen());
                    EntfernenComboBox.Items.Remove(selectedItem);
                }
            }
        }
    }
}

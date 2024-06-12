using System;
using System.Collections.Generic;
using System.Linq;
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
            BücherCheckBox.Checked += FilterCheckBox_Checked;
            BücherCheckBox.Unchecked += FilterCheckBox_Checked;
            ZeitschriftenCheckBox.Checked += FilterCheckBox_Checked;
            ZeitschriftenCheckBox.Unchecked += FilterCheckBox_Checked;
        }

        private void FilterCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AktualisiereMedienListBox();
        }

        private void AktualisiereMedienListBox()
        {
            MedienListBox.Items.Clear();
            IEnumerable<Medium> gefilterteMedien = medienListe;

            if (BücherCheckBox.IsChecked == true && ZeitschriftenCheckBox.IsChecked == false)
            {
                gefilterteMedien = gefilterteMedien.OfType<Buch>();
            }
            else if (BücherCheckBox.IsChecked == false && ZeitschriftenCheckBox.IsChecked == true)
            {
                gefilterteMedien = gefilterteMedien.OfType<Zeitschrift>();
            }

            foreach (var medium in gefilterteMedien)
            {
                MedienListBox.Items.Add(medium.Anzeigen());
            }
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
            else if (MedienTypComboBox.SelectedIndex == 2)
            {
                ZusatzTextBlockAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBoxAusgabe.Visibility = Visibility.Collapsed;
                ZusatzTextBlockVerlag.Visibility = Visibility.Collapsed;
                ZusatzTextBoxVerlag.Visibility = Visibility.Collapsed;
                ZusatzTextBlockIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBoxIsbn.Visibility = Visibility.Collapsed;
                ZusatzTextBlockGenre.Visibility = Visibility.Collapsed;
                ZusatzTextBoxGenre.Visibility = Visibility.Collapsed;
                // Felder clearen
                TitelTextBox.Text = "";
                AutorTextBox.Text = "";
                ErscheinungsjahrTextBox.Text = "";
            }
        }

        private void HinzufuegenButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitelTextBox.Text) ||
                string.IsNullOrWhiteSpace(AutorTextBox.Text) ||
                !int.TryParse(ErscheinungsjahrTextBox.Text, out int erscheinungsjahr))
            {
                MessageBox.Show("Bitte füllen Sie alle Felder korrekt aus.", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string titel = TitelTextBox.Text;
            string autor = AutorTextBox.Text;

            // Buch
            if (MedienTypComboBox.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(ZusatzTextBoxIsbn.Text) ||
                    string.IsNullOrWhiteSpace(ZusatzTextBoxGenre.Text))
                {
                    MessageBox.Show("Bitte füllen Sie alle Felder für das Buch aus.", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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

                // Felder clearen
                TitelTextBox.Text = "";
                AutorTextBox.Text = "";
                ErscheinungsjahrTextBox.Text = "";

                // ComboBox auf standard (funktioniert nicht)
                EntfernenComboBox.Text = "";
                EntfernenComboBox.SelectedIndex = -1;
            }

            // Zeitschrift
            else if (MedienTypComboBox.SelectedIndex == 1)
            {
                if (!int.TryParse(ZusatzTextBoxAusgabe.Text, out int ausgabe) ||
                    string.IsNullOrWhiteSpace(ZusatzTextBoxVerlag.Text))
                {
                    MessageBox.Show("Bitte füllen Sie alle Felder für die Zeitschrift korrekt aus.", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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

                // Felder clearen
                TitelTextBox.Text = "";
                AutorTextBox.Text = "";
                ErscheinungsjahrTextBox.Text = "";

                // ComboBox auf standard (funktioniert nicht)
                EntfernenComboBox.Text = "";
                EntfernenComboBox.SelectedIndex = -1;

            }
            else if (MedienTypComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Bitte wählen sie eine Medium art aus!", "Fehlende Angaben", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

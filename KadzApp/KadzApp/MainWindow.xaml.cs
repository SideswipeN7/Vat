using KadzApp.steel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KadzApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Steel steel;
        private Vat vat;

        public MainWindow()
        {
            InitializeComponent();
            InitObjects();
            InitComboBoxes();
            SetVisibility();
        }

        private void SetVisibility()
        {
            //vat compute
            txbVatRScale_1.Visibility = Visibility.Hidden;
            txbVatRScale_2.Visibility = Visibility.Hidden;
            lbVatRScale.Visibility = Visibility.Hidden;
            lbVatRscaleInfo.Visibility = Visibility.Hidden;
            //vat scale
            txbVatScale_1.Visibility = Visibility.Hidden;
            txbVatScale_2.Visibility = Visibility.Hidden;
            lbVatScale.Visibility = Visibility.Hidden;
            gdScale.Visibility = Visibility.Hidden;
            gdVatScale.Visibility = Visibility.Hidden;
        }

        private void InitObjects()
        {
            steel = new Steel()
            {
                WeightUnit = WEIGHT_UNITS.KG,
                DensityUnit = DENSITY_UNITS.KGM3,
                SizeUnit = SIZE_UNITS.M3,
                Density = 0,
                Size = 0,
                Weight = 0
            };
            vat = new Vat();
        }

        private void InitComboBoxes()
        {
            //Steel
            //Weight units
            cbSteelWeight.Items.Add(new ComboBoxItem() { Content = "kg", Tag = "KG" });
            cbSteelWeight.Items.Add(new ComboBoxItem() { Content = "T", Tag = "T" });
            cbSteelWeight.SelectedIndex = 1;
            //Density units
            cbSteelDensity.Items.Add(new ComboBoxItem() { Content = "g/cm^3", Tag = "GCM3" });
            cbSteelDensity.Items.Add(new ComboBoxItem() { Content = "kg/m^3", Tag = "KGM3" });
            cbSteelDensity.SelectedIndex = 1;
            //Size units
            cbSteelSize.Items.Add(new ComboBoxItem() { Content = "dcm^3", Tag = "DCM3" });
            cbSteelSize.Items.Add(new ComboBoxItem() { Content = "m^3", Tag = "M3" });
            cbSteelSize.SelectedIndex = 1;
            //Vat
            //Height units
            cbHeight.Items.Add(new ComboBoxItem() { Content = "dm", Tag = "DCM" });
            cbHeight.Items.Add(new ComboBoxItem() { Content = "m", Tag = "M" });
            cbHeight.SelectedIndex = 1;
            //Upper diameter units
            cbUpDiameter.Items.Add(new ComboBoxItem() { Content = "dm", Tag = "DCM" });
            cbUpDiameter.Items.Add(new ComboBoxItem() { Content = "m", Tag = "M" });
            cbUpDiameter.SelectedIndex = 1;
            //Lower diameter units
            cbLowDiameter.Items.Add(new ComboBoxItem() { Content = "dm", Tag = "DCM" });
            cbLowDiameter.Items.Add(new ComboBoxItem() { Content = "m", Tag = "M" });
            cbLowDiameter.SelectedIndex = 1;
            //Vat size
            cbVatSize.Items.Add(new ComboBoxItem() { Content = "dcm^3", Tag = "DCM3" });
            cbVatSize.Items.Add(new ComboBoxItem() { Content = "m^3", Tag = "M3" });
            cbVatSize.SelectedIndex = 1;
            //TextBoxes
            txbVatRScale_1.Text = $"{vat.RscalerLeft}";
            txbVatRScale_2.Text = $"{vat.RscalerRight}";
            txbVatScale_1.Text = $"{vat.VatscaleLeft}";
            txbVatScale_2.Text = $"{vat.VatscaleRight}";
        }

        //TAB Size
        private void RecalcSteel()
        {
            steel.Calc();
            txbSteelSize.Text = steel.GetSize();
        }

        private void TxbSteelWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(txbSteelWeight.Text, out double weight))
            {
                steel.Weight = weight;
                RecalcSteel();
            }
        }

        private void TxbSteelDensity_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(txbSteelDensity.Text, out double density))
            {
                steel.Density = density;
                RecalcSteel();
            }
        }

        private void CbWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSteelWeight.SelectedIndex >= 0)
            {
                steel.WeightUnit = (WEIGHT_UNITS)Enum.Parse(typeof(WEIGHT_UNITS), ((ComboBoxItem)cbSteelWeight.SelectedItem).Tag.ToString());
                RecalcSteel();
            }
        }

        private void CbDensity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSteelDensity.SelectedIndex >= 0)
            {
                steel.DensityUnit = (DENSITY_UNITS)Enum.Parse(typeof(DENSITY_UNITS), ((ComboBoxItem)cbSteelDensity.SelectedItem).Tag.ToString());
                RecalcSteel();
            }
        }

        private void CbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSteelSize.SelectedIndex >= 0)
            {
                steel.SizeUnit = (SIZE_UNITS)Enum.Parse(typeof(SIZE_UNITS), ((ComboBoxItem)cbSteelSize.SelectedItem).Tag.ToString());
                RecalcSteel();
            }
        }

        //TAB Vat
        private void RecalcVat()
        {
            vat.Calc();
            ChangeD1Lb();
            ChangeD2Lb();
            ChangeHLb();
            if (vat.Compute)
            {
                txbVatD1.Text = $"{vat.UpperDiamater}";
                txbVatD2.Text = $"{vat.LowerDiamater}";
                txbVatH.Text = $"{vat.Height}";
            }
            else
            {
                txbVatSize.Text = $"{vat.SteelSize}";
            }
            ChVatScale_Checked(null, null);
        }

        private void TxbVatD1_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(txbVatD1.Text, out double upperDiameter))
            {
                vat.UpperDiamater = upperDiameter;
                RecalcVat();
            }
        }

        private void TxbVatH_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(txbVatH.Text, out double height))
            {
                vat.Height = height;
                RecalcVat();
            }
        }

        private void TxbVatD2_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(txbVatD2.Text, out double LowerDiameter))
            {
                vat.LowerDiamater = LowerDiameter;
                RecalcVat();
            }
        }

        private void CbHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHeight.SelectedIndex >= 0)
            {
                vat.HeightUnit = (DIMENSIONS_UNITS)Enum.Parse(typeof(DIMENSIONS_UNITS), ((ComboBoxItem)cbHeight.SelectedItem).Tag.ToString());
                RecalcVat();
            }
        }

        private void CbUpDiameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUpDiameter.SelectedIndex >= 0)
            {
                vat.UpperDiamaterUnit = (DIMENSIONS_UNITS)Enum.Parse(typeof(DIMENSIONS_UNITS), ((ComboBoxItem)cbUpDiameter.SelectedItem).Tag.ToString());
                RecalcVat();
            }
        }

        private void CbLowDiameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLowDiameter.SelectedIndex >= 0)
            {
                vat.LowerDiamaterUnit = (DIMENSIONS_UNITS)Enum.Parse(typeof(DIMENSIONS_UNITS), ((ComboBoxItem)cbLowDiameter.SelectedItem).Tag.ToString());
                RecalcVat();
            }
        }

        private void CbVatSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVatSize.SelectedIndex >= 0)
            {
                vat.SteelSizeUnit = (SIZE_UNITS)Enum.Parse(typeof(SIZE_UNITS), ((ComboBoxItem)cbVatSize.SelectedItem).Tag.ToString());
                RecalcVat();
            }
        }


        private void BtnSaveVat_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Obliczona_Kadź", // Default file name
                DefaultExt = ".Kadz", // Default file extension
                Filter = "Text documents (.Kadz)|*.Kadz" // Filter files by extension
            };

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string fileName = dlg.FileName;
                File.WriteAllText(fileName, JsonConvert.SerializeObject(vat));
            }
        }

        private void BtnReadVat_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".Kadz", // Default file extension
                Filter = "Text documents (.Kadz)|*.Kadz" // Filter files by extension
            };

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string fileName = dlg.FileName;
                    vat = JsonConvert.DeserializeObject<Vat>(File.ReadAllText(fileName));
                    ChangeD1Lb();
                    ChangeD2Lb();
                    ChangeHLb();
                    txbVatD1.Text = $"{vat.UpperDiamater}";
                    txbVatD2.Text = $"{vat.LowerDiamater}";
                    txbVatH.Text = $"{vat.Height}";
                    RecalcVat();
                    chVatScale.IsChecked = false;
                    if (vat.UseScale)
                    {
                        txbVatRScale_1.Text = $"{vat.RscalerLeft}";
                        txbVatRScale_2.Text = $"{vat.RscalerRight}";
                        chVatScale.IsChecked = true;
                    }
                    chVatRSize.IsChecked = false;
                    if (vat.Compute)
                    {
                        txbVatScale_1.Text = $"{vat.VatscaleLeft}";
                        txbVatScale_2.Text = $"{vat.VatscaleRight}";
                        chVatRSize.IsChecked = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("[ERROR] Bład wczytywania pliku");
                    Console.Error.WriteLine($"[ERROR] {ex}");
                    MessageBoxResult dialog = MessageBox.Show("Bład wczytywania pliku", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ChVatScale_Checked(object sender, RoutedEventArgs e)
        {
            vat.UseScale = (bool)chVatScale.IsChecked;
            if (vat.UseScale)
            {
                txbVatScale_1.Visibility = Visibility.Visible;
                txbVatScale_2.Visibility = Visibility.Visible;
                lbVatScale.Visibility = Visibility.Visible;
                gdScale.Visibility = Visibility.Visible;
                gdVatScale.Visibility = Visibility.Visible;
                RecalcSmallVat();
            }
            else
            {
                txbVatScale_1.Visibility = Visibility.Hidden;
                txbVatScale_2.Visibility = Visibility.Hidden;
                lbVatScale.Visibility = Visibility.Hidden;
                gdScale.Visibility = Visibility.Hidden;
                gdVatScale.Visibility = Visibility.Hidden;
            }
        }

        private void ChangeD1Lb()
        {
            try
            {
                lbVatCalcD1.Content = $"{vat.UpperDiamater} {((ComboBoxItem)cbUpDiameter.SelectedItem).Content}";
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[ERROR] {nameof(ChangeD1Lb)} {e.ToString()}");
            }
        }

        private void ChangeD2Lb()
        {
            try
            {
                lbVatCalcD2.Content = $"{vat.LowerDiamater} {((ComboBoxItem)cbLowDiameter.SelectedItem).Content}";
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[ERROR] {nameof(ChangeD2Lb)} {e.ToString()}");
            }
        }

        private void ChangeHLb()
        {
            try
            {
                lbVatCalcHeight.Content = $"{vat.Height} {((ComboBoxItem)cbHeight.SelectedItem).Content}";
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[ERROR] {nameof(ChangeHLb)} {e.ToString()}");
            }
        }

        private void ChVatRScale_Checked(object sender, RoutedEventArgs e)
        {
            vat.Compute = (bool)chVatRSize.IsChecked;
            txbVatD2.IsEnabled = !vat.Compute;
            txbVatD1.IsEnabled = !vat.Compute;
            txbVatH.IsEnabled = !vat.Compute;
            if (vat.Compute)
            {
                txbVatRScale_1.Visibility = Visibility.Visible;
                txbVatRScale_2.Visibility = Visibility.Visible;
                lbVatRScale.Visibility = Visibility.Visible;
                lbVatRscaleInfo.Visibility = Visibility.Visible;
                if (int.TryParse(txbVatRScale_1.Text, out int left) && int.TryParse(txbVatRScale_2.Text, out int right))
                {
                    if (left != right)
                    {
                        vat.RscalerLeft = left;
                        vat.RscalerRight = right;
                        txbVatSize.Text = $"{steel.Size}";
                        vat.SteelSize = steel.Size;
                        vat.SteelSizeUnit = SIZE_UNITS.M3;
                        cbVatSize.SelectedIndex = 1;
                        RecalcVat();
                    }
                }
            }
            else
            {
                txbVatRScale_1.Visibility = Visibility.Hidden;
                txbVatRScale_2.Visibility = Visibility.Hidden;
                lbVatRScale.Visibility = Visibility.Hidden;
                lbVatRscaleInfo.Visibility = Visibility.Hidden;
            }
        }


        //On vat R scale Key UP
        private void TxbVatRScale_KeyUp(object sender, KeyEventArgs e)
        {
            if (int.TryParse(txbVatRScale_2.Text, out int right) && int.TryParse(txbVatRScale_1.Text, out int left))
            {
                vat.RscalerRight = right;
                vat.RscalerLeft = left;
                RecalcVat();
            }
        }

        //On small vat scale Key UP
        private void TxbVatScale_KeyUp(object sender, KeyEventArgs e)
        {
            if (int.TryParse(txbVatScale_1.Text, out int left) && int.TryParse(txbVatScale_2.Text, out int right))
            {
                vat.VatscaleRight = right;
                vat.VatscaleLeft = left;
                RecalcSmallVat();
            }
        }

        private void RecalcSmallVat()
        {
            lbVatSmallD1Val.Content = $"{vat.UpperDiamaterInScale()} m";
            lbVatCalcD1Small.Content = $"{vat.UpperDiamaterInScale()} m";
            lbVatSmallD2Val.Content = $"{vat.LowerDiamaterInScale()} m";
            lbVatCalcD2Small.Content = $"{vat.LowerDiamaterInScale()} m";
            lbVatSmallHVal.Content = $"{vat.HeightInScale()} m";
            lbVatCalcHeightSmall.Content = $"{vat.HeightInScale()} m";
            lbVatSmallSizeVal.Content = $"{vat.SteelSizeInScale()} m^3";
        }
    }
}
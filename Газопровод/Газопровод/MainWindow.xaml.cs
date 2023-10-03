using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Газопровод
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point[] pointsP;
        Point[] pointsT;
        Point[] pointsV;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void checkBoxSettingsPipeline_Checked(object sender, RoutedEventArgs e)
        {
            stackPanelSettingsPipeline.Visibility = Visibility.Visible;
        }
        private void checkBoxSettingsPipeline_Unchecked(object sender, RoutedEventArgs e)
        {
            stackPanelSettingsPipeline.Visibility = Visibility.Collapsed;
        }
        private void checkBoxMainSettings_Checked(object sender, RoutedEventArgs e)
        {
            stackPanelMainSettings.Visibility = Visibility.Visible;
        }
        private void checkBoxMainSettings_Unchecked(object sender, RoutedEventArgs e)
        {
            stackPanelMainSettings.Visibility = Visibility.Collapsed;
        }
        private void checkBoxGasComposition_Checked(object sender, RoutedEventArgs e)
        {
            stackPanelGasComposition.Visibility = Visibility.Visible;
        }
        private void checkBoxGasComposition_Unchecked(object sender, RoutedEventArgs e)
        {
            stackPanelGasComposition.Visibility = Visibility.Collapsed;
        }
        private void checkBoxSoilCharacteristics_Checked(object sender, RoutedEventArgs e)
        {
            stackPanelSoilCharacteristics.Visibility = Visibility.Visible;
        }
        private void checkBoxSoilCharacteristics_Unchecked(object sender, RoutedEventArgs e)
        {
            stackPanelSoilCharacteristics.Visibility = Visibility.Collapsed;
        }
        private void ClickButtonInput(object sender, RoutedEventArgs e)
        {
            if (!CheckDataInput())
            {
                MessageBox.Show("Не все входные данные были введены", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ChecktextBoxThermalConductivityCoefficientOfInsulation())
            {
                MessageBox.Show("Коэффициент теплопроводности изоляции введен неверно. Даже если отсутствует изоляция, для расчета необходимо использовать ненулевое значение, которое будет учтено обнулением толщины изоляции", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ChecktextBoxThermalConductivityCoefficientOfSnowCover())
            {
                MessageBox.Show("Коэффициент теплопроводности снежного покрова введен неверно. Должна стоять ненулевая величина даже если нет снежного покрова, его отсутствие учтено обнулением толщины снежного покрова", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            GasProperties gasProperties = new GasProperties();

            WriteRes(gasProperties);

            pointsP = gasProperties.graphP.ToArray();
            pointsT = gasProperties.graphT.ToArray();
            pointsV = gasProperties.graphV.ToArray();

            buttinBuildPressure.IsEnabled = true;
            buttinBuildTemperature.IsEnabled = true;
            buttinBuildSpeed.IsEnabled = true;
        }
        private bool ChecktextBoxThermalConductivityCoefficientOfInsulation() { if (textBoxThermalConductivityCoefficientOfInsulation.Text == "0") return false; return true; }
        private bool ChecktextBoxThermalConductivityCoefficientOfSnowCover() { if (textBoxThermalConductivityCoefficientOfSnowCover.Text == "0") return false; return true; }
        private bool CheckDataInput()
        {
            if (string.IsNullOrEmpty(textBoxInnerDiameter.Text)) return false;
            if (string.IsNullOrEmpty(textBoxOutsideDiameter.Text)) return false;
            if (string.IsNullOrEmpty(textBoxInitialPressure.Text)) return false;
            if (string.IsNullOrEmpty(textBoxLayingDepth.Text)) return false;
            if (string.IsNullOrEmpty(textBoxGasPipelineCapacity.Text)) return false;
            if (string.IsNullOrEmpty(textBoxInitialTemperature.Text)) return false;
            if (string.IsNullOrEmpty(textBoxAmbientTemperature.Text)) return false;
            if (string.IsNullOrEmpty(textBoxPipelineLength.Text)) return false;
            if (string.IsNullOrEmpty(textBoxAverageWindSpeed.Text)) return false;
            if (string.IsNullOrEmpty(textBoxInsulationThickness.Text)) return false;
            if (string.IsNullOrEmpty(textBoxThermalConductivityCoefficientOfInsulation.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSoilThermalConductivityCoefficient.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSnowThickness.Text)) return false;
            if (string.IsNullOrEmpty(textBoxThermalConductivityCoefficientOfSnowCover.Text)) return false;

            if (string.IsNullOrEmpty(textBoxМethaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxEthaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxPropaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxIsobutaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxButaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxNeoPentaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxIsopentaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxPentaneСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxHexanePlusСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxNitrogenСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxCO2Сoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxOxygenСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxHeliumСoncentration.Text)) return false;
            if (string.IsNullOrEmpty(textBoxHydrogenСoncentration.Text)) return false;

            if (string.IsNullOrEmpty(textBoxLoadReliabilityCoefficient.Text)) return false;
            if (string.IsNullOrEmpty(textBoxLinearExpansionCoefficient.Text)) return false;
            if (string.IsNullOrEmpty(textBoxPoissonCoefficient.Text)) return false;
            if (string.IsNullOrEmpty(textBoxFillHeight.Text)) return false;
            if (string.IsNullOrEmpty(textBoxPipeDensity.Text)) return false;
            if (string.IsNullOrEmpty(textBoxProductMassReliabilityCoefficient.Text)) return false;
            if (string.IsNullOrEmpty(textBoxInsulationWeight.Text)) return false;

            if (string.IsNullOrEmpty(textBoxAngleOfFriction.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSoilCohesion.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSoilFriction.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSoilDensity.Text)) return false;
            if (string.IsNullOrEmpty(textBoxSoilUnitWeight.Text)) return false;
            if (string.IsNullOrEmpty(textBoxOverburdenCoefficient.Text)) return false;

            return true;
        }
        private void WriteRes(GasProperties gasProperties)
        {
            textBoxEndPressure.Text = Math.Round(gasProperties.PkR, 2).ToString();
            textBoxAverageTemperature.Text = Math.Round(gasProperties.TsrR, 1).ToString();
            textBoxTemperatureDropCoefficient.Text = Math.Round(gasProperties.aR, 4).ToString();
            textBoxJouleThomsonCoefficient.Text = Math.Round(gasProperties.DiR, 3).ToString();
            textBoxHeatTransferCoefficient.Text = Math.Round(gasProperties.ksr, 3).ToString();
            textBoxCompressibilityCoefficient.Text = Math.Round(gasProperties.ZsrR, 3).ToString();
            textBoxFinalTemperature.Text = Math.Round(gasProperties.TkR, 1).ToString();
            textBoxAveragePressure.Text = Math.Round(gasProperties.Psr, 2).ToString();

            textBoxLongitudinaMovement.Text = Math.Round(gasProperties.u0 * 1000, 1).ToString();
            textBoxSectionLength.Text = Math.Round(gasProperties.L_t, 0).ToString();
        }
        /// <summary>
        /// Метод,вызывающий окна с графиками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBuildGraph(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "buttinBuildPressure")
            {

                Graph graph = new Graph("График падения давления по длине газопровода", pointsP, "Длина газопровода, м", "Давление в газопроводе, МПа");
                graph.Show();
            }
            if (button.Name == "buttinBuildTemperature")
            {
                Graph graph = new Graph("График падения температуры по длине газопровода", pointsT, "Длина газопровода, м", "Температура в газопроводе, К");
                graph.Show();
            }
            if (button.Name == "buttinBuildSpeed")
            {
                Graph graph = new Graph("График измененния скорости по длине газопровода", pointsV, "Длина газопровода, м", "Скорость газа в газопроводе, м/c");
                graph.Show();
            }
        }
        private void ClickClear(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(this);
        }
        /// <summary>
        ///  Очистка text из TextBox
        /// </summary>
        /// <param name="parent"></param>
        private void ClearTextBoxes(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                    textBox.Text = "";
                }
                else
                {
                    ClearTextBoxes(child);
                }
            }
        }
        /// <summary>
        /// Проверка введенных значений в TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Allow only digits (0-9), the minus sign (-), and the period or comma as a decimal separator
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != '-' && e.Text[0] != '.' && e.Text[0] != ',')
            {
                e.Handled = true;
            }
            if (e.Text[0] == ',' && ((TextBox)sender).Text.Contains(','))
            {
                e.Handled = true;
            }
            if (e.Text[0] == '-')
            {
                if (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains('-'))
                {
                    e.Handled = true;
                }
            }

        }
        /// <summary>
        /// Замена всех '.' на ',' внутри TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = textBox.Text.Replace('.', ',');
            textBox.Text = textBox.Text.Replace("-,", "-0,");
        }
        /// <summary>
        /// В случае, если в TextBox первым символом является ',', то перед ней ставим 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.EndsWith(",")) textBox.Text += '0';
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Газопровод
{
    public class GasProperties
    {
        private double d; // внутренний диаметр в мм
        private double dn; // наружный диаметр в мм
        private double Pn; // начальное давление в МПа
        private double h0; // глубина заложения газопровода до оси в м
        private double q; // пропускная способность газопровода в млн. м³/сут
        private double Tn; // начальная температура в кельвинах
        private double Tokr; // температура в кельвинах за на глубине грунта в тепловом естественном состоянии + среды окружающей температура
        private double L; // длина участка в км
        private double v; // средняя скорость ветра в м/с
        private double delta_iz; // толщина изоляции в мм, если нет, то ноль
        private double lambda_iz; // коэффициент теплопроводности изоляции
        private double lambda_gr; ////коэффициент теплопроводности грунта Вт/(м* К);
        private double delta_sn; // толщина снежного покрова в м, если нет, то ноль;
        private double lambda_sn; //коэффициент теплопроводности снежного покрова Вт/(м* К)
        private double[] concentrations; // Компонентный состав транспортируемого газа 
        private double gP=0;
        private double delta_d;

        private double delta_t;//температурный перепад, 0C
        private double coeff_n;//коэффициент надежности по нагрузке давлением
        private double E;// модуль Юнга стали, МПа
        private double coeff_a;//коэффициент линейного расширения стали
        private double coeff_mu;//коэффициент Пуассона для стали
        private double h;//высота засыпки до верхней образующей трубы, м
        private double Ymp;//удельный вес стали трубы, Н/м3
        private double n_vr;//коэффициент надежности по нагрузке массой продукта
        private double q_iz;// погонный вес изоляции Н/м

        private double phi;
        private double C0;
        private double Cox;
        private double Pgr;
        private double Ygr;
        private double ngr;

        private double M;//молярная масса газовой смеси
        private double Ppk;// псевдокритическое давление
        private double Tpk;// псевдокритическая температура
        private double Pprc;// приведенное давление для стандартныйх условий
        private double Tprc;// приведенная температура для стандартных условий

        private double A1c;
        private double A2c;
        

        private double rhoSt; // плотность стандартных условиях
        private double delta; // относительная плотность природного газа по воздуху
        private double Pprsr; // приведенное давление как функция среднего давления
        private double Tprsr; // приведенная температура как функция средней тепературы
        private double muSr; // вязкость газа при средних условиях
        private double mu0sr;
        private double B1sr;
        private double B2sr;
        private double B3sr;
        private double R;
        private double Cp;// полиномальная функция для теплоемкости газа функция средней температуры и среднего давления
        private double E0;
        private double E1;
        private double E2;
        private double E3;
        private double H0;
        private double H1;
        private double H2;
        private double H3;
        private double A1sr;
        private double A2sr;
        private double Zc;

        private double alphaVozd;
        private double alphaGr;
        private double rIz;
        private double hoe;
        
        public double a;
        public double Tsr;
        public double Pk;
        public double Di;
        public double Zsr;
        public double ksr;
        public double Psr;
        public double Tk;


        public double PkR;
        public double TsrR;
        public double PprsrR;
        public double TprsrR;
        public double B1srR;
        public double B2srR;
        public double B3srR;
        public double mu0srR;
        public double muSrR;
        public double E0R;
        public double E1R;
        public double E2R;
        public double E3R;
        public double H0R;
        public double H1R;
        public double H2R;
        public double H3R;
        public double CpR;
        public double DiR;
        public double A1srR;
        public double A2srR;
        public double ZsrR;
        public double aR;
        public double TkR;

        public double EI;//Изгибная жесткость прямой трубы
        public double F;//Площадь сечения стенок трубы
        public double q_mp;//Вес единицы длины трубы
        public double q_gaz;//Вес продукта (газа) в единице длины трубы
        public double qn_mp;//Вес трубы с продуктом и изоляцией
        public double Ch;//Коэффициент, учитывающий образование свода естественного равновесия грунта
        public double t_np;//Предельное сопротивление грунта сдвигу

        public double sigma_kc;//Кольцевые напряжения
        public double S;//Эквивалентное усилие
        public double Y;//Параметр упругой работы грунта
        public double K;//Проверка критерия, характеризующего наличие участка предельного равновесия грунта по длине трубопровода
        public double u0;//Продольное перемещение конца газопровода 
        public double L_t;//Длина участка, с которого собираются температурные перемещения к его началу


        public List<Point> graphP = new();
        public List<Point> graphT = new();
        public List<Point> graphV = new();
        public GasProperties()
        {
            GetInputData();
            DeterminationOfTheParametersOfTheTransportedGas();
            DeterminationOfTheTotalHeatTransferCoefficient();
            test(L);
            CalculateReSVal();
            testGetPoints();

            CalculateEI();
            CalculateLimitingSoilResistance();
            CalculateEquivalentAxialForce();
        }
        private void GetInputData()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            d = Convert.ToDouble(mainWindow.textBoxInnerDiameter.Text);
            dn = Convert.ToDouble(mainWindow.textBoxOutsideDiameter.Text);
            Pn = Convert.ToDouble(mainWindow.textBoxInitialPressure.Text);
            h0 = Convert.ToDouble(mainWindow.textBoxLayingDepth.Text);
            q = Convert.ToDouble(mainWindow.textBoxGasPipelineCapacity.Text); 
            Tn = Convert.ToDouble(mainWindow.textBoxInitialTemperature.Text);
            Tokr = Convert.ToDouble(mainWindow.textBoxAmbientTemperature.Text);
            L = Convert.ToDouble(mainWindow.textBoxPipelineLength.Text);
            v = Convert.ToDouble(mainWindow.textBoxAverageWindSpeed.Text);
            delta_iz = Convert.ToDouble(mainWindow.textBoxInsulationThickness.Text);
            lambda_iz = Convert.ToDouble(mainWindow.textBoxThermalConductivityCoefficientOfInsulation.Text);
            lambda_gr = Convert.ToDouble(mainWindow.textBoxSoilThermalConductivityCoefficient.Text);
            delta_sn = Convert.ToDouble(mainWindow.textBoxSnowThickness.Text);
            lambda_sn = Convert.ToDouble(mainWindow.textBoxThermalConductivityCoefficientOfSnowCover.Text);

            concentrations = new double[14];
            concentrations[0] = Convert.ToDouble(mainWindow.textBoxМethaneСoncentration.Text);
            concentrations[1] = Convert.ToDouble(mainWindow.textBoxEthaneСoncentration.Text);
            concentrations[2] = Convert.ToDouble(mainWindow.textBoxPropaneСoncentration.Text);
            concentrations[3] = Convert.ToDouble(mainWindow.textBoxIsobutaneСoncentration.Text);
            concentrations[4] = Convert.ToDouble(mainWindow.textBoxButaneСoncentration.Text);
            concentrations[5] = Convert.ToDouble(mainWindow.textBoxNeoPentaneСoncentration.Text);
            concentrations[6] = Convert.ToDouble(mainWindow.textBoxIsopentaneСoncentration.Text);
            concentrations[7] = Convert.ToDouble(mainWindow.textBoxPentaneСoncentration.Text);
            concentrations[8] = Convert.ToDouble(mainWindow.textBoxHexanePlusСoncentration.Text);
            concentrations[9] = Convert.ToDouble(mainWindow.textBoxNitrogenСoncentration.Text);
            concentrations[10] = Convert.ToDouble(mainWindow.textBoxCO2Сoncentration.Text);
            concentrations[11] = Convert.ToDouble(mainWindow.textBoxOxygenСoncentration.Text);
            concentrations[12] = Convert.ToDouble(mainWindow.textBoxHeliumСoncentration.Text);
            concentrations[13] = Convert.ToDouble(mainWindow.textBoxHydrogenСoncentration.Text);

            delta_t = double.Parse(mainWindow.textBoxTemperatureDifference.Text);
            coeff_n = double.Parse(mainWindow.textBoxLoadReliabilityCoefficient.Text);
            E = double.Parse(mainWindow.textBoxYoungModulus.Text);
            coeff_a = double.Parse(mainWindow.textBoxLinearExpansionCoefficient.Text);
            coeff_mu = double.Parse(mainWindow.textBoxPoissonCoefficient.Text);
            h = double.Parse(mainWindow.textBoxFillHeight.Text);
            Ymp = double.Parse(mainWindow.textBoxPipeDensity.Text);
            n_vr = double.Parse(mainWindow.textBoxProductMassReliabilityCoefficient.Text);
            q_iz = double.Parse(mainWindow.textBoxInsulationWeight.Text);

            delta_d = (dn - d) / 2;

            phi = Convert.ToDouble(mainWindow.textBoxAngleOfFriction.Text);
            C0 = Convert.ToDouble(mainWindow.textBoxSoilCohesion.Text);
            Cox = Convert.ToDouble(mainWindow.textBoxSoilFriction.Text);
            Pgr = Convert.ToDouble(mainWindow.textBoxSoilDensity.Text);
            Ygr = Convert.ToDouble(mainWindow.textBoxSoilUnitWeight.Text);
            ngr = Convert.ToDouble(mainWindow.textBoxOverburdenCoefficient.Text);


            M = CalculateMolarMass();
            Ppk = CalculatePseudoCriticalPressure();
            Tpk = CalculatePseudoCriticalTemperature();
            Pprc = CalculateReducedPressure();
            Tprc = CalculateReducedTemperature();
        }
        void test(double L)
        {
            // Задаем начальные условия
            double Pk = 0.0; // начальное значение Pk
            double Tsr = 200.0; // начальное значение Tsr
            double eps = 0.0001;

            // Итерационный процесс
            int n = 0;
            while (n<1000)
            {
                double new_Pk = g1(Pk, Tsr,L);
                double new_Tsr = g2(Pk, Tsr,L);
                double delta = Math.Max(Math.Abs(new_Pk - Pk), Math.Abs(new_Tsr - Tsr));
                if (delta < eps)
                    break;
                Pk = new_Pk;
                Tsr = new_Tsr;
                n++;
            }
            if (Double.IsNaN(Pk))
            {
                this.Pk = 0;
            }
            else
            {
                this.Pk = Pk;
            }
            this.Tsr = Tsr;

        }
        double g1(double Pk, double Tsr, double L)
        {
            CalculateRerVal(Pk, Tsr);
            return Math.Sqrt(Math.Pow(Pn, 2) - Math.Pow(q / (3.32 * Math.Pow(10, -6) * Math.Pow(d, 2.5)),2) * ((0.067 * Math.Pow(158 / (17.75 * 1000 * q * delta / (d * muSr)) + 2 * 0.03 / d, 0.2) * delta * Tsr * Zsr * L) / Math.Pow(0.95, 2)));
        }

        double g2(double Pk, double Tsr, double L)
        {
            CalculateRerVal(Pk, Tsr);
            return Tokr + (Tn - Tokr) / (a * L) * (1 - Math.Exp(-a * L)) - Di * (-Math.Pow(Pk, 2) + Math.Pow(Pn, 2)) / (2 * a * L * 2 / 3 * (Pn + Math.Pow(Pk, 2) / (Pn + Pk))) * (1 - (1 - Math.Exp(-a * L)) / (a * L));
        }

        void testGetPoints()
        {
            double st = L / 1000;
            for (double z = 0; z <= L; z += st)
            {
                bool flagZ0 = false;
                if (z == 0)
                {
                    z = st / 100000;
                    flagZ0 = true;
                }
                test(z);
                graphP.Add(new Point(z, Pk));
                double TkX = Tokr + (Tn - Tokr) * Math.Exp(-aR * z) - DiR * (-Math.Pow(PkR, 2) + Math.Pow(Pn, 2)) / (2 * aR * L * 2 / 3 * (Pn + Math.Pow(PkR, 2) / (Pn + PkR))) * (1 - (Math.Exp(-aR * z)));
                graphT.Add(new Point(z, TkX));
                double PprsrX = 2.0 / 3 * (Pn + Math.Pow(Pk, 2) / (Pn + Pk)) / Ppk;
                double TprsrX = Tsr / Tpk;
                double A1srX = -0.39 + 2.03 / TprsrX - 3.16 / (TprsrX * TprsrX) + 1.09 / (TprsrX * TprsrX * TprsrX);
                double A2srX = 0.0423 - 0.1812 / TprsrX + 0.2124 / (TprsrX * TprsrX);
                double ZsrX = A2srX * PprsrX * PprsrX + A1srX * PprsrX + 1;

                double W = (q * Math.Pow(10, 6) / 24 * Constants.Pc / (Pk + Constants.Pc) * TkX / 273.15 * ZsrX) / (Math.PI * Math.Pow(d / 1000, 2) / 4 * 3600);
                graphV.Add(new Point(z, W));

                if (flagZ0)
                {
                    z = 0;
                }
            }
        }
        private double CalculateMolarMass()
        {
            double M = Constants.M1 * concentrations[0] + Constants.M2 * concentrations[1] + Constants.M3 * concentrations[2] + Constants.M4 * concentrations[3] + Constants.M5 * concentrations[4] + Constants.M6 * concentrations[5] +
               Constants.M7 * concentrations[6] + Constants.M8 * concentrations[7] + Constants.M9 * concentrations[8] + Constants.M10 * concentrations[9] + Constants.M11 * concentrations[10] + Constants.M12 * concentrations[11] + Constants.M13 * concentrations[12] + Constants.M14 * concentrations[13];
            return M;
        }
        private double CalculatePseudoCriticalPressure()
        {
            double Ppk = Constants.Pкр1 * concentrations[0] + Constants.Pкр10 * concentrations[9] + Constants.Pкр11 * concentrations[10] + Constants.Pкр12 * concentrations[11] + Constants.Pкр13 * concentrations[12] + Constants.Pкр14 * concentrations[13] +
                        Constants.Pкр2 * concentrations[1] + Constants.Pкр3 * concentrations[2] + Constants.Pкр4 * concentrations[3] + Constants.Pкр5 * concentrations[4] + Constants.Pкр6 * concentrations[5] + Constants.Pкр7 * concentrations[6] + Constants.Pкр8 * concentrations[7] +
                        Constants.Pкр9 * concentrations[8];

            return Ppk;
        }
        private double CalculatePseudoCriticalTemperature()
        {
            double Tpk = Constants.Tкр1 * concentrations[0] + Constants.Tкр2 * concentrations[1] + Constants.Tкр3 * concentrations[2] +
                        Constants.Tкр4 * concentrations[3] + Constants.Tкр5 * concentrations[4] + Constants.Tкр6 * concentrations[5] +
                        Constants.Tкр7 * concentrations[6] + Constants.Tкр8 * concentrations[7] + Constants.Tкр9 * concentrations[8] +
                        Constants.Tкр10 * concentrations[9] + Constants.Tкр11 * concentrations[10] + Constants.Tкр12 * concentrations[11] +
                        Constants.Tкр13 * concentrations[12] + Constants.Tкр14 * concentrations[13];

            return Tpk;
        }
        private double CalculateReducedPressure()
        {
            return Constants.Pc / CalculatePseudoCriticalPressure();
        }
        public double CalculateReducedTemperature()
        {
            return Constants.Tc / CalculatePseudoCriticalTemperature();
        }

        /// <summary>
        /// Определение параметров транспортируемого газа 
        /// </summary>
        private void DeterminationOfTheParametersOfTheTransportedGas()
        {
            A1c = -0.39 + 2.03 / Tprc - 3.16 / (Tprc * Tprc) + 1.09 / (Tprc * Tprc * Tprc);
            A2c = 0.0423 - 0.1812 / Tprc + 0.2124 / (Tprc * Tprc);
            Zc = A2c * Pprc * Pprc + A1c * Pprc + 1;
            rhoSt = 1000 * M * Constants.Pc / (Constants.Ru * Constants.Tc * Zc);
            delta = rhoSt / Constants.RhoVozd;
            R = 8.3143 / M;
        }
        private void DeterminationOfTheTotalHeatTransferCoefficient()
        {
            alphaVozd = 6.2 + 4.2 * v;
            hoe = h0 + lambda_gr * (1 / alphaVozd + delta_sn / lambda_sn);
            alphaGr = lambda_gr / (Math.Pow(10, -3) * dn) * (0.65 + Math.Pow(Math.Pow(10, -3) * dn / hoe, 2));
            rIz = Math.Pow(10, -3) * dn / (2 * lambda_iz) * Math.Log((dn + 2 * delta_iz) / dn);
            ksr = Math.Pow((rIz + 1 / alphaGr), -1);
        }
        private void CalculateRerVal(double Pk, double Tsr)
        {
            Pprsr = 2.0 / 3 * (Pn + Pk * Pk / (Pn + Pk)) / Ppk;//ok
            Tprsr = Tsr / Tpk;//ok
            Psr = 2.0 / 3 * (Pn + Math.Pow(Pk, 2) / (Pn + Pk));//ok
            Tk = Tokr + (Tn - Tokr) * Math.Exp(-a * L) - Di * (Math.Pow(Pn, 2) - Math.Pow(Pk, 2)) / (2 * a * L * 2.0 / 3.0 * (Pn + Pk * Pk / (Pn + Pk))) * (1 - Math.Exp(-a * L));

            B1sr = -0.67 + 2.36 / Tprsr - 1.93 / Math.Pow(Tprsr, 2);
            B2sr = 0.8 - 2.89 / Tprsr + 2.65 / Math.Pow(Tprsr, 2);
            B3sr = -0.1 + 0.354 / Tprsr - 0.314 / Math.Pow(Tprsr, 2);
            mu0sr = (1.81 + 5.95 * Tprsr) * Math.Pow(10, -6);
            muSr = mu0sr * (B3sr * Pprsr * Pprsr * Pprsr + B2sr * Pprsr * Pprsr + B1sr * Pprsr + 1);
            E0 = 4.437 - 1.015 * Tprsr + 0.591 * Math.Pow(Tprsr, 2);//ok
            E1 = 3.29 - 11.37 / Tprsr + 10.9 / Math.Pow(Tprsr, 2);//ok
            E2 = 3.23 - 16.27 / Tprsr + 25.48 / Math.Pow(Tprsr, 2) - 11.81 / Math.Pow(Tprsr, 3);//ok
            E3 = -0.214 + 0.908 / Tprsr - 0.967 / Math.Pow(Tprsr, 2);//ok
            H0 = 24.96 - 20.3 * Tprsr + 4.57 * Math.Pow(Tprsr, 2);//ok
            H1 = 5.66 - 19.92 / Tprsr + 16.89 / Math.Pow(Tprsr, 2);//ok
            H2 = -4.11 + 14.68 / Tprsr - 13.39 / Math.Pow(Tprsr, 2);//ok
            H3 = 0.568 - 2.0 / Tprsr + 1.79 / Math.Pow(Tprsr, 2);//ok
            Cp = R * (E3 * Pprsr * Pprsr * Pprsr + E2 * Pprsr * Pprsr + E1 * Pprsr + E0);//ok
            Di = H0 + H1 * Pprsr + H2 * Math.Pow(Pprsr, 2) + H3 * Math.Pow(Pprsr, 3);
            A1sr = -0.39 + 2.03 / Tprsr - 3.16 / (Tprsr * Tprsr) + 1.09 / (Tprsr * Tprsr * Tprsr);
            A2sr = 0.0423 - 0.1812 / Tprsr + 0.2124 / (Tprsr * Tprsr);
            Zsr = A2sr * Pprsr * Pprsr + A1sr * Pprsr + 1;
            a = 225.5 * ksr * dn / (q * delta * Cp * Math.Pow(10, 6));//ok


        }
        private void CalculateReSVal()
        {
            PkR = Pk;
            TsrR = Tsr;
            PprsrR = 2.0 / 3 * (Pn + Pk * Pk / (Pn + Pk)) / Ppk;

            TprsrR = Tsr / Tpk;//ok
            Psr = 2.0 / 3 * (Pn + Math.Pow(Pk, 2) / (Pn + Pk));

            B1srR = -0.67 + 2.36 / TprsrR - 1.93 / Math.Pow(TprsrR, 2);
            B2srR = 0.8 - 2.89 / TprsrR + 2.65 / Math.Pow(TprsrR, 2);
            B3srR = -0.1 + 0.354 / TprsrR - 0.314 / Math.Pow(TprsrR, 2);
            mu0srR = (1.81 + 5.95 * TprsrR) * Math.Pow(10, -6);
            muSrR = mu0srR * (B3srR * PprsrR * PprsrR * PprsrR + B2srR * PprsrR * PprsrR + B1srR * PprsrR + 1);

            E0R = 4.437 - 1.015 * TprsrR + 0.591 * Math.Pow(TprsrR, 2);
            E1R = 3.29 - 11.37 / TprsrR + 10.9 / Math.Pow(TprsrR, 2);
            E2R = 3.23 - 16.27 / TprsrR + 25.48 / Math.Pow(TprsrR, 2) - 11.81 / Math.Pow(TprsrR, 3);
            E3R = -0.214 + 0.908 / TprsrR - 0.967 / Math.Pow(TprsrR, 2);
            H0R = 24.96 - 20.3 * TprsrR + 4.57 * Math.Pow(TprsrR, 2);
            H1R = 5.66 - 19.92 / TprsrR + 16.89 / Math.Pow(TprsrR, 2);
            H2R = -4.11 + 14.68 / TprsrR - 13.39 / Math.Pow(TprsrR, 2);
            H3R = 0.568 - 2.0 / TprsrR + 1.79 / Math.Pow(TprsrR, 2);

            CpR = R * (E3R * PprsrR * PprsrR * PprsrR + E2R * PprsrR * PprsrR + E1R * PprsrR + E0R);
            DiR = H0R + H1R * PprsrR + H2R * Math.Pow(PprsrR, 2) + H3R * Math.Pow(PprsrR, 3);
            A1srR = -0.39 + 2.03 / TprsrR - 3.16 / (TprsrR * TprsrR) + 1.09 / (TprsrR * TprsrR * TprsrR);
            A2srR = 0.0423 - 0.1812 / TprsrR + 0.2124 / (TprsrR * TprsrR);
            ZsrR = A2srR * PprsrR * PprsrR + A1srR * PprsrR + 1;
            aR = 225.5 * ksr * dn / (q * delta * CpR * Math.Pow(10, 6));//ok
            TkR= Tokr + (Tn - Tokr) * Math.Exp(-aR * L) - DiR * (Math.Pow(Pn, 2) - Math.Pow(PkR, 2)) / (2 * aR * L * 2.0 / 3.0 * (Pn + PkR * PkR / (Pn + PkR))) * (1 - Math.Exp(-aR * L));

        }
        private void CalculateEI()
        {
            EI = E * Math.PI * (Math.Pow(dn/1000, 4) - Math.Pow(dn / 1000 - 2 * delta_d / 1000, 4)) / 64;
        }
        private void CalculateLimitingSoilResistance()
        {

            F = Math.PI * (Math.Pow(dn / 1000, 2) - Math.Pow(dn / 1000 - 2 * delta_d / 1000, 2)) / 4;
            q_mp = Ymp * F;
            q_gaz = 100 * (Math.Pow(dn / 1000 - 2 * delta_d / 1000, 2)) * PkR * n_vr;
            qn_mp = q_mp + q_gaz + q_iz;
            Ch = 0.416 * h / (dn / 1000) - 0.056 * Math.Pow(h / (dn / 1000), 2) + 0.095;
            t_np = ngr * (qn_mp * Math.Tan(phi) + 2 * Ygr * Ch *  Math.PI * Math.Pow(dn / 1000, 2) * Math.Tan(phi) + 0.6 * Math.PI * dn / 1000 * C0);
        }
        private void CalculateEquivalentAxialForce()
        {
            sigma_kc = coeff_n * PkR * (dn / 1000 - 2 * delta_d / 1000) / (2 * delta_d / 1000);
            S = (coeff_a * delta_t * E + 0.2 * sigma_kc) * F;
            Y = Math.Sqrt(Math.PI * dn / 1000 * Cox / (E * F));
            K = S * Y / (t_np * Math.Pow(10, -6));
            u0 = 1 / (2 * E * Math.Pow(10, 6) * F) * (Math.Pow(S * Math.Pow(10, 6), 2) / t_np + t_np / Math.Pow(Y, 2));
            L_t = 3 / Y + S * Math.Pow(10, 6) / t_np;
        }

    }
}

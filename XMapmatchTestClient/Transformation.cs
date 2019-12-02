using System;
using System.Collections;

namespace MGG.Coordinates
{
    public enum CoordinateTypeEnum
    {
        Default = 0, Mercator = 0, GeoDecimal_WGS84 = 1, EuroConform = 2, SuperConform = 2, GeoMinSec = 3, GaussKrueger = 4, UTM = 5, Conform = 6, RasterSmartUnits = 7
        // UTMREF = MGRS
    }

    public enum CoordinateZoneEnum : int
    {
        Default = 32767, UTM_Germany_32 = 32, UTM_Germany_33 = 33, GK_Germany_2 = 2, GK_Germany_3 = 3, GK_Germany_4 = 4, GK_Germany_5 = 5, GK_Germany_6 = 6
    }

    public enum CoordinateOverlayEnum : int
    {
        Default = 1, Overlay1 = 1, Overlay2 = 4, Overlay3 = 16, Overlay4 = 64, Overlay5 = 256, Overlay6 = 1024, Overlay7 = 4096
    }

    public class Transformation
    {
        /*
		// Die Werte werden in der Umrechnungsroutine berechnet.
		*/
        //private static double eHayBessel_la0;

        /*
		// Die Werte werden im Klassen-Konstruktor berechnet.
		*/
        private static double gpEuro_n;
        private static double gpEuro_nInv;
        private static double gpEuro_RF;
        private static double gpEuro_rho0;

        //private static double eHayBessel_n;
        //private static double eHayBessel_rhoN2;
        private static double eHayBessel_L;

        private static double eHayBessel_h1;
        private static double eHayBessel_h2;
        private static double eHayBessel_h3;

        /* Funktionen zur Koordinaten-Transformation */

        /* Umwandlung Mercator nach ... */

        public static int Mercator_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Mercator_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Mercator_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Mercator_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Mercator_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung GeoDecimal (WGS84) nach ... */

        public static int GeoDecimal_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoDecimal_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoDecimal_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoDecimal_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoDecimal_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung EuroConform (= SuperConform) nach ... */

        public static int EuroConform_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int EuroConform_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = EuroConform_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = EuroConform_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int EuroConform_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 3400000;
                yArg = yArg - 1700000;
            }

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung GeoMinSec nach ... */

        public static int GeoMinSec_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GeoMinSec_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GeoMinSec_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GeoMinSec_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GeoMinSec_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                xArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 100;
                tmp = (double)(int)min;
                sec = (min - tmp) * 100;
                min = tmp;
                yArg = (deg * 100000 + (min * 60 + sec) / 0.036) * vz;
            }

            {
                xArg = xArg / 100000;
                yArg = yArg / 100000;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung GaussKrueger nach ... */

        public static int GaussKrueger_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int GaussKrueger_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = GaussKrueger_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = GaussKrueger_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int GaussKrueger_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                xArg = xArg - 500000;
            }

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = zArg * 3;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung UTM nach ... */

        public static int UTM_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int UTM_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = UTM_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = UTM_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int UTM_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                int zArg = zIn;
                double lambda, phi;
                double NF, m0NF, m0NF2, VF, VF2, T, TT, G, eta2, phiF, phiStrF, phiGF, phiStrGF, sinPhiF, cosPhiF, cosPhiF2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                G = yArg / 1.0;
                phiStrGF = G / eHayBessel_L;
                phiStrF = (Math.PI / 180) * phiStrGF;
                phiGF = phiStrGF + eHayBessel_h1 * Math.Sin(2 * phiStrF) + eHayBessel_h2 * Math.Sin(4 * phiStrF) + eHayBessel_h3 * Math.Sin(6 * phiStrF);
                phiF = (Math.PI / 180) * phiGF;
                sinPhiF = Math.Sin(phiF);
                cosPhiF = Math.Cos(phiF);
                cosPhiF2 = cosPhiF * cosPhiF;
                VF = Math.Sqrt(1 + 0.00671922 * cosPhiF2);
                NF = 6398786.849 / VF;
                T = sinPhiF / cosPhiF;
                TT = T * T;
                eta2 = 0.00671922 * cosPhiF2;
                m0NF = 1.0 * NF;
                m0NF2 = xArg / m0NF;
                m0NF2 = m0NF2 * m0NF2;
                VF2 = VF * VF;
                phi = (180 / Math.PI) * (phiF - 0.5 * T * m0NF2 * (VF2 - (5 + 3 * TT + 6 * eta2 * (1 - TT)) / 12 * m0NF2));
                lambda = eHayBessel_la0 + (180 / Math.PI) * ((xArg / (m0NF * cosPhiF)) * (1 - m0NF2 / 6 * (VF2 + 2 * TT - 0.05 * (5 + TT * (28 + 24 * TT)) * m0NF2)));
                xArg = lambda;
                yArg = phi;
                zIn = zArg;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung Conform nach ... */

        public static int Conform_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int Conform_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = Conform_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_RasterSmartUnits(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = Conform_2_RasterSmartUnits((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int Conform_2_RasterSmartUnits(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;

            {
                double lambda, phi;
                double teta, rho, h;
                double x, y;
                x = (xArg - 1800000) / 6365000;
                y = (yArg + 500000) / 6365000;
                h = gpEuro_rho0 - y;
                teta = Math.Atan(x / h);
                rho = Math.Sqrt(x * x + h * h);
                if (gpEuro_n < 0)
                    rho = -rho;
                lambda = (180 / Math.PI) * (teta / gpEuro_n + 0.174532925);
                phi = (180 / Math.PI) * (2 * Math.Atan(Math.Pow(gpEuro_RF / rho, gpEuro_nInv)) - (Math.PI / 2));
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                lambda = xArg;
                phi = yArg;
                xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
                yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));
            }
            {
                xArg = xArg + 20015087;
                yArg = yArg + 20015087;
            }
            {
                int zArg = zOut;
                xArg = xArg / 0.4809543 / zArg;
                yArg = yArg / 0.4809543 / zArg;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Umwandlung RasterSmartUnits nach ... */

        public static int RasterSmartUnits_2_Mercator(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_Mercator(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_Mercator((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_Mercator(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 4.809543 * zArg;
                yArg = yArg * 4.809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GeoDecimal(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_GeoDecimal((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GeoDecimal(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_EuroConform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_EuroConform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_EuroConform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            {
                xArg = xArg + 3400000;
                yArg = yArg + 1700000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GeoMinSec(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_GeoMinSec((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GeoMinSec(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                xArg = xArg * 100000;
                yArg = yArg * 100000;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (xArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    xArg = -xArg;
                }
                deg = (double)(int)(xArg / 100000);
                min = ((xArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                xArg = (deg * 100000 + min * 1000 + sec) * vz;
            }
            {
                double deg, min, sec, tmp;
                int vz;
                if (yArg >= 0)
                {
                    vz = 1;
                }
                else
                {
                    vz = -1;
                    yArg = -yArg;
                }
                deg = (double)(int)(yArg / 100000);
                min = ((yArg / 100000) - deg) * 60;
                tmp = (double)(int)min;
                sec = (min - tmp) * 600;
                min = tmp;
                yArg = (deg * 100000 + min * 1000 + sec) * vz;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GaussKrueger(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_GaussKrueger((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_GaussKrueger(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)(lambda / 3 + 0.5);
                eHayBessel_la0 = zArg * 3;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            {
                xArg = xArg + 500000;
            }
            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_UTM(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_UTM(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_UTM((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_UTM(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                int zArg = zOut;
                double lambda, phi;
                double h, V, N, T, TT, dLa, eta2, B, sinPhi, cosPhi, cosPhi2;
                double eHayBessel_la0;
                lambda = xArg;
                phi = yArg;
                if (zArg == 32767)
                    zArg = (int)((lambda + 180) / 6) + 1;
                eHayBessel_la0 = 3 + (zArg - 31) * 6;
                phi = (Math.PI / 180) * phi;
                sinPhi = Math.Sin(phi);
                cosPhi = Math.Cos(phi);
                cosPhi2 = cosPhi * cosPhi;
                eta2 = 0.00671922 * cosPhi2;
                V = Math.Sqrt(1 + eta2);
                N = 6398786.849 / V;
                T = sinPhi / cosPhi;
                TT = T * T;
                dLa = (Math.PI / 180) * (lambda - eHayBessel_la0);
                B = 6366742.521 * phi + sinPhi * cosPhi * (-32044.3278 + cosPhi2 * (134.5392 + cosPhi2 * (-0.7031 + cosPhi2 * 0.0040)));
                h = cosPhi2 * dLa * dLa;
                yArg = 1.0 * (B + 0.5 * N * T * h * (1 + ((5 + 9 * eta2 - TT) / 12.0) * h));
                xArg = 1.0 * (N * cosPhi * dLa * (1 + h / 6 * (V * V - TT + ((5 - 18 * TT + TT * TT) / 20.0) * h)));
                /* xArg = xArg + (double) zArg * 1000000; */
                zOut = zArg;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        public static int RasterSmartUnits_2_Conform(long xIn, long yIn, out long xOut, out long yOut)
        {
            int zIn = 0, zOut = 0;
            double xRes, yRes;
            int result = RasterSmartUnits_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_Conform(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            double xRes, yRes;
            int result = RasterSmartUnits_2_Conform((double)xIn, (double)yIn, out xRes, out yRes, ref zIn, ref zOut);
            xOut = (long)Math.Round(xRes);
            yOut = (long)Math.Round(yRes);
            return result;
        }

        public static int RasterSmartUnits_2_Conform(double xIn, double yIn, out double xOut, out double yOut, ref int zIn, ref int zOut)
        {
            int result = 0;
            double xArg = xIn, yArg = yIn;
            {
                int zArg = zIn;
                xArg = xArg * 0.4809543 * zArg;
                yArg = yArg * 0.4809543 * zArg;
            }
            {
                xArg = xArg - 20015087;
                yArg = yArg - 20015087;
            }
            {
                double lambda, phi;
                lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
                phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
                xArg = lambda;
                yArg = phi;
            }
            {
                double lambda, phi;
                double teta, rho;
                lambda = xArg;
                phi = yArg;
                lambda = (Math.PI / 180) * lambda;
                phi = (Math.PI / 180) * phi;
                teta = gpEuro_n * (lambda - 0.174532925);
                rho = gpEuro_RF / Math.Pow(Math.Tan((Math.PI / 4) + phi / 2), gpEuro_n);
                xArg = (rho * Math.Sin(teta)) * 6365000 + 1800000;
                yArg = (gpEuro_rho0 - rho * Math.Cos(teta)) * 6365000 - 500000;
            }

            xOut = xArg;
            yOut = yArg;
            return result;
        }

        /* Wrapper der Koordinaten-Transformation */

        /*
		// Liste der Funktionen
		*/
        private static ArrayList m_listTransformation = new ArrayList();

        /*
		// Anzahl der unterschiedlichen Koordinaten-Formate
		*/
        private static int m_nStep;

        /*
		// Konstruktor
		*/

        static Transformation()
        {
            gpEuro_n = (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2)));
            gpEuro_nInv = (1 / (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2))));
            gpEuro_RF = (1.0 * Math.Cos(0.785398163) * Math.Pow(Math.Tan((Math.PI / 4) + 0.785398163 / 2), (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2)))) / (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2))));
            gpEuro_rho0 = ((1.0 * Math.Cos(0.785398163) * Math.Pow(Math.Tan((Math.PI / 4) + 0.785398163 / 2), (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2)))) / (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2)))) / Math.Pow(Math.Tan((Math.PI / 4) + 0.523598776 / 2), (Math.Log(Math.Cos(0.785398163) / Math.Cos(0.959931089)) / Math.Log(Math.Tan((Math.PI / 4) + 0.959931089 / 2) / Math.Tan((Math.PI / 4) + 0.785398163 / 2)))));

            //eHayBessel_la0		= 0.0;
            //eHayBessel_rhoN2	= ((180 / Math.PI) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0));
            eHayBessel_L = (((Math.PI / 180) * 6377397.0) * (1 / (1 + (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0))) * (1 + (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) / 4 + (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) / 64));
            eHayBessel_h1 = (((180 / Math.PI) * 3 / 2) * ((6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) - 9 * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) / 16));
            eHayBessel_h2 = ((21 / 16.0) * ((180 / Math.PI) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0)));
            eHayBessel_h3 = ((151 / 96.0) * ((180 / Math.PI) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0)) * (6377397.0 - 6356079.0) / (6377397.0 + 6356079.0));

            m_nStep = (int)(CoordinateTypeEnum)Enum.GetValues(typeof(CoordinateTypeEnum)).GetValue(Enum.GetValues(typeof(CoordinateTypeEnum)).Length - 1) + 1;

            // Mercator
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(Mercator_2_RasterSmartUnits));

            // GeoDecimal_WGS84
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(GeoDecimal_2_RasterSmartUnits));

            // EuroConform = SuperConform
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(EuroConform_2_RasterSmartUnits));

            // GeoMinSec
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(GeoMinSec_2_RasterSmartUnits));

            // GaussKrueger
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(GaussKrueger_2_RasterSmartUnits));

            // UTM
            m_listTransformation.Add(new TransformationDelegate(UTM_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(UTM_2_RasterSmartUnits));

            // Conform
            m_listTransformation.Add(new TransformationDelegate(Conform_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
            m_listTransformation.Add(new TransformationDelegate(Conform_2_RasterSmartUnits));

            // RasterSmartUnits
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_Mercator));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_GeoDecimal));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_EuroConform));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_GeoMinSec));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_GaussKrueger));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_UTM));
            m_listTransformation.Add(new TransformationDelegate(RasterSmartUnits_2_Conform));
            m_listTransformation.Add(new TransformationDelegate(Dummy));
        }

        public static int Dummy(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            xOut = xIn; yOut = yIn; return 0;
        }

        private delegate int TransformationDelegate(long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut);

        public static int Transform(CoordinateTypeEnum nTypeIn, CoordinateTypeEnum nTypeOut, long xIn, long yIn, out long xOut, out long yOut, ref int zIn, ref int zOut)
        {
            xOut = 0;
            yOut = 0;

            int result = 0;
            int nIndex = (int)nTypeOut + m_nStep * (int)nTypeIn;
            if (nIndex < m_listTransformation.Count)
            {
                result = ((TransformationDelegate)m_listTransformation[nIndex])(xIn, yIn, out xOut, out yOut, ref zIn, ref zOut);
            }

            return result;
        }

        /*
                [System.Runtime.InteropServices.DllImport("UtmRef.dll", CharSet=System.Runtime.InteropServices.CharSet.Ansi, CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
                public static extern int fnUtmRef();

                [System.Runtime.InteropServices.DllImport("UtmRef.dll", CharSet=System.Runtime.InteropServices.CharSet.Ansi, CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
                public static extern int Set_MGRS_Parameters(
                    double a,
                    double f,
                    [System.Runtime.InteropServices.In] string Ellipsoid_Code);

                [System.Runtime.InteropServices.DllImport("UtmRef.dll", CharSet=System.Runtime.InteropServices.CharSet.Ansi, CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
                public static extern int Convert_Geodetic_To_MGRS(
                    double Latitude,
                    double Longitude,
                    int    Precision,
                    System.Text.StringBuilder MGRS_String);

                [System.Runtime.InteropServices.DllImport("UtmRef.dll", CharSet=System.Runtime.InteropServices.CharSet.Ansi, CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
                public static extern int Convert_MGRS_To_Geodetic(
                    string MGRS_String,
                    out double Latitude,
                    out double Longitude);
        */
    }
}
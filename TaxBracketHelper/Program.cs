using System.Collections;
using TaxBracketHelper.classes;

Console.WriteLine("2023 Tax Burden (Income = $96,000, Filing = Single): $" + new TaxHelper(96000, TaxHelper.FilingStatus.SINGLE).taxBurden);
Console.WriteLine("2023 Tax Burden (Income = $96,000, Filing = Married Jointly): $" + new TaxHelper(96000, TaxHelper.FilingStatus.MARRIEDFILINGJOINTLY).taxBurden);

namespace TaxBracketHelper.classes
{
    internal class TaxHelper
    {
        //props
        public double income;
        public double taxBurden;
        ArrayList TaxBrackets = new ArrayList();

        public FilingStatus status;
        public enum FilingStatus
        {
            SINGLE,
            MARRIEDFILINGJOINTLY
        }

        public class TaxBracket
        {
            public double taxRate;
            public double bracketStart;
            public double bracketEnd;

            public TaxBracket(double TaxRate, double BracketStart, double BracketEnd)
            {
                taxRate = TaxRate;
                bracketStart = BracketStart;
                bracketEnd = BracketEnd;
            }
        }

        //classes
        internal TaxHelper(double Income, FilingStatus Status)
        {
            income = Income;
            status = Status;

            switch (Status)
            {
                case FilingStatus.SINGLE:
                    {
                        //2023 Filing Single Tax Brackets
                        //10 %     $0  $11,000
                        //12 %     $11,001     $44,725
                        //22 %     $44,726     $95,375
                        //24 %     $95,376     $182,100
                        //32 %     $182,101    $231,250
                        //35 %     $231,251    $578,125
                        //37 %     $578,126    And up
                        TaxBrackets.Add(new TaxBracket(.10, 0, 11000));
                        TaxBrackets.Add(new TaxBracket(.12, 11001, 44725));
                        TaxBrackets.Add(new TaxBracket(.22, 44726, 95375));
                        TaxBrackets.Add(new TaxBracket(.24, 95376, 182100));
                        TaxBrackets.Add(new TaxBracket(.32, 182101, 231250));
                        TaxBrackets.Add(new TaxBracket(.35, 231251, 578125));
                        TaxBrackets.Add(new TaxBracket(.37, 578126, -1));

                        break;
                    }
                case FilingStatus.MARRIEDFILINGJOINTLY:
                    {
                        //2024 Filing Single Tax Brackets
                        //10 %     $0  $22,000
                        //12 %     $22,001     $89,450
                        //22 %     $89,451     $190,750
                        //24 %     $190,751    $364,200
                        //32 %     $364,201    $462,500
                        //35 %     $462,501    $693,750
                        //37 %     $693,751    And up
                        TaxBrackets.Add(new TaxBracket(.10, 0, 22000));
                        TaxBrackets.Add(new TaxBracket(.12, 22001, 89450));
                        TaxBrackets.Add(new TaxBracket(.22, 89451, 190750));
                        TaxBrackets.Add(new TaxBracket(.24, 190751, 364200));
                        TaxBrackets.Add(new TaxBracket(.32, 364201, 462500));
                        TaxBrackets.Add(new TaxBracket(.35, 462501, 693750));
                        TaxBrackets.Add(new TaxBracket(.37, 693751, -1));

                        break;
                    }
            }

            taxBurden = Math.Ceiling(CalculateTaxBurden());
        }

        //methods
        private double CalculateTaxBurden()
        {
            double calculatedBurden = 0;

            foreach (TaxBracket taxBracket in TaxBrackets)
            {
                if (income > taxBracket.bracketEnd)
                {
                    //Calculate full tax burden for this bracket
                    calculatedBurden += taxBracket.taxRate * (taxBracket.bracketEnd - taxBracket.bracketStart);
                }
                else
                {
                    //Calculate partial tax burden for this tax bracket.
                    calculatedBurden += taxBracket.taxRate * (income - taxBracket.bracketStart);
                    break;
                }
            }


            return calculatedBurden;

        }

    }
}


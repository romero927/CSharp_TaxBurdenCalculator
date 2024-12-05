# CSharp_TaxBurdenCalculator

Federal Tax Brackets for 2023: https://www.irs.gov/filing/federal-income-tax-rates-and-brackets

Federal Tax Brackets Calculator (for testing): https://goodcalculators.com/us-tax-brackets-calculator/

Goal: Build a library that allows a user to input Income and Filing Status (currently supports SINGLE and MARRIED FILING JOINTLY brackets for 2023) and receive estimated Tax Burden.

Code Here: https://github.com/romero927/CSharp_TaxBurdenCalculator/blob/main/TaxBracketHelper/Program.cs

Explanation of Algorithm:

1. Loop through all tax Brackets
2. For Each Bracket:
  If income is greater than the end of the current bracket, calculate full estimated tax burden for that bracket (taxBracket.taxRate * (taxBracket.bracketEnd - taxBracket.bracketStart)) and add to calculatedBurden
  If income is less than or equal to the end of the current bracket, calculate partial estimated tax burden for that bracket (taxBracket.taxRate * (income - taxBracket.bracketStart)) and add to calculatedBurden. Break loop  as we have no income in higher tax brackets.

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



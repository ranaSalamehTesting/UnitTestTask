﻿using System;
using System.Linq;

namespace StringCalculator.Core.Processor
{
    public class StringCalculatorProcessor
    {
        public StringCalculatorProcessor()
        {
        }

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var separator = FindSeparator(ref numbers);

            string[] numbersArray = numbers.Split(separator, '\n');

            CheckForNegativeNumbers(numbersArray);

            var sum = numbersArray
                .Select(numberString =>
                {
                    int.TryParse(numberString, out var value);
                    return value;
                })
                .Where(value => value <= 1000)
                .Sum();

            return sum;
        }

        private void CheckForNegativeNumbers(string[] numbersArray)
        {
            var negativeNumbers = numbersArray.Where(c => int.Parse(c) < 0);
            if (negativeNumbers.Any())
            {
                throw new Exception("negatives not allowed: " + string.Join(",", negativeNumbers));
            }
        }

        private char FindSeparator(ref string numbers)
        {
            var separator = ',';

            if (numbers.StartsWith("//"))
            {
                separator = numbers[2];
                numbers = numbers.Substring(numbers.IndexOf('\n') + 1);
            }

            return separator;
        }
    }
}
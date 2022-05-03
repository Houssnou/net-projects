namespace unit_testing_00
{
    public class Calculator
    {
        public List<int> NumbersRange = new();

        public int Add(int a, int b)
        {
            return a + b;
        }
        public double Add(double a, double b)
        {
            return a + b;
        }

        public bool IsOddNumber(int i)
        {
            return i % 2 != 0;
        }

        public List<int> GetOddRange(int min, int max)
        {
            NumbersRange.Clear();

            for (int i = min; i <= max; i++)
            {

                if (i % 2 != 0)
                {
                    NumbersRange.Add(i);
                }
            }
            return NumbersRange;
        }
    }
}
using ExtensibleAPI;
namespace InversePythagorean {
    public class InversePythagorean:IOperation  {
        public string Name { get; init; }
        public string[] ArgumentNames { get; init; }
        public double[] Arguments { get; }

        public double Calculate(params List<double> arguments) {
            if (arguments.Count != 3) {
                throw new ArgumentException("Inverse Pythagorean operation requires exactly 3 arguments.");
            }

            // Find the index of  greatest of the three arguments
            int greatestIndex = 0;
            for (int i = 1; i < arguments.Count; i++) {
                if (arguments[i] > arguments[greatestIndex]) {
                    greatestIndex = i;
                }
            }

            // Place the greatest in argument c and the rest in a and b
            double greatest = arguments[greatestIndex];
            double first = arguments[(greatestIndex + 1) % 3];
            double second = arguments[(greatestIndex + 2) % 3];

            Arguments[0] = first;
            Arguments[1] = second;
            Arguments[2] = greatest;

            // Calculate the inverse Pythagorean theorem:
            // Arguments[0]^2 + Arguments[1]^2 = Arguments[2]^2 and
            // return 1 if the equation holds, otherwise return 0
            double leftSide = Math.Pow(Arguments[0], 2) + Math.Pow(Arguments[1], 2);
            double rightSide = Math.Pow(Arguments[2], 2);
            if (Math.Abs(leftSide - rightSide) < 1e-10) {
                return 1;
            }
            else {
                return 0;
            }
        }

        public string PrintResult() {
            double result = Calculate(Arguments.ToList());
            if (result == 1) {
                return $"The numbers {Arguments[0]}, {Arguments[1]} and {Arguments[2]} satisfy the Pythagorean theorem.";
            } else {
                return $"The numbers {Arguments[0]}, {Arguments[1]} and {Arguments[2]} do not satisfy the Pythagorean theorem.";
            }
        }

        public InversePythagorean() {
            Name = "Inverse Pythagorean";
            ArgumentNames = new string[] { "a", "b", "c" };
            Arguments = new double[3];
        }
    }
}

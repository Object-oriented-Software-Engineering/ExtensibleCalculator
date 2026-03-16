namespace ExtensibleAPI {
    public interface IOperation {
        string Name { get; init; }

        string[] ArgumentNames { get; init; }
        double[] Arguments { get; }

        double Calculate(params List<double> arguments);

        string PrintResult();
    }
}

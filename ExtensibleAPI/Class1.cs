namespace ExtensibleAPI {
    public interface Ioperation {
        string Name { get; init; }

        string[] ArgumentNames { get; init; }

        double Calculate(params List<double> arguments);        
    }
}

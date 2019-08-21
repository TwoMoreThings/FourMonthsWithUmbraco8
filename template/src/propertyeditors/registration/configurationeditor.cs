public class NestedContentConfiguration
{
    [ConfigurationField("minItems", "Min Items", "number",
        Description = "Set the minimum number of items allowed.")]
    public int? MinItems { get; set; }
}

public class NestedContentConfigurationEditor :
    ConfigurationEditor<NestedContentConfiguration>
{
    public override object DefaultConfigurationObject
    { get; } = new NestedContentConfiguration();
}
[DataEditor("nestedContent", "Nested Content", "nestedcontent",
    ValueType = "JSON", Group = "lists", Icon = "icon-thumbnail-list")]
[PropertyEditorAsset(ClientDependencyType.Javascript,
 "/App_Plugins/PathToAsset.js")]
public class NestedContentPropertyEditor : DataEditor
{
    public NestedContentPropertyEditor(ILogger logger)
        : base(logger)
    { }

    protected override IConfigurationEditor
        CreateConfigurationEditor() => new NestedContentConfigurationEditor();

    protected override IDataValueEditor
        CreateValueEditor() => new NestedContentPropertyValueEditor();
}
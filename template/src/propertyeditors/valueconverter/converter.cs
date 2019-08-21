public class OEmbedPickerValueConverter
    : PropertyValueConverterBase
{
    public override bool IsConverter(PublishedPropertyType propertyType) { }

    public override Type GetPropertyValueType(PublishedPropertyType propertyType) { }
    public override object ConvertIntermediateToObject(IPublishedElement owner, PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel,
            object inter,
            bool preview)
    {
        var allowMultipe =
            propertyType.DataType.ConfigurationAs<OEmbedPickerConfiguration>().AllowMultiple;
    }
}
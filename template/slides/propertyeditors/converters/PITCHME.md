---

@title[Property Value Converters]

@snap[north span-100]

### Property Value Converters

@snapend

@snap[midpoint span-100 text-center]

A [Property Value Converter](https://our.umbraco.com/documentation/Extending/Property-Editors/value-converters) converts a property editor's database-stored value to another type. The converted value can be accessed from MVC Razor or any other Published Content API.
<br>
<br>
@css[fragment](For example the standard Umbraco Core "Content Picker" stores a nodeId or UDI as string. The converter returns an IPublishedContent object.)

@snapend

---

@title[C# - What changed]

@snap[midpoint span-100 text-center]

### C&num; - What changed

<table class="text-08">
  <tr>
    <th>V7</th>
    <th>V8</th>
  </tr>
  <tr>
     <td>Inherit PropertyValueConverterBase     
     </td>
    <td>Inherit PropertyValueConverterBase</td>
  </tr>
   <tr>
     <td>Implement IPropertyValueConverterMeta<br>  
     </td>
    <td></td>
  </tr>  
   <tr>
     <td>or use PropertyValueType and PropertyValueCache attributes<br>  
     </td>
    <td>
    </td>
  </tr>  
</table>

@snapend

---?code=template/src/propertyeditors/valueconverter/converter.cs&lang=csharp&color=#fff&title=Property Value Converter

@[1-2](Inherit from PropertyValueConverterBase)
@[4-17](Override methods to change behavior)

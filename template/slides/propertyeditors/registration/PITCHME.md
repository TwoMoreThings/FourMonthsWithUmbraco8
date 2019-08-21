---

@title[Registering property editors]

@snap[north span-100]

### Registering property editors

@snapend

@snap[midpoint span-100 text-center]

@ul

- For simple editors => [package.manifest](https://our.umbraco.com/documentation/Extending/Property-Editors/package-manifest)
- For more advanced editors => C#
- Gives more control of config, data, validation

@ulend

@snapend

---

@title[C# - What changed]

@snap[midpoint span-100 text-center]

### C&num; - What changed

<table class="text-09">
  <tr>
    <th></th>
    <th>V7</th>
    <th>V8</th>
  </tr>
  <tr>
    <td>Prevalues</td>
    <td>PreValueEditor</td>
    <td>ConfigurationEditor&lt;T&gt;</td>
  </tr>
  <tr class="fragment">
    <td>ValueEditor</td>
    <td>PropertyValueEditor</td>
    <td>DataValueEditor</td>
  </tr>  
  <tr class="fragment">
    <td>Editor</td>
    <td>PropertyEditor</td>
    <td>DataEditor</td>
  </tr>     
</table>

@snapend

---?code=template/src/propertyeditors/registration/configurationeditor.cs&lang=csharp&color=#fff&title=Configuration

@[1-6](Configuration class)
@[8-9](Inherit from ConfigurationEditor<T>)
@[8-13](Override methods to change behavior)

---?code=template/src/propertyeditors/registration/valueeditor.cs&lang=csharp&color=#fff&title=Value editor

@[1](Inherit from DataValueEditor)
@[3-9](Override methods to change behavior)

---?code=template/src/propertyeditors/registration/dataeditor.cs&lang=csharp&color=#fff&title=Data editor

@[1-2](Decorate with DataEditor attribute)
@[3-4](Optionally register assets, eg scripts and css)
@[5](Inherit from DataEditor)
@[7-9](Required constructor)
@[11-15](Set configuration and datavalue editor)

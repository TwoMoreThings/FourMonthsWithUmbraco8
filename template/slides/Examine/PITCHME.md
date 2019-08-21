---?color=#ffc927

@title[Examine on Azure]

@snap[midpoint]

@css[text-25](Examine)

@snapend

---

@title[Examine]

@snap[north span-100 text-center]

### Examine

@snapend


@snap[midpoint span-100]

@ul[squares text-08]

- Create new indexes in C#
  - Although this is not recommended by HQ
  - Extending the External Index is recommended
- Events are still used to add custom data to the index
  - but OnGatheringNodeData has been moved
- ExamineSettings.config is gone
  - All settings now done in C#
  - With ~~1~~ 2 exceptions
- The exceptions...
@ulend

---?image=template/img/examine-on-azure.png&position=midpoint&size=50%

@title[Configure for Azure]

@snap[north span-100 text-center]

### Configure for Azure
@snapend



@snap[south span-100]


@ul[squares text-08]

```xml
    <!-- reference for this https://github.com/umbraco/Umbraco-CMS/issues/5035 -->
    <!-- issue merged into 8.1.1
    <add key="Umbraco.Core.LocalTempStorage" value="EnvironmentTemp" />
    <add key="Umbraco.Examine.LuceneDirectoryFactory" value="Examine.LuceneEngine.Directories.SyncTempEnvDirectoryFactory, Examine" />
```
- But what was the issue?
@ulend

---?image=template/img/NuCache-Issue.png&position=midpoint&size=80%
@title[NuCache Issue]

---?color=linear-gradient(80deg, #FFC927 50%, white 50% )

@title[What about OnGatheringNodeData?]

@snap[west span-40 text-center]

## What about OnGatheringNodeData?

@snapend

@snap[east span-45 text-center]

@ul[squares text-08]

  @ulend

---

### - It's now...


@ul[squares text-08]
```csharp
indexProvider.TransformingIndexValues
```
- you need to get the indexProvider
- and you do this inside a Component
- and you register the Component in a Composer
- Confused?
  - Here's an example (which includes Variants)
  @ulend

@snapend

---

@title[Examine Composer]

@snap[north span-100 text-center]

### First the Composer
@snapend



@snap[midpoint span-100]

```csharp
using FourMonthsWithUmbraco8.Code.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace FourMonthsWithUmbraco8.Code.Composers
{
    public class ExamineComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<ExamineComponent>();
        }
    }
}
```

---
@css[text-15]
@title[Examine Component]

@snap[north span-100 text-center]

### Then the Component (pt1)
@snapend



@snap[midpoint span-100]
```csharp
public class ExamineComponent : IComponent
{

    private readonly IExamineManager _examineManager;
    private readonly ILocalizationService _localizationService;

    public ExamineComponent(IExamineManager examineManager, 
        ILocalizationService localizationService)
    {
        _examineManager = examineManager;
        _localizationService = localizationService;
    }
```

---

@title[Examine Component]

@snap[north span-100 text-center]

### Then the Component (pt2)
@snapend



@snap[midpoint span-100]

```csharp
public void Initialize()
{
    if (!_examineManager.TryGetIndex("ExternalIndex", out IIndex index))
        throw new InvalidOperationException("No index found by name ExternalIndex");

    //we need to cast because BaseIndexProvider contains 
    //the TransformingIndexValues event
    if (!(index is BaseIndexProvider indexProvider))
        throw new InvalidOperationException("Index is not type BaseIndexProvider");

    indexProvider.TransformingIndexValues += IndexProviderTransformingIndexValues;
}
```

---

@title[Examine Component]

@snap[north span-100 text-center]

### Then the Component (pt3)
@snapend



@snap[midpoint span-100]

```csharp
private void IndexProviderTransformingIndexValues(object sender, 
    IndexingItemEventArgs e)
{
    //only run on a Content index
    if (e.ValueSet.Category != IndexTypes.Content) return;

    var languages = _localizationService.GetAllLanguages();
    foreach (var language in languages)
    {
        if (e.ValueSet.Values
            .ContainsKey($"myPropertyAlias_{language.IsoCode.ToLower()}"))
        {
            foreach (var value in e.ValueSet
                .Values[$"myPropertyAlias_{language.IsoCode.ToLower()}"])
            {
                //work out what we want to store in the index
                e.ValueSet
                    .TryAdd($"myNewIndexField_{language.IsoCode.ToLower()}"
                    , "myNewValueToBeAddedToThisIndex");
            }
```



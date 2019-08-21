---?color=#ffc927

@title[A Simple Component]

@snap[midpoint]

@css[text-25](A Simple Component)

@snapend

---

@title[A Simple Example Component]

@snap[north text-center span-100]

### A Simple Example Component

@snapend

@snap[midpoint text-center span-100]

@ul
- Set a publication date on Save
- Unless one is already set
- This allows you to always sort on 1 field
@ulend

@snapend



---
@title[BlogArticle Component]

@snap[north text-center span-100]

### BlogArticleComponent Component

@snapend

@snap[midpoint text-center span-100]

```csharp
public class BlogArticleComponent : IComponent
{
    public void Initialize()
    {
        //Umbraco event subscriptions
        ContentService.Saving += ContentService_Saving;
    }

    private void ContentService_Saving(IContentService sender, 
                SaveEventArgs<IContent> e)
    {
        foreach (var content in e.SavedEntities)
        {
            if (content.ContentType.Alias.InvariantEquals("BlogPost") 
                    && content.GetValue("articleDate") == null)
            {
                content.SetValue("articleDate", DateTime.Now);


```
@snapend


---
@title[BlogArticle Composer]

@snap[north text-center span-100]

### BlogArticle Composer

@snapend

@snap[midpoint text-center span-100]

```csharp
public class BlogArticleComposer : IUserComposer
{
    public void Compose(Composition composition)
    {
        composition.Components().Append<BlogArticleComponent>();
    }
}
```
@snapend

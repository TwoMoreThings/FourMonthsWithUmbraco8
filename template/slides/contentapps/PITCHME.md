---?color=#26ffc9

@title[Content Apps]

@snap[midpoint]

@css[text-25](Content Apps)

@snapend

---?image=template/img/apps.jpg&position=right&size=auto 100%

@title[What are Content apps]

@snap[west span-60 text-center]

### [Content apps](https://our.umbraco.com/documentation/Extending/Content-Apps/)

@ul
- Not a app that edits content
- It's a "companion" for a content item
- For non umbraco "content"
- Limitable by content type, role
- Registered in package manifest or C# content app factory
@ulend

@snapend

---

@title[V8 - Package manifest]

@snap[north span-100 text-center]

### Package manifest


@snapend


@snap[midpoint span-100]

```json
  {
    "contentApps": [
      {
        "name": "Incoming links", 
        "alias": "nexulinks", 
        "weight": 0,
        "icon": "icon-nexu", 
        "view": "PathToView", 
        "show": [ 
                "+content/*", 
                "+media/*" 
            ]
      }
    ]
}
```

---

@title[C# - Content app factory]

@snap[north span-100 text-center]

### C&num; Content app factory

@snapend


@snap[midpoint span-100]

```csharp
 public class IncomingLinksApp : IContentAppFactory
    {
        public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
              var app = new ContentApp
            {
                Alias = "nexulinks",
                Name = "Incoming links"
                Icon = "icon-nexu",
                View = "PathToView",
                Weight = 0
            };
            return app;
        }
    }
```

---

@title[C# - Register content app]

@snap[north span-100 text-center]

### C&num; Register content app

@snapend


@snap[midpoint span-100]

```csharp
[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    internal class NexuComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {          
            composition.ContentApps().Append<IncomingLinksApp>();
        }
    }
```



---?color=#ffc927

@title[Quick Tips]

@snap[midpoint]

@css[text-25](Quick Tips)

@snapend

---

@title[dictionary items are not cached]

@snap[north span-100 text-center]

### Dictionary items are not cached

@snapend

@snap[midpoint span-100]

@ul[squares text-08]

- You can see this via the MiniProfiler
- Which you can turn on by default

@ulend

---

@title[Can't 'Invite' new Users?]

@snap[north span-100 text-center]

### Can't 'Invite' new Users?

@snapend

@snap[midpoint span-100]

@ul[squares text-08]

- Do you have 'Add Users' option, but missing the dropdown so you can 'Invite Users'
- It's because you haven't setup your SMTP settings
- Specifically the sender address is your@email.com

@ulend


---

@title[Register your own services to use via DI]

@snap[north span-100 text-center]

### Register your own services to use via DI

@snapend

@snap[midpoint span-100]

```csharp
public void Compose(Composition composition)
{
    composition.Register<IMyServiceInterface, MyService>();
}
```

---

@title[Things You'll Get Used To]

@snap[north span-100 text-center]

### Things You'll Get Used To

@snapend

@snap[midpoint span-100]

@ul
- Umbraco failed to boot YSOD
- Recycling your App Pool (if you run from IIS) to run new composers
@ulend


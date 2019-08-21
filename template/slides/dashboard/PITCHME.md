---?color=#ffc927

@title[Dashboards]

@snap[midpoint]

@css[text-25](Dashboards)

@snapend

---?image=template/img/default-content-dashboard.png&position=right&size=40%

@title[Removing dashboards]

@snap[west span-70]


@snap[midpoint span-100]

@ul[squares text-08]

### "Marketing" dashboards

- Removing dashboards

- It's only possible in C&num;

@ulend

@snapend

@snap[east span-25]

@snapend



---

@title[Removing dashboards]

@snap[north span-100 text-center]

### Removing dashboards
@snapend



@snap[midpoint span-100]

```csharp
    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Web;
    using Umbraco.Web.Dashboards;

  namespace FourMonthsWithUmbraco8.Composers
  {
      [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
      public class RemoverDefaultDashboards : IUserComposer
      {
          public void Compose(Composition composition)
          {
              composition.Dashboards().Remove<MembersDashboard>();
              composition.Dashboards().Remove<ContentDashboard>();
              composition.Dashboards().Remove<SettingsDashboard>();
          }
      }
  }
```
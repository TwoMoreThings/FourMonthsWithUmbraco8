---?color=#ffc927

@title[CORS]

@snap[midpoint]

@css[text-25](CORS)

@snapend

---

@title[CORS]

@snap[north text-center span-100]

### Configuring Cross Origin Resource Sharing

@snapend

@snap[midpoint text-center span-100]

@ul
- Install the nuget package
- Add your Component (and Composer)
- Attribute your endpoint

```csharp
install-package Microsoft.AspNet.WebApi.Cors
```

@ulend

@snapend



---
@title[CORS Component]

@snap[north text-center span-100]

### CORS Component

@snapend

@snap[midpoint text-center span-100]

```csharp
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace FourMonthsWithUmbraco8.Code.Components
{
    public class EnableCorsComponent : IComponent
    {
        public void Initialize()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.EnableCors();
        }

        public void Terminate()
        {
        }
    }
}

```
@snapend


---
@title[CORS Composer]

@snap[north text-center span-100]

### CORS Composer

@snapend

@snap[midpoint text-center span-100]

```csharp
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace FourMonthsWithUmbraco8.Code.Composers
{
    public class ComposeEnableCorsComponentComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Insert<EnableCorsComponent>();
        }
    }

}

```
@snapend



---
@title[CORS Attributes]

@snap[north text-center span-100]

### CORS Attributes

@snapend

@snap[midpoint text-center span-100]

```csharp
//restrict to the minimum headers and methods!
[EnableCors(origins: "yourdomian.com", headers: "*", methods: "*")]
public class MyApiController : UmbracoApiController
{

    //Your methods as normal
}
```

@snapend

---
@title[CORS Options]

@snap[north text-center span-100]

### Handling CORS Options

@snapend

@snap[midpoint text-center span-100]

@ul
- You then either...
    - Handle the Http OPTIONS verbs
    - or create an attribute based route
        - A RoutePrefix attribute on the class
        - A Route attribute on the method
@ulend


@snapend


---
@title[CORS Options]

@snap[north text-center span-100]

### Handling CORS 'OPTIONS'

@snapend

@snap[midpoint text-center span-100]

```csharp
[System.Web.Mvc.AcceptVerbs("OPTIONS")]
[HttpGet]
[HttpPut]
[Route("")]
public HttpResponseMessage Put()
{
    var resp = new HttpResponseMessage(HttpStatusCode.OK);
    resp.Headers.Add("Access-Control-Allow-Origin", "*");
    resp.Headers.Add("Access-Control-Allow-Methods", "GET,PUT");//,OPTIONS

    return resp;
}
```

@snapend






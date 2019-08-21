---?color=#ffc927

@title[Logging]

@snap[midpoint]

@css[text-25](Logging)

@snapend

---

@title[Logging]

@snap[north span-100 text-center]

### Configuring Logging

@snapend


@snap[midpoint span-100]


@ul[squares text-08]

- Serilog can be configured and extended by using the two XML configuration files
    - /config/serilog.config 
        - the main Umbraco logging pipeline
    - /config/serilog.user.config
        - modifies user logging without affecting the main Umbraco logger.
- You can also configure for specific namespaces
    - As shown above
```xml
//VALID Values: Verbose, Debug, Information, Warning, Error, Fatal
<add key="serilog:minimum-level" value="Information" />

<add key="serilog:minimum-level:override:Microsoft" value="Warning" />
<add key="serilog:minimum-level:override:Microsoft.AspNet.Mvc" value="Error" />
<add key="serilog:minimum-level:override:MyNamespace" value="Information" />
```
@ulend

---

@title[You can configure Logging in C#]

@snap[north span-100 text-center]

### You can configure Logging in C#
@snapend



@snap[midpoint span-100]


@ul[squares text-08]
- This isn't done in a Component
- Logging needs configured before Composers
    - It has to be, in order to log Composers and Components initialisation
- But the documentation was wrong
- It set a level
- Then read from the config files which overwrote the level setting
- Even if you removed the level option from those files
    - or tried changing the order
    - it still didn't work
- But that wouldn't solve my problem...

@ulend

---
@title[How to set different logging levels per environment?]

@snap[north span-100]

### How to set different logging levels per environment?

@snapend

@snap[midpoint span-100]

@ul[squares text-08]
- Create a class which inherits from UmbracoApplication
- Change Global.asax to inherit from this class
- Read the level you want from web.config
    - Allowing web.config transforms to be used
- Set the level the correct way
    - `.MinimumLevel.ControlledBy(loggingLevelSwitch)`
- overrride GetRuntime() method
- and the GetLogger() method
  @ulend


---

@title[UmbracoApplication Class]

@snap[north span-100 text-center]

### UmbracoApplication Class (pt1)
@snapend



@snap[midpoint span-100]

```csharp
public class ConfigureUmbracoApplication : UmbracoApplication
{

    protected override IRuntime GetRuntime()
    {
        return new SetMinimumLoggingLevelRuntime(this);
    }

}

public class SetMinimumLoggingLevelRuntime : WebRuntime
{
    public SetMinimumLoggingLevelRuntime(UmbracoApplicationBase umbracoApplication) : base(umbracoApplication)
    {
    }
...

```

---

@title[Set Minimun Logging Level]

@snap[north span-100 text-center]

### Set Minimun Logging Level (pt2)
@snapend



@snap[midpoint span-100]

```csharp
protected override Umbraco.Core.Logging.ILogger GetLogger()
{
    //<add key="myMinimumLoggingLevel" value="Debug" />
    var logLevelSetting = ConfigurationManager.AppSettings["myMinimumLoggingLevel"];
    const bool ignoreCase = true;
    if (!Enum.TryParse(logLevelSetting, ignoreCase, out LogEventLevel level))
    {
        level = LogEventLevel.Information;
    }

    var levelSwitch = new LoggingLevelSwitch { MinimumLevel = level };

    var loggerConfig = new LoggerConfiguration()
        .MinimalConfiguration()
        .ReadFromConfigFile()
        .ReadFromUserConfigFile()
        .MinimumLevel.ControlledBy(levelSwitch);

    return new SerilogLogger(loggerConfig);
}
```

---

@title[Edit Global.asax]

@snap[north span-100 text-center]

### Edit Global.asax (pt3)
@snapend



@snap[midpoint span-100]

```csharp
<%@ Application Inherits="FourMonthsWithUmbraco8.Code.ConfigureUmbracoApplication" Language="C#" %>
```

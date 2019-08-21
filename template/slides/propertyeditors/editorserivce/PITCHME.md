---

@title[Editor service]

@snap[north span-100]

### Bye bye dialogservice

### Hello editorService

@snapend

@snap[midpoint span-100 tex-center]

@ul

- DialogService deprecated since 7.4, in favor of the [umb-dialog](https://our.umbraco.com/apidocs/v8/ui/#/api/umbraco.directives.directive:umbOverlay) directive
- [EditorService](https://our.umbraco.com/apidocs/v8/ui/#/api/umbraco.services.editorService) is a application-wide service for handling infinite editing introduced in V8
- A lot of built-in dialogues (content, media, etc...)
- Custom views possible

@ulend

@snapend

---?code=template/src/propertyeditors/editorservice/editor.html&lang=html&color=#fff&title=Editor service view example

---?code=template/src/propertyeditors/editorservice/controller.js&lang=javascript&color=#fff&title=Editor service controller example

@[1](Inject the service in your controller)
@[6-17](Set the dialogue options and open dialog)

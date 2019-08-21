---?color=#ffc927

@title[Migrations]

@snap[midpoint]

@css[text-25](Migrations)

@snapend

---

@title[What are migrations]

@snap[north text-center span-100]

### What are migrations

@snapend

@snap[midpoint text-center span-100]

Migrations are used when you want to run setup code during startup

@ul
- Create or modify database table
- Migrate data
- Run on each environment when needed
- State based in v8
@ulend

@snapend

---

@title[Migration]

@snap[north span-100 text-center]

### Migration

@snapend


@snap[midpoint span-100]

```csharp
   public class CreateRelationTableMigration : MigrationBase
    {
        public CreateRelationTableMigration(IMigrationContext context)
            : base(context)
        {
        }

        public override void Migrate()
        {
            this.Create.Table("MyTable")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_Id").NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .Do();
        }
    }
```
@[1](Inherit from migration base)
@[8-15](Add your migration code)

---

@title[Migration plan]

@snap[north span-100 text-center]

### Migration plan

@snapend


@snap[midpoint span-100]

```csharp
   public class NexuMigrationPlan : MigrationPlan
    {
        public NexuMigrationPlan()
            : base("Our.Umbraco.Nexu")
        {
            this.From(this.InitialState)
			.To<CreateRelationTableMigration>("2.0.0-Initial");
        }

        public override string InitialState => string.Empty;

    }
```
@[1](Inherit from migration plan)
@[6-7](Run your migrations depending on state)

---

@title[Migration component]

@snap[north span-100 text-center]

### Migration component

@snapend


@snap[midpoint span-100]

```csharp
   public class MigrationComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly ILogger logger;

        public MigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            // set dependencies
        }

        public void Initialize()
        {
            var upgrader = new Upgrader(new NexuMigrationPlan());

            upgrader.Execute(this.scopeProvider, this.migrationBuilder, this.keyValueService, this.logger);
        }
    }
```

---

@title[The Devil is in the Detail]

@snap[north span-100 text-center]

### The Devil is in the Detail

@snapend

@snap[midpoint span-100 text-center]

@ul[](true)

- NTEXT and NCHAR columns are special cases
  - Not covered by plain NPoco
- There are some specific NPoco attributes that might be useful
- Don't go looking for the 'Migrations' table
    - It's now called UmbracoKeyValues
    - It's not well documented

  @ulend

@snapend

---

@title[NTEXT and NCHAR columns]

@snap[north span-100 text-center]

### NTEXT and NCHAR columns

@snapend

@snap[midpoint span-100 text-center]

```csharp
public override void Migrate()
{
    var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();
    
    if (tables.InvariantContains(TableName))
    {
        var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

        var textType = SqlSyntax.GetSpecialDbType(SpecialDbTypes.NTEXT);

        if (columns.Any(x => x.TableName.InvariantEquals(TableName)
                                && x.ColumnName.InvariantEquals(ColumnName)) == false)
        {
            Create.Column(ColumnName)
                .OnTable(TableName)
                .AsCustom(textType)
                .Nullable().Do();
        }
    }
}

```
@snapend


---

@title[Some NPoco Attributes]

@snap[north span-100 text-center]

### Some NPoco Attributes

@snapend

@snap[midpoint span-100 text-center]

```csharp
using System;
using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;

namespace FourMonthsWithUmbraco8.Code.CustomTables
{
    [TableName("TM_TestTable")]
    [PrimaryKey("TestId")]
    public class TestTable
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int TestId { get; set; }
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        [ComputedColumn(ComputedColumnType.Always, Name = "DateCreated")]
        public DateTime DateCreated { get; set; }

        //Other props here
    }
}

```
@snapend




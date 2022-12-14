using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations;

[NopMigration("2022/12/12 12:00:00:2551770", "Add Author Column", UpdateMigrationType.Data, MigrationProcessType.Update)]
public class AddAuthorProperty : AutoReversingMigration
{
    public override void Up()
    {
        Create.Column(nameof(Product.Author))
            .OnTable(nameof(Product))
            .AsString(255)
            .Nullable();
    }
}
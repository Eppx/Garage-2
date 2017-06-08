namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garages", "Parkerad", c => c.DateTime(nullable: false));
            AddColumn("dbo.Garages", "TotalParkedTime", c => c.Int(nullable: false));
            AddColumn("dbo.Garages", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.Garages", "RegNr", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.Garages", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.Garages", "Brand", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Garages", "Brand", c => c.String());
            AlterColumn("dbo.Garages", "Color", c => c.String());
            AlterColumn("dbo.Garages", "RegNr", c => c.String(maxLength: 6));
            DropColumn("dbo.Garages", "Price");
            DropColumn("dbo.Garages", "TotalParkedTime");
            DropColumn("dbo.Garages", "Parkerad");
        }
    }
}

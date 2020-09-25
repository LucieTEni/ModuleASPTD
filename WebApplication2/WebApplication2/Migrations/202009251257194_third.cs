namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samourais", "Potentiel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Samourais", "Potentiel");
        }
    }
}

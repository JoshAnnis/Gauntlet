namespace Gauntlet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stillworkingonvideos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserVideo", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "UserPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoto", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "UserVideo");
        }
    }
}

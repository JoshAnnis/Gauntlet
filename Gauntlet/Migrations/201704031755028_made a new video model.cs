namespace Gauntlet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeanewvideomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Viedos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FromUserId = c.String(),
                        UserVideo = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Viedos");
        }
    }
}

namespace MediaLibrary.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DatabasePublicIdentifier : DbMigration
    {
        public override void Up()
        {
            AddColumn("Databases", "PublicIdentifier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Databases", "PublicIdentifier");
        }
    }
}

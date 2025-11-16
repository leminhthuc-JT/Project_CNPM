namespace G6_Website_BQA.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}

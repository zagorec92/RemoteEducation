namespace RemoteEducation.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Score = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        Question_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.Question_ID)
                .Index(t => t.Question_ID);
            
            CreateTable(
                "dbo.ApplicationLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserDetailsID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailsID, cascadeDelete: true)
                .Index(t => t.UserDetailsID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        Email = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScoreLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Role_ID })
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.Role_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScoreLogs", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "UserDetailsID", "dbo.UserDetails");
            DropForeignKey("dbo.UserRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Answers", "Question_ID", "dbo.Questions");
            DropIndex("dbo.UserRoles", new[] { "Role_ID" });
            DropIndex("dbo.UserRoles", new[] { "User_ID" });
            DropIndex("dbo.ScoreLogs", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "UserDetailsID" });
            DropIndex("dbo.Answers", new[] { "Question_ID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.ScoreLogs");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Questions");
            DropTable("dbo.ApplicationLogs");
            DropTable("dbo.Answers");
        }
    }
}

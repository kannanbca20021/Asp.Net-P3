namespace College.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDeptModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Professors", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Seminars", "DepartmentId", "dbo.Departments");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Departments", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.Departments", "Id");
            AddForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Professors", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Seminars", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seminars", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Professors", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Departments", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Departments", "Id");
            AddForeignKey("dbo.Seminars", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Professors", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}

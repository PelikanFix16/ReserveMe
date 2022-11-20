﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Persistence.Migrations
{
    public partial class UserVerified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Users");
        }
    }
}

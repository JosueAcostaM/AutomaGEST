using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_AutomaG.Migrations
{
    /// <inheritdoc />
    public partial class automagest_bdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "campos_conocimiento",
                columns: table => new
                {
                    idcam = table.Column<string>(type: "text", nullable: false),
                    codigocam = table.Column<string>(type: "text", nullable: false),
                    nombrecam = table.Column<string>(type: "text", nullable: false),
                    descripcioncam = table.Column<string>(type: "text", nullable: true),
                    estadocam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campos_conocimiento", x => x.idcam);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    idcon = table.Column<string>(type: "text", nullable: false),
                    telefonocon = table.Column<string>(type: "text", nullable: false),
                    fechacontacto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.idcon);
                });

            migrationBuilder.CreateTable(
                name: "modalidades",
                columns: table => new
                {
                    idmod = table.Column<string>(type: "text", nullable: false),
                    nombremod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modalidades", x => x.idmod);
                });

            migrationBuilder.CreateTable(
                name: "niveles",
                columns: table => new
                {
                    idniv = table.Column<string>(type: "text", nullable: false),
                    codigoniv = table.Column<string>(type: "text", nullable: false),
                    nombreniv = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_niveles", x => x.idniv);
                });

            migrationBuilder.CreateTable(
                name: "precios",
                columns: table => new
                {
                    idpre = table.Column<string>(type: "text", nullable: false),
                    inscripcionpre = table.Column<decimal>(type: "numeric", nullable: false),
                    matriculapre = table.Column<decimal>(type: "numeric", nullable: false),
                    arancelpre = table.Column<decimal>(type: "numeric", nullable: false),
                    monedapre = table.Column<string>(type: "text", nullable: false),
                    vigente = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_precios", x => x.idpre);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    idrol = table.Column<string>(type: "text", nullable: false),
                    codigorol = table.Column<string>(type: "text", nullable: false),
                    nombrerol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.idrol);
                });

            migrationBuilder.CreateTable(
                name: "tipos_horario",
                columns: table => new
                {
                    idtipo = table.Column<string>(type: "text", nullable: false),
                    nombretipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_horario", x => x.idtipo);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    idusu = table.Column<string>(type: "text", nullable: false),
                    nombreusu = table.Column<string>(type: "text", nullable: false),
                    emailusu = table.Column<string>(type: "text", nullable: false),
                    passwordhash = table.Column<string>(type: "text", nullable: false),
                    activousu = table.Column<bool>(type: "boolean", nullable: false),
                    fechacreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.idusu);
                });

            migrationBuilder.CreateTable(
                name: "aspirantes",
                columns: table => new
                {
                    idasp = table.Column<string>(type: "text", nullable: false),
                    idcon = table.Column<string>(type: "text", nullable: false),
                    nombreasp = table.Column<string>(type: "text", nullable: true),
                    apellidoasp = table.Column<string>(type: "text", nullable: true),
                    emailasp = table.Column<string>(type: "text", nullable: true),
                    provinciaasp = table.Column<string>(type: "text", nullable: true),
                    ciudadasp = table.Column<string>(type: "text", nullable: true),
                    nivelinteres = table.Column<string>(type: "text", nullable: true),
                    estadoasp = table.Column<string>(type: "text", nullable: true),
                    programainteres = table.Column<string>(type: "text", nullable: true),
                    fecharegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspirantes", x => x.idasp);
                    table.ForeignKey(
                        name: "FK_aspirantes_contactos_idcon",
                        column: x => x.idcon,
                        principalTable: "contactos",
                        principalColumn: "idcon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "programas",
                columns: table => new
                {
                    idpro = table.Column<string>(type: "text", nullable: false),
                    nombrepro = table.Column<string>(type: "text", nullable: false),
                    idcam = table.Column<string>(type: "text", nullable: false),
                    idniv = table.Column<string>(type: "text", nullable: false),
                    idmod = table.Column<string>(type: "text", nullable: false),
                    idpre = table.Column<string>(type: "text", nullable: false),
                    duracionpro = table.Column<string>(type: "text", nullable: true),
                    descripcionpro = table.Column<string>(type: "text", nullable: true),
                    estadopro = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programas", x => x.idpro);
                    table.ForeignKey(
                        name: "FK_programas_campos_conocimiento_idcam",
                        column: x => x.idcam,
                        principalTable: "campos_conocimiento",
                        principalColumn: "idcam",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_programas_modalidades_idmod",
                        column: x => x.idmod,
                        principalTable: "modalidades",
                        principalColumn: "idmod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_programas_niveles_idniv",
                        column: x => x.idniv,
                        principalTable: "niveles",
                        principalColumn: "idniv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_programas_precios_idpre",
                        column: x => x.idpre,
                        principalTable: "precios",
                        principalColumn: "idpre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    idhor = table.Column<string>(type: "text", nullable: false),
                    idtipo = table.Column<string>(type: "text", nullable: false),
                    dia = table.Column<string>(type: "text", nullable: false),
                    horainicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    horafin = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.idhor);
                    table.ForeignKey(
                        name: "FK_horarios_tipos_horario_idtipo",
                        column: x => x.idtipo,
                        principalTable: "tipos_horario",
                        principalColumn: "idtipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_roles",
                columns: table => new
                {
                    idusu = table.Column<string>(type: "text", nullable: false),
                    idrol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_roles", x => new { x.idusu, x.idrol });
                    table.ForeignKey(
                        name: "FK_usuario_roles_roles_idrol",
                        column: x => x.idrol,
                        principalTable: "roles",
                        principalColumn: "idrol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_roles_usuarios_idusu",
                        column: x => x.idusu,
                        principalTable: "usuarios",
                        principalColumn: "idusu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "programas_horarios",
                columns: table => new
                {
                    idpro = table.Column<string>(type: "text", nullable: false),
                    idhor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programas_horarios", x => new { x.idpro, x.idhor });
                    table.ForeignKey(
                        name: "FK_programas_horarios_horarios_idhor",
                        column: x => x.idhor,
                        principalTable: "horarios",
                        principalColumn: "idhor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_programas_horarios_programas_idpro",
                        column: x => x.idpro,
                        principalTable: "programas",
                        principalColumn: "idpro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aspirantes_idcon",
                table: "aspirantes",
                column: "idcon");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_idtipo",
                table: "horarios",
                column: "idtipo");

            migrationBuilder.CreateIndex(
                name: "IX_programas_idcam",
                table: "programas",
                column: "idcam");

            migrationBuilder.CreateIndex(
                name: "IX_programas_idmod",
                table: "programas",
                column: "idmod");

            migrationBuilder.CreateIndex(
                name: "IX_programas_idniv",
                table: "programas",
                column: "idniv");

            migrationBuilder.CreateIndex(
                name: "IX_programas_idpre",
                table: "programas",
                column: "idpre");

            migrationBuilder.CreateIndex(
                name: "IX_programas_horarios_idhor",
                table: "programas_horarios",
                column: "idhor");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_roles_idrol",
                table: "usuario_roles",
                column: "idrol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspirantes");

            migrationBuilder.DropTable(
                name: "programas_horarios");

            migrationBuilder.DropTable(
                name: "usuario_roles");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "horarios");

            migrationBuilder.DropTable(
                name: "programas");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "tipos_horario");

            migrationBuilder.DropTable(
                name: "campos_conocimiento");

            migrationBuilder.DropTable(
                name: "modalidades");

            migrationBuilder.DropTable(
                name: "niveles");

            migrationBuilder.DropTable(
                name: "precios");
        }
    }
}

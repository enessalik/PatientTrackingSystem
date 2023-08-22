using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientTrackingSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var stored_p = "CREATE PROCEDURE public.insert_patient(p_id_card BIGINT,p_name_surname CHARACTER VARYING,p_birthday DATE) LANGUAGE plpgsql AS $$ BEGIN INSERT INTO public.\"Patients\" (id_card, name_surname, birthday) VALUES(p_id_card, p_name_surname, p_birthday); END; $$;";

            migrationBuilder.Sql(stored_p);

      

        }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
}

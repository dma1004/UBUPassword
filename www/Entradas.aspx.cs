using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using LibClass;

namespace www
{
    public partial class Entradas : Page
    {
        Usuario usuario;
        ICapaDatos db;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (Usuario)Session["UsuarioActivo"];

            if (usuario == null)
            {
                Response.Redirect("/Inicio.aspx");
                return;
            }
            if (usuario.EsGestor)
            {
                this.btnIrGestion.Visible = true;
            }
            //No tiene contraseña caducada TODO

            this.lblUser.Text = usuario.ToString();

            db = (ICapaDatos)Application["BaseDeDatos"];

            if (db == null)
            {
                db = new DBPruebas();
                Application["BaseDeDatos"] = db;
            }

            DataTable tabla_entradas = new DataTable();
            tabla_entradas.Columns.Add("Id.", typeof(Int16));
            tabla_entradas.Columns.Add("Descripción", typeof(String));
            tabla_entradas.Columns.Add("Email", typeof(String));

            if (!Page.IsPostBack)
            {
                foreach (Entrada en in db.ObtenerTodasEntradas())
                {
                    if (db.PuedeAccederEntrada(en,this.usuario.Id))
                    {
                        tabla_entradas.Rows.Add(en.Id, en.Descripción, en.Email);
                    }
                }
                this.gvEntradas.DataSource = tabla_entradas;
                this.gvEntradas.DataBind();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Anular sesion
            Session["UsuarioActivo"] = null;
            //Reenviar a inicio
            Response.Redirect("/Inicio.aspx");
        }

        protected void btnIrGestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Gestion.aspx");

        }

        protected void verContraseña(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "btnVerContraseña")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvEntradas.Rows[index];
                short identrada = Convert.ToInt16(row.Cells[0].Text);

                Entrada en = db.ObtenerEntrada(identrada);

                this.lblMostrarContraseña.Text = $"Has accedido a la entrada {row.Cells[0].Text}, su contraseña es: <b style=\"font-family: monospace;\">{en.Password}</b>";
                this.lblMostrarContraseña.Visible = true;

                db.CrearEntradaLog(this.usuario.Id, en);
            }
        }
    }
}
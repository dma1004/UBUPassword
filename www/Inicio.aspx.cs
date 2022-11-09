using System;
using System.Web.UI;
using Datos;
using LibClass;

namespace www
{
    public partial class Inicio : Page
    {
        Usuario usuario;
        ICapaDatos db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = (ICapaDatos)Application["BaseDeDatos"];

            if (db == null)
            {
                db = new DBPruebas();
                Application["BaseDeDatos"] = db;
            }

            usuario = (Usuario)Session["UsuarioActivo"];

            if (usuario != null)
            {
                if (this.usuario.EsGestor)
                {
                    Response.Redirect("/Gestion.aspx");
                }
                else
                {
                    Response.Redirect("/Entradas.aspx");
                }
            }
            usuario = null;
            Session["UsuarioActivo"] = usuario;
        }

        protected void Entrar_Click(object sender, EventArgs e)
        {
            this.usuario = db.ObtenerUsuario(this.TBXUserName.Text);
            if (this.usuario is null)
            {
                this.lblerror.Text = "Email incorrecto";
            }
            else if (!this.usuario.ComprobarContraseña(this.TBXPassword.Text))
            {
                this.lblerror.Text = "Contraseña incorrecta";
            }
            else if (this.usuario.ContraseñaCaducada())
            {
                this.lblerror.Text = "Contraseña caducada (30 días), contacta con un Gestor";
            }
            else
            {
                Session["UsuarioActivo"] = this.usuario;
                db.CrearEntradaLog(usuario.Id, null);
                if (this.usuario.EsGestor)
                {
                    Response.Redirect("/Gestion.aspx");
                }
                else
                {
                    Response.Redirect("/Entradas.aspx");
                }
            }

            this.lblerror.Visible = true;
        }
    }
}
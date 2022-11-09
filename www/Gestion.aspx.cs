using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using LibClass;

namespace www
{
    public partial class Gestion : Page
    {
        ICapaDatos db;
        Usuario usuario;
        List<ListItem> itemsSel = new List<ListItem>();
        DataTable tabla_usuarios;
        DataTable tabla_logs;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (Usuario)Session["UsuarioActivo"];

            if (usuario == null || !usuario.EsGestor)
            {
                // Llevar a inicio, donde se decide dónde llevar al usuario
                Response.Redirect("/Inicio.aspx");
                return;
            }

            this.lblUser.Text = usuario.ToString();

            db = (ICapaDatos)Application["BaseDeDatos"];

            if (db == null)
            {
                db = new DBPruebas();
                Application["BaseDeDatos"] = db;
            }

            //Poner el nombre de usuario en la etiqueta
            //actualizar la etiqueta de título (para saber donde estamos)

            this.tabla_usuarios = new DataTable();
            this.tabla_usuarios.Columns.Add("Id.", typeof(Int16));
            this.tabla_usuarios.Columns.Add("Nombre", typeof(String));
            this.tabla_usuarios.Columns.Add("Apellidos", typeof(String));
            this.tabla_usuarios.Columns.Add("Email", typeof(String));
            this.tabla_usuarios.Columns.Add("Gestor", typeof(bool));
            
            this.tabla_logs = new DataTable();
            this.tabla_logs.Columns.Add("Id.", typeof(Int16));
            this.tabla_logs.Columns.Add("Fecha", typeof(DateTime));
            this.tabla_logs.Columns.Add("usuario", typeof(Usuario));
            this.tabla_logs.Columns.Add("Tipo de acceso", typeof(string));

            if (!this.IsPostBack)
            {
                // Añadir opciones de selección de sección
                this.itemsSel.Add(new ListItem("Elige la sección", "null"));
                this.itemsSel.Add(new ListItem("Gestión Usuarios", "Usuarios"));
                this.itemsSel.Add(new ListItem("Log", "Log"));

                this.ddlSelFuncionGestor.DataSource = this.itemsSel;
                this.ddlSelFuncionGestor.DataBind();
            }
        }


        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Anular sesion
            Session["UsuarioActivo"] = null;
            //Reenviar a inicio
            Response.Redirect("/Inicio.aspx");
        }

        protected void btnIrEntradas_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Entradas.aspx");

        }

        protected void ddlSelFuncionGestor_SelectedIndex(object sender, EventArgs e)
        {
            string funcion = this.ddlSelFuncionGestor.SelectedValue;

            if (funcion.Equals("Gestión Usuarios"))
            {
                // Añadir usuarios a la sección de usuarios
                foreach (Usuario u in db.ObtenerTodosUsuarios())
                {
                    this.tabla_usuarios.Rows.Add(u.Id, u.Nombre, u.Apellidos, u.Email, u.EsGestor);
                }
                this.gvGestionUsuarios.DataSource = this.tabla_usuarios;
                this.gvGestionUsuarios.DataBind();

                this.gvLog.Visible = false;
                this.gvGestionUsuarios.Visible = true;
            }else if (funcion.Equals("Log"))
            {
                // Añadir entradas a la sección de log
                foreach (EntradaLog en in db.ObtenerTodasEntradasLog())
                {
                    string tipo;
                    // Si no tiene ninguna Entrada asociada
                    if (en.Entrada == null)
                    {
                        tipo = "Inicio de sesión";
                    }
                    else
                    {
                        tipo = en.Entrada.ToString();
                    }

                    this.tabla_logs.Rows.Add(en.Id, en.Fecha, en.Usuario, tipo);
                }
                this.gvLog.DataSource = this.tabla_logs;
                this.gvLog.DataBind();

                this.gvGestionUsuarios.Visible = false;
                this.gvLog.Visible = true;
            }
            else
            {
                this.gvGestionUsuarios.Visible = false;
                this.gvLog.Visible = false;
            }
        }
    }
}
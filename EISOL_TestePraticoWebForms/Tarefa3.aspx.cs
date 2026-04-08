using System;
using System.Web.UI.WebControls;

namespace EISOL_TestePraticoWebForms
{
	public partial class Tarefa3 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.CarregarControles();
			}
		}

		/// <summary>
		/// Carregar dados e povoar os controles
		/// </summary>
		private void CarregarControles()
		{
			this.ddlUf.Items.Clear();
			this.ddlUf.Items.Add(new ListItem("[Selecione]", ""));
			this.ddlUf.DataSource = new BLL.UF().CarregarTodos();
			this.ddlUf.DataTextField = "NOME";
			this.ddlUf.DataValueField = "COD_UF";
			this.ddlUf.DataBind();

			this.ddlCidades.Items.Clear();
			this.ddlCidades.Items.Add(new ListItem("[Selecione]", ""));
		}

		protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CarregarCidades();
		}

		/// <summary>
		/// Carregar cidades pela UF selecionada
		/// </summary>
		private void CarregarCidades()
		{
			this.ddlCidades.Items.Clear();
			this.ddlCidades.Items.Add(new ListItem("[Selecione]", ""));

			if (string.IsNullOrEmpty(ddlUf.SelectedValue))
				return;

			decimal codigoUF = Convert.ToDecimal(ddlUf.SelectedValue);
			var cidades = new BLL.CIDADES().CarregarPorUF(codigoUF);

			if (cidades != null)
			{
				this.ddlCidades.DataSource = cidades;
				this.ddlCidades.DataTextField = "NOME";
				this.ddlCidades.DataValueField = "COD_CIDADE";
				this.ddlCidades.DataBind();
			}
		}
	}
}
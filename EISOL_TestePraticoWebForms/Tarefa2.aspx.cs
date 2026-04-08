using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EISOL_TestePraticoWebForms
{
	public partial class Tarefa2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnGravar_Click(object sender, EventArgs e)
		{
			var erros = new List<string>();

			if (string.IsNullOrWhiteSpace(txtNome.Text))
				erros.Add("Nome");

			string cpf = Regex.Replace(txtCpf.Text, @"\D", "");
			if (string.IsNullOrWhiteSpace(cpf))
				erros.Add("CPF");

			if (string.IsNullOrWhiteSpace(txtRg.Text))
				erros.Add("RG");

			if (ddlSexo.SelectedIndex == 0)
				erros.Add("Sexo");

			DateTime dataNascimento;
			bool dataValida = DateTime.TryParseExact(txtDataNascimento.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento);

			if (!dataValida)
				erros.Add("Data de nascimento");

			if (erros.Count > 0)
			{
				return;
			}

			string telefone = Regex.Replace(txtTelefone.Text, @"\D", "");

			var pessoa = new DAO.PESSOAS();
			pessoa.NOME = txtNome.Text.Trim();
			pessoa.CPF = cpf;
			pessoa.RG = txtRg.Text.Trim();
			pessoa.TELEFONE = telefone;
			pessoa.EMAIL = txtEmail.Text.Trim();
			pessoa.SEXO = ddlSexo.SelectedValue;
			pessoa.DATA_NASCIMENTO = dataNascimento;

			this.Gravar(pessoa);
			this.Limpar();
		}

		/// <summary>
		/// Persistir os dados no Banco.
		/// </summary>
		/// <param name="pessoa">DAO.PESSOAS</param>
		private void Gravar(DAO.PESSOAS pessoa)
		{
			new BLL.PESSOAS().Adicionar(pessoa);
			this.Alertar();
		}

		/// <summary>
		/// Apresentar o alerta de sucesso na operação.
		/// </summary>
		private void Alertar()
		{
			this.divAlerta.Visible = true;
		}

		/// <summary>
		/// Limpar os campos após a presistência dos dados.
		/// </summary>
		private void Limpar()
		{
			txtNome.Text = string.Empty;
			txtCpf.Text = string.Empty;
			txtRg.Text = string.Empty;
			txtTelefone.Text = string.Empty;
			txtEmail.Text = string.Empty;
			ddlSexo.SelectedIndex = 0;
			txtDataNascimento.Text = string.Empty;
		}
	}
}
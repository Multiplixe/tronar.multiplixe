using System;
using System.Text.Json.Serialization;
using adduoform = adduo.helper;
using coreinterfaces = multiplixe.comum.dto.interfaces;

namespace multiplixe.comum.dto.entries
{
	public class SolicitacaoRedefinicaoDeSenha : adduoform.entries.BaseEntries, coreinterfaces.IEmpresaID
	{
		[JsonPropertyName("email")]
		public adduoform.entries.Email Email { get; set; }

		[JsonIgnore]
		public Guid EmpresaId { get; set; }

		public SolicitacaoRedefinicaoDeSenha()
		{
			InitEntries();
		}

		public override void SetEntries()
		{
			AddEntry(Email);
		}

		public override void AddValidators()
		{
		}

		public override void InitEntries()
		{
			Email = new adduoform.entries.Email();
		}

	}
}

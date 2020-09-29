using System;
using System.Text.Json.Serialization;
using adduoform = adduo.helper;
using coreinterfaces = multiplixe.comum.dto.interfaces;

namespace multiplixe.comum.dto.entries
{
	public class RedefinicaoDeSenha : adduoform.entries.BaseEntries
	{
		[JsonPropertyName("password")]
		public adduoform.entries.Password Senha { get; set; }

		[JsonPropertyName("token")]
		public Guid Token { get; set; }

		public RedefinicaoDeSenha()
		{
			InitEntries();
		}

		public override void SetEntries()
		{
			AddEntry(Senha);
		}

		public override void AddValidators()
		{
		}

		public override void InitEntries()
		{
			Senha = new adduoform.entries.Password();
		}

	}
}

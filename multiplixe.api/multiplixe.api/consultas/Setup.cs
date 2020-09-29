using adduo.helper.envelopes;
using multiplixe.usuarios.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.consultas
{
    public class Setup
    {
        private Ranking ranking { get; }
        private Dashboard dashboard { get; }

        private comum_dto.Setup setup { get; set; }

        public Setup(Dashboard dashboard, Ranking ranking)
        {
            this.dashboard = dashboard;
            this.ranking = ranking;

            setup = new comum_dto.Setup();
        }

        public comum_dto.Setup Get(Guid usuarioId)
        {
            this.setup = new comum_dto.Setup();

            Usuario(usuarioId);
            Dashboard(usuarioId);
            Ranking(usuarioId);

            return setup;
        }

        private void Usuario(Guid usuarioId)
        {
            var usuarioClient = new UsuarioClient();

            var filtro = new comum_dto.filtros.UsuarioFiltro { UsuarioId = usuarioId };
            var request = new RequestEnvelope<comum_dto.filtros.UsuarioFiltro>(filtro);
            var response = usuarioClient.Obter(request);

            if (response.Success)
            {
                setup.usuario = response.Item;
            }
        }

        private void Dashboard(Guid usuarioId)
        {
            var response = dashboard.Obter(usuarioId);

            if(response.Success)
            {
                setup.Dashboard = response.Item;
            }

        }

        private void Ranking(Guid usuarioId)
        {
            var response = ranking.Obter(usuarioId);

            if (response.Success)
            {
                setup.Ranking = response.Item;
            }

        }


    }
}

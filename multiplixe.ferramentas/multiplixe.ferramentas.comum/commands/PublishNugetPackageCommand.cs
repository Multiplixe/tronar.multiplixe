using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public class PublishNugetPackageCommand : Command
    {
        private string File { get; set; }
        private string AccessToken { get; set; }

        public PublishNugetPackageCommand(string file, string accessToken) : base(new ProjectLib())
        {
            File = file;
            AccessToken = accessToken;
        }

        public override string ToString()
        {
            return $"/c gpr push {File} -k {AccessToken}";
        }
    }
}

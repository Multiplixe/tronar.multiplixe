using multiplixe.ferramentas.comum.projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.ferramentas.comum.commands
{
    public  class PublishConsoleCommand : Command
    {
        public PublishConsoleCommand(Project project) : base(project)
        {
                Log.Write($"- publicando projeto console;");
        }

        public override string ToString()
        {
            return $"/c dotnet publish {project.FileProjectPath} -c Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile";
        }

    }
}

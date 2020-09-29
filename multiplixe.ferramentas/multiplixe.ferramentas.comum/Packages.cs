using multiplixe.ferramentas.comum.commands;
using multiplixe.ferramentas.comum.projects;
using System;
using System.Linq;
using System.Threading;

namespace multiplixe.ferramentas.comum
{
    public class Packages
    {
        public static void UpdatePackages(Project project)
        {
            Log.Write($"- atualizando pacotes;");

            foreach (var packageName in project.Packages)
            {
                Log.Write($"- - {packageName};");
                var command = new UpdatePackageCommand(project, packageName);
                ExecuteCommand.Execute(command);
                Thread.Sleep(5000);
            }
        }

        public static void BuildPackage(Project project)
        {
            Command command = project.CreatePublishCommand();
            ExecuteCommand.Execute(command);
            Thread.Sleep(5000);
        }

        public static void AddNugetPackage(Project project)
        {
            if (project.Type == typeOfProject.lib)
            {
                var command = new AddNugetCommand(project);
                ExecuteCommand.Execute(command);
                Thread.Sleep(5000);
            }
        }
    }
}


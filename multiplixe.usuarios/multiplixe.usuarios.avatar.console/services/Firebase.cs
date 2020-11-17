using Firebase.Auth;
using Firebase.Storage;
using multiplixe.empresas.client;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.avatar.console.services
{
    public class Firebase
    {
        private AppSettings settings { get; }
        private EmpresaClient empresaClient { get; }
        private PathHelper pathHelper { get; }

        public Firebase(
            AppSettings settings,
            EmpresaClient empresaClient,
            PathHelper pathHelper)
        {
            this.settings = settings;
            this.empresaClient = empresaClient;
            this.pathHelper = pathHelper;
        }

        public async Task Processar(dto.AvatarParaProcessar avatarParaProcessar)
        {

            try
            {
                var path = pathHelper.CriarImagemCaminhoCompleto(avatarParaProcessar);

                if (File.Exists(path))
                {
                    var firebaseInfo = empresaClient.ObterInfoFirebase(avatarParaProcessar.EmpresaId);

                    if (firebaseInfo.Success)
                    {
                        using (var stream = File.Open(path, FileMode.Open))
                        {
                            var auth = new FirebaseAuthProvider(new FirebaseConfig(firebaseInfo.Item.ApiKey));

                            var a = await auth.SignInWithEmailAndPasswordAsync(firebaseInfo.Item.Usuario, firebaseInfo.Item.Senha);

                            var c = new CancellationTokenSource();

                            var task = new FirebaseStorage($"{firebaseInfo.Item.Bucket}.appspot.com", new FirebaseStorageOptions
                            {
                                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                                ThrowOnCancel = true
                            })
                            .Child(settings.PastaFirebase)
                            .Child($"{avatarParaProcessar.UsuarioId.ToString()}.jpg")
                            .PutAsync(stream, c.Token);

                            Console.WriteLine("Firebase processou");

                            await task;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                var foregroundColor = Console.ForegroundColor;
                var backgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Erro");
                Console.WriteLine(ex.Message);
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("");

                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
            }
        }

    }
}

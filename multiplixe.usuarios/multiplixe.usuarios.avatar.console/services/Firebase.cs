﻿using Firebase.Auth;
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
            var path = pathHelper.CriarImagemCaminhoCompleto(avatarParaProcessar);


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
                    .Child(avatarParaProcessar.Avatar.Imagem)
                    .PutAsync(stream, c.Token);

                    Console.WriteLine("Firebase processou");

                    await task;
                }
            }

        }

    }
}

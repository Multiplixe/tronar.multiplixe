using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Net;

namespace multiplixe.central_rtdb.grpc.core
{
    public abstract class Persistencia
    {
        protected FirebaseClient firebaseClient { get; }

        public Persistencia(FirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        public abstract ChildQuery ObterChildQuery(string[] paths);

        public protos.Response Post(string[] path, object o)
        {
            var response = new protos.Response();

            try
            {
                var query = ObterChildQuery(path);

                query.PostAsync<object>(o).GetAwaiter();

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Erro = ex.Message;
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

        public protos.Response Put(string[] path, object o)
        {
            var response = new protos.Response();

            try
            {
                var query = ObterChildQuery(path);

                query.PutAsync<object>(o).GetAwaiter();

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Erro = ex.Message;
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

        public protos.Response Delete(string[] path)
        {
            var response = new protos.Response();

            try
            {
                var query = ObterChildQuery(path);

                query.PutAsync<object>(null).GetAwaiter();

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Erro = ex.Message;
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }

    }
}

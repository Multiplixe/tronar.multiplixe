import { Injectable } from '@angular/core';
import { firebase } from '@firebase/app';
import '@firebase/messaging';
import { HttpService } from './http.service';

import { firebaseEnvironment } from '../../environments/environment';
import { AppInjector } from '../app-injector';

@Injectable({
   providedIn: 'root'
})
export class PushNotificationService {

   httpService: HttpService;

   constructor() {
      this.httpService = AppInjector.get(HttpService);
   }

   init(): Promise<void> {
      return new Promise<void>((resolve, reject) => {
         navigator.serviceWorker.ready.then(async (registration) => {

            if (!firebase.messaging.isSupported()) {
               await this.registerToken("isSupported");
               resolve();
               return;
            }

            const messaging = firebase.messaging();

            //   messaging.useServiceWorker(registration);

            //    messaging.usePublicVapidKey(
            //       firebaseEnvironment.vapidKey
            //    );

            messaging.onMessage((payload) => {
               console.log('payload', payload);
            });

            messaging.onTokenRefresh(() => {
               messaging.getToken()
                  .then(async (refreshedToken: string) => {
                     await this.registerToken(refreshedToken);
                  }).catch(async (err) => {
                     await this.registerToken(err);
                     console.error(err);
                  });
            });



            resolve();

         }, (err) => {
            reject(err);
         });
      });
   }

   private async registerToken(_token: string) {
      await this.httpService.postAPI<any>("restrito/usuarios/registrar-token-push", { value: _token });
   }

   requestPermission(): Promise<void> {
      return new Promise<void>(async (resolve) => {
         if (!Notification) {
            await this.registerToken("!Notification");
            resolve();
            return;
         }
         if (!firebase.messaging.isSupported()) {
            await this.registerToken("isSupported 2");
            resolve();
            return;
         }
         try {
            const messaging = firebase.messaging();
            await messaging.requestPermission();

            const token: string = await messaging.getToken();

            await this.registerToken(token);

         } catch (err) {
            // No notifications granted
         }

         resolve();
      });
   }


}
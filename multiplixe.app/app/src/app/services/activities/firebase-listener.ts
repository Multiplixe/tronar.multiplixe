import { AngularFireDatabase } from '@angular/fire/database';
import { AppInjector } from 'src/app/app-injector';
import { Factory as ActivityFactory } from './factory';
import { AuthService } from '../auth.service';

export class FirebaseListener {

   private angularFireDatabase: AngularFireDatabase;
   private activityFactory: ActivityFactory;
   private authService: AuthService;

   constructor() {
      this.angularFireDatabase = AppInjector.get(AngularFireDatabase)
      this.activityFactory = AppInjector.get(ActivityFactory)
      this.authService = AppInjector.get(AuthService)
   }

   start() {

      this.stop();

      let id = this.authService.getId();

      let ref =
         this.angularFireDatabase
            .database
            .ref('users/' + id + '/activities');

      ref.on('child_changed', (e) => {
         let activity = this.activityFactory.get(e.key);
         activity.processActivity(e.val());
      });


   }

   stop() {

      let id = this.authService.getId();

      let ref =
         this.angularFireDatabase
            .database
            .ref('users/' + id + '/activities');

      ref.off('child_changed', (e) => { });
   }


   private teste(element: number, id: string) {

      this.angularFireDatabase
         .list('users/' + id + '/activities')
         .push({
            "name": "score",
            "value": {
               "points": element,
               "level": "Nível " + element
            }
         })

      setTimeout(() => {
         this.teste(++element, id);
      }, 10000);

   }

}
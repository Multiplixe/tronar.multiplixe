import { AngularFireDatabase } from '@angular/fire/database';
import { AppInjector } from 'src/app/app-injector';
import { Factory } from './factory';

export class FirebaseListener {

   private angularFireDatabase: AngularFireDatabase;
   private activityFactory: Factory;

   constructor() {
      this.angularFireDatabase = AppInjector.get(AngularFireDatabase)
      this.activityFactory = AppInjector.get(Factory)
   }

   private getRef() {
      return this.angularFireDatabase
         .database
         .ref('common/activities');
   }

   start() {

      this.stop();

      let ref = this.getRef();

      ref.on('child_changed', (e) => {
         let activity = this.activityFactory.get(e.key);
         activity.execute();
      });

   }

   stop() {
      let ref = this.getRef();
      ref.off('child_changed', (e) => { });
   }

}
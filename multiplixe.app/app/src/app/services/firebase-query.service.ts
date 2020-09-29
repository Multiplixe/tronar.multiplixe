import { AngularFireDatabase } from '@angular/fire/database';
import { AppInjector } from 'src/app/app-injector';
import { AuthService } from './auth.service';

export class FirebaseQuery  {

   private angularFireDatabase: AngularFireDatabase;
   private authService: AuthService;

   constructor() {
      this.angularFireDatabase = AppInjector.get(AngularFireDatabase)
      this.authService = AppInjector.get(AuthService)
   }
   
   async avatar() {
      
      let id = this.authService.getId();

      let ref =
         this.angularFireDatabase
            .database
            .ref('users/' + id + '/activities/avatar');

      return await ref.once('value');

   }

}
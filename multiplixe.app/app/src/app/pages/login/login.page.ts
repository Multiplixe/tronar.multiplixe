

import { Component, OnInit, AfterViewInit } from '@angular/core';
import Login from 'src/app/dtos/login';
import { BasePage } from '../base.page';
import { DisplayControl } from 'src/app/dtos/display-control';

@Component({
   selector: 'app-login',
   templateUrl: './login.page.html'
})
export class LoginPage extends BasePage implements OnInit, AfterViewInit {

   public login: Login = Login.Create();
   public messages: DisplayControl = DisplayControl.create();

   constructor() {
      super();
   }

   ngOnInit() {
      this.authService.isAuthenticated()
         .then(() => {
            this.redirect("/setup");
         })
         .catch(() => {
            this.displayControl.set('1');
         });
   }

   ngAfterViewInit() {
   }

   async authenticate() {

      try {

         this.messages.reset();

         this.runLoading('Autenticando...');

         let fireaseUser = await this.authService.authenticate(this.login);

         this.updateCurrentUser(fireaseUser);

         this.stopLoading();

         this.redirect("/setup");

      }
      catch (e) {


         if (e.code) {
            this.messages.set(e.code);
         }
         else {
            this.processError(e, "Ocorreu algum problema fazer a autenticação. Por favor, tente novamente.");
         }
      }
      finally {
         this.stopLoading();
      }
   }




}

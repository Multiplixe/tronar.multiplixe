import { Component, OnInit, ViewChild } from '@angular/core';
import { BasePage } from '../base.page';
import { AppInjector } from '../../app-injector'
import { UserProfileService } from 'src/app/services/user-profile.service';

import UserProfileEntry from 'src/app/dtos/user-profile.entries';
import { EntryStatusEnum, EntryCodeEnum } from 'src/app/entries/entry.enum';
import Login from 'src/app/dtos/login';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html'
})
export class RegisterPage extends BasePage implements OnInit {

  public user: UserProfileEntry = UserProfileEntry.Create();

  private registerService: UserProfileService;

  constructor() {
    super();
    this.registerService = AppInjector.get(UserProfileService);

  }
  
  ngOnInit() {
   this.resetUser();
  }

  private resetUser() {
    this.user = UserProfileEntry.Create();
  }

  async register() {

    var passwordConfirValid = this.passwordConfirmValidate();

    if (passwordConfirValid) {

      try {

        this.runLoading();

        await this.registerService.save(this.user)

        let login = new Login();
        login.email = this.user.email.value;
        login.password = this.user.password.value;

        await this.authService.authenticate(login);

        this.stopLoading();

        this.resetUser();

        this.redirect('setup');

      }
      catch (e) {

        if (e.status != 500) {
          this.user = e.error.item;
        }
        else {
          this.processError(e, "Ocorreu algum problema ao criar seu perfil. Por favor, tente novamente.");
        }

      }
      finally {
        this.stopLoading();
      }
    }
  }

  private passwordConfirmValidate() {

    let valid = this.user.password.value == this.user.confirmPassword;

    if (!valid) {
      this.user.password.status = EntryStatusEnum.invalid;
      this.user.password.code = EntryCodeEnum.different;
    }

    return valid;

  }

}

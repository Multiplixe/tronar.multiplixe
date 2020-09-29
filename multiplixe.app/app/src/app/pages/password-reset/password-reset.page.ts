import { Component, OnInit } from '@angular/core';
import { BasePage } from '../base.page';
import { Envelope } from 'src/app/envelopes/envelope';
import { PasswordResetEntries } from 'src/app/dtos/password-reset.entries';
import { EntryStatusEnum, EntryCodeEnum } from 'src/app/entries/entry.enum';
import { DisplayControl } from 'src/app/dtos/display-control';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.page.html'
})
export class PasswordResetPage extends BasePage implements OnInit {

  public entries: PasswordResetEntries = PasswordResetEntries.Create();
  public token: string = null;
  public confirmPassword: string = null;
  public steps: DisplayControl = DisplayControl.create();
  public messagesStep1: DisplayControl = DisplayControl.create();
  public messagesStep2: DisplayControl = DisplayControl.create();

  constructor(private route: ActivatedRoute) {
    super();

    this.route.queryParams.subscribe(params => {
      this.entries.mode = params['mode'];
      this.entries.oobCode = params['oobCode'];
      this.entries.apiKey = params['apiKey'];
      this.passwordResetValidate()
  });
  }

  ngOnInit() {
    this.steps.set('0');
  }

  async passwordResetValidate() {

    try {

      this.messagesStep1.reset();

      this.runLoading();

      await this.authService.passwordResetValidate(this.entries);

      this.steps.set('2');

      this.stopLoading();

    } catch (e) {

      if (e.code) {
        this.steps.set('1');
        this.messagesStep1.set(e.code);
      }
      else {
        this.processError(e, "Ocorreu algum problema fazer a redefinição da senha. Por favor, tente novamente.");
      }

    }
    finally {
      this.stopLoading();
    }

  }

  async reset() {

    this.messagesStep2.reset();
    this.entries.resetAll();

    var passwordConfirValid = this.passwordConfirmValidate();

    if (passwordConfirValid) {

      try {

        this.runLoading();

        await this.authService.passwordReset(this.entries);

        this.steps.set('3');

        this.stopLoading();

      } catch (e) {

        if (e.code) {
          this.entries.password.setInvalidStatus();
          this.messagesStep2.set(e.code);
        }
        else {
          this.processError(e, "Ocorreu algum problema fazer a redefinição da senha. Por favor, tente novamente.");
        }
      }
      finally {
        this.stopLoading();
      }

    }

  }

  private passwordConfirmValidate() {

    let valid = this.entries.password.value == this.confirmPassword;

    this.entries.password.status = EntryStatusEnum.none;
    this.entries.password.code = EntryCodeEnum.none;

    if (!valid) {
      this.entries.password.status = EntryStatusEnum.invalid;
      this.entries.password.code = EntryCodeEnum.different;
    }

    return valid;

  }

}

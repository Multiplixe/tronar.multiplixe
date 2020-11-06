import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PasswordResetEntries } from 'src/app/dtos/password-reset.entries';
import { EntryCodeEnum, EntryStatusEnum } from 'src/app/entries/entry.enum';
import { DisplayHelper } from 'src/app/helpers/display.helper';
import { BasePage } from '../base-page';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.page.html'
})
export class PasswordResetPage extends BasePage implements OnInit {

  public entries: PasswordResetEntries = PasswordResetEntries.Create();
  public token: string = null;
  public confirmPassword: string = null;
  public steps: DisplayHelper = DisplayHelper.create();
  public messagesStep1: DisplayHelper = DisplayHelper.create();
  public messagesStep2: DisplayHelper = DisplayHelper.create();

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

        console.log("this.confirmPassword", this.confirmPassword)
        console.log("this.entries", this.entries)
        await this.authService.passwordReset(this.entries);

        this.steps.set('3');

        this.stopLoading();

      } catch (e) {

        console.log("e", e)

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

    this.entries.password.status = EntryStatusEnum.none;
    this.entries.password.code = EntryCodeEnum.none;

    if (this.entries.password.value != this.confirmPassword) {
      this.entries.password.status = EntryStatusEnum.invalid;
      this.entries.password.code = EntryCodeEnum.different;
      return false;
    }

    return true;
  }

}

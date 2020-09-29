import { Component, OnInit } from '@angular/core';
import { BasePage } from '../base.page';
import { Envelope } from 'src/app/envelopes/envelope';
import { PasswordResetRequestEntries } from 'src/app/dtos/password-reset-request.entries';
import { DisplayControl } from 'src/app/dtos/display-control';
import { EntryHelper } from 'src/app/entries/entry.helper';
import { EntryCodeEnum } from 'src/app/entries/entry.enum';

@Component({
  selector: 'app-password-reset-request',
  templateUrl: './password-reset-request.page.html'
})
export class PasswordResetRequestPage extends BasePage implements OnInit {

  public passwordRequest: PasswordResetRequestEntries = PasswordResetRequestEntries.Create();
  public messages: DisplayControl = DisplayControl.create();
  public steps: DisplayControl = DisplayControl.create();

  constructor() {
    super();
  }

  ngOnInit() {
    this.steps.set('1');
  }

  async request() {

    try {

      this.messages.reset();
      this.passwordRequest.resetAll();

      this.runLoading();

      EntryHelper.resetEntry(this.passwordRequest.email);

      await this.authService.passwordResetRequest(this.passwordRequest);

      this.steps.set('2');

      this.stopLoading();

    } catch (r) {
      this.passwordRequest.email.setInvalidStatus();
      this.messages.set(r.code);
    }
    finally {
      this.stopLoading();
    }

  }

}

import { Component, OnInit } from '@angular/core';
import UserProfileEntry from 'src/app/dtos/user-profile.entries';
import { UserProfileService } from 'src/app/services/user-profile.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../../restrict.page';
import { DisplayControl } from 'src/app/dtos/display-control';
import { SetupService } from 'src/app/services/setup.service';

@Component({
  selector: 'app-user-profile-edit',
  templateUrl: './user-profile-edit.page.html'
})
export class UserProfileEditPage extends RestrictPage implements OnInit {

  public user: UserProfileEntry = UserProfileEntry.Create();
  private userProfileService: UserProfileService;
  private setupService: SetupService;
  public messagesStep: DisplayControl = DisplayControl.create();

  constructor() {
    super();
    this.userProfileService = AppInjector.get(UserProfileService);
    this.setupService = AppInjector.get(SetupService);
  }

  async ngOnInit() {
    super.ngOnInit();
    this.get();
  }

  async get() {

    try {

      this.runLoading();

      let response = await this.userProfileService.get()

      this.user = UserProfileEntry.Merge(response.item);

      this.canShowContent();

      this.stopLoading();

    }
    catch (e) {

      if (e.status == 400) {
        this.user = e.error.item;
      }
      else {
        this.processError(e, "Ocorreu algum problema ao atualizar seu perfil. Por favor, tente novamente.", async () => { this.get() });
      }

    }
    finally {
      this.stopLoading();
    }

  }

  async update() {

    try {

      this.messagesStep.reset();

      this.runLoading("Atualizando");

      var response = await this.userProfileService.update(this.user)
      this.user = response.item;

      let profile = UserProfileEntry.extract(this.user);
      this.setupService.userProfile(profile);

      this.messagesStep.set(1);

      this.stopLoading();

    }
    catch (e) {

      if (e.status != 500) {
        this.messagesStep.set(e.status);
        this.user = e.error.item;
      }
      else {
        this.processError(e, "Ocorreu algum problema ao atualizar seu perfil. Por favor, tente novamente.", async () => { this.update() });
      }

    }
    finally {
      this.stopLoading();
    }
  }

}

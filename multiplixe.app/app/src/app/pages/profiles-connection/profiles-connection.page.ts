import { Component, OnInit, Output } from '@angular/core';
import { SocialMediaConnections } from 'src/app/dtos/social-media-connections';
import { SocialMediaProfileService } from 'src/app/services/social-media-profile.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../restrict.page';

@Component({
  selector: 'app-profiles-connection',
  templateUrl: './profiles-connection.page.html'
})
export class ProfilesConnectionPage extends RestrictPage implements OnInit {

  public socialMediaProfiles: SocialMediaConnections = new SocialMediaConnections();

  @Output()
  private profileService: SocialMediaProfileService;

  constructor() {
    super();
    this.profileService = AppInjector.get(SocialMediaProfileService)
  }

  async ngOnInit() {
    super.ngOnInit();
    await this.load();
  }

  async load() {

    try {

      this.runLoading("Buscando suas conexões");

      var response = await this.profileService.getProfilesConnection();
      this.socialMediaProfiles = response.item;

      this.canShowContent();
      this.stopLoading();
    }
    catch (e) {
      this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.", async () => { this.load() });
    }
    finally {
      this.stopLoading();
    }

  }
}

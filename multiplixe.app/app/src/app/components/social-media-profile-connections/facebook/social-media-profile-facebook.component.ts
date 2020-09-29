import { Component, OnInit, Input } from '@angular/core';

import { AppInjector } from 'src/app/app-injector';
import { FacebookService, InitParams, LoginResponse } from 'ngx-facebook';
import { SocialMediaProfileService } from 'src/app/services/social-media-profile.service';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';
import { BaseComponent } from '../../base.component';

import { facebookEnvironment } from '../../../../environments/environment';

@Component({
  selector: 'app-social-media-profile-facebook',
  templateUrl: './social-media-profile-facebook.component.html' 
})
export class SocialMediaProfileFacebookComponent
  extends BaseComponent
  implements OnInit {

  private profileService: SocialMediaProfileService;
  private facebookService: FacebookService;
  private FB: any;

  @Input()
  public profilesConnected: SocialMediaProfile[] = [];
  public profile: SocialMediaProfile;

  constructor() {
    super();
    this.facebookService = AppInjector.get(FacebookService)
    this.profileService = AppInjector.get(SocialMediaProfileService)
  }

  ngOnInit() {

    let initParams: InitParams = {
      appId: facebookEnvironment.app_id,
      xfbml: facebookEnvironment.xfbml,
      version: facebookEnvironment.version
    };

    this.facebookService.init(initParams);

    if (this.hasConnected()) {
      this.profile = this.profilesConnected[0];
    }

  }

  async ok() {

    if (this.hasConnected()) {
      this.redirectRestrict("minhas-conexoes/facebook");
    }
    else {
      await this.connect();
    }
  }  

  async connect() {

    try {

      var response: LoginResponse = await this.facebookService.login()

      if (response && response.status == "connected") {

        //

        var userInfo = await this.facebookService.api("/me?fields=id,name,picture.width(500).height(500)", "get", { access_token: response.authResponse.accessToken })

        var profile = new SocialMediaProfile();
        profile.profileId = userInfo.id;
        profile.name = userInfo.name;
        profile.token = JSON.stringify(response.authResponse);

        if (userInfo.picture &&
          userInfo.picture.data &&
          userInfo.picture.data.url) {
          profile.imageUrl = userInfo.picture.data.url;
        }

        await this.profileService.saveFacebook(profile);

        this.profilesConnected.push(profile);
      }
      else {
        this.showToast("Não foi possível fazer a conexão com o Facebook");
      }
    }
    catch (e) {
      this.showToast("Ocorreu algum problema ao conectar nosso sistema ao Facebook. Por favor, tente novamente mais tarde.");
      this.errorLog(e);
      this.processError(e);
    }
  }

  hasConnected(): boolean {
    return this.profilesConnected.length > 0;
  }

}

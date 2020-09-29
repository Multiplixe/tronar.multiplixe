import { Component, OnInit } from '@angular/core';
import { SocialMediaProfileDashboardBase } from '../social-media-profile-dashboard-base';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { FacebookService, InitParams, LoginResponse } from 'ngx-facebook';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';
import { SocialMediaProfileService } from 'src/app/services/social-media-profile.service';
import { AppInjector } from 'src/app/app-injector';
import { facebookEnvironment } from '../../../../environments/environment';

@Component({
  selector: 'app-social-media-profile-dashboard-facebook',
  templateUrl: './facebook.page.html'
})
export class FacebookPage extends SocialMediaProfileDashboardBase implements OnInit {

  private facebookService: FacebookService;
  private FB: any;
  private profileService: SocialMediaProfileService;

  public profilesConnected: SocialMediaProfile[] = [];
  public profile: SocialMediaProfile;  

  constructor() {
    super();
    this.facebookService = AppInjector.get(FacebookService)
    this.profileService = AppInjector.get(SocialMediaProfileService)
  }  

  async ngOnInit() {
    super.ngOnInit();

    let initParams: InitParams = {
      appId: facebookEnvironment.app_id,
      xfbml: facebookEnvironment.xfbml,
      version: facebookEnvironment.version
    };

    this.facebookService.init(initParams);

    this.load(SocialMediaEnum.facebook);
  }

  async connect() {

    try {

      var response: LoginResponse = await this.facebookService.login()

      if (response && response.status == "connected") {

        var userInfo = await this.facebookService.api("/me?fields=id,name,picture.width(500).height(500)", "get", { access_token: response.authResponse.accessToken })

        this.profile = new SocialMediaProfile();
        this.profile.profileId = userInfo.id;
        this.profile.name = userInfo.name;
        this.profile.token = JSON.stringify(response.authResponse);

        if (userInfo.picture &&
          userInfo.picture.data &&
          userInfo.picture.data.url) {
            this.profile.imageUrl = userInfo.picture.data.url;
        }

        await this.profileService.saveFacebook(this.profile);

        this.dashboard.profiles.push(this.profile);
      }
      else {
        this.showToast("Não foi possível fazer a conexão com o Facebook");
      }
    }
    catch (e) {
      super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
    }
  }  

}

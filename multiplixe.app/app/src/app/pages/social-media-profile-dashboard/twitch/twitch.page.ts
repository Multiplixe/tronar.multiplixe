import { Component, OnInit } from '@angular/core';
import { SocialMediaProfileDashboardBase } from '../social-media-profile-dashboard-base';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { AppInjector } from 'src/app/app-injector';
import { TwitchService } from 'src/app/services/twitch.service';

@Component({
  selector: 'app-social-media-profile-dashboard-twitch',
  templateUrl: './twitch.page.html'
})
export class TwitchPage extends SocialMediaProfileDashboardBase implements OnInit {

  private twitchService: TwitchService;

  constructor() {
    super();
    this.twitchService = AppInjector.get(TwitchService)
  }
  
  async ngOnInit() {
    super.ngOnInit();
    this.load(SocialMediaEnum.twitch);
  }

  async connect() {

    try {
      var envelope = await this.twitchService.authorize();

      window.location.href = envelope.item;
    }
    catch (e) {
      super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
    }

  }

}

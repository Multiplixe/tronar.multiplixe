import { Component, OnInit } from '@angular/core';
import { SocialMediaProfileDashboardBase } from '../social-media-profile-dashboard-base';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { TwitterService } from 'src/app/services/twitter.service';
import { AppInjector } from 'src/app/app-injector';

@Component({
  selector: 'app-social-media-profile-dashboard-twitter',
  templateUrl: './twitter.page.html'
})
export class TwitterPage extends SocialMediaProfileDashboardBase implements OnInit {

  private twitterService: TwitterService;

  constructor() {
    super();
    this.twitterService = AppInjector.get(TwitterService)
  }

  async ngOnInit() {
    super.ngOnInit();
    this.load(SocialMediaEnum.twitter);
  }

  async connect() {

    try {
      
      var envelope = await this.twitterService.authenticate();

      window.location.href = envelope.item;

    }
    catch (e) {
      super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
    }
  }  

}

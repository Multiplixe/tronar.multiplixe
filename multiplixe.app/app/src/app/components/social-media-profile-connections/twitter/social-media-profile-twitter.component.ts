import { Component, OnInit, Input } from '@angular/core';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';
import { BaseComponent } from '../../base.component';
import { AppInjector } from '../../../app-injector';
import { TwitterService } from 'src/app/services/twitter.service';

@Component({
  selector: 'app-social-media-profile-twitter',
  templateUrl: './social-media-profile-twitter.component.html'
})
export class SocialMediaProfileTwitterComponent
  extends BaseComponent
  implements OnInit {

  private twitterService: TwitterService;

  @Input()
  public profilesConnected: SocialMediaProfile[] = [];
  public profile: SocialMediaProfile;

  constructor() {
    super();
    this.twitterService = AppInjector.get(TwitterService)
  }

  ngOnInit() {
    if (this.hasConnected()) {
      this.profile = this.profilesConnected[0];
    }
  }

  async ok() {

    if (this.hasConnected()) {
      this.redirectRestrict("minhas-conexoes/twitter");
    }
    else {
      await this.connect();
    }
  }

  async connect() {

    try {
      var envelope = await this.twitterService.authenticate();

      window.location.href = envelope.item;

    }
    catch (e) {
      this.processError(e);
    }

  }

  hasConnected(): boolean {
    return this.profilesConnected.length > 0;
  }


}

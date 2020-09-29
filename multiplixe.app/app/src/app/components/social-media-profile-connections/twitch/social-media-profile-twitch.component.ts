import { Component, OnInit, Input } from '@angular/core';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';
import { BaseComponent } from '../../base.component';
import { AppInjector } from '../../../app-injector';
import { TwitchService } from 'src/app/services/twitch.service';

@Component({
  selector: 'app-social-media-profile-twitch',
  templateUrl: './social-media-profile-twitch.component.html'
})
export class SocialMediaProfileTwitchComponent
  extends BaseComponent
  implements OnInit {

  private twitchService: TwitchService;

  @Input()
  public profilesConnected: SocialMediaProfile[] = [];
  public profile: SocialMediaProfile;

  constructor() {
    super();
    this.twitchService = AppInjector.get(TwitchService)

  }

  ngOnInit() {

    if (this.hasConnected()) {
      this.profile = this.profilesConnected[0];
    }

  }

  async ok() {

    if (this.hasConnected()) {
      this.redirectRestrict("minhas-conexoes/twitch");
    }
    else {
      await this.connect();
    }
  }

  async connect() {

    try {
      var envelope = await this.twitchService.authorize();
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

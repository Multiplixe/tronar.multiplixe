import { Component, OnInit } from '@angular/core';
import { SocialMediaProfileDashboardBase } from '../social-media-profile-dashboard-base';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { YoutubeService } from 'src/app/services/youtube.service';
import { AppInjector } from 'src/app/app-injector';

@Component({
  selector: 'app-social-media-profile-dashboard-youtube',
  templateUrl: './youtube.page.html'
})
export class YoutubePage extends SocialMediaProfileDashboardBase implements OnInit {

  private youtubeService: YoutubeService;

  constructor() {
    super();
    this.youtubeService = AppInjector.get(YoutubeService)
  }

  async ngOnInit() {
    super.ngOnInit();
    this.load(SocialMediaEnum.youtube);
  }

  async connect() {
    window.location.href = this.youtubeService.oauth2Url();
  }

}

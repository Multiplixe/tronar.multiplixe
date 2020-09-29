import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from '../base.component';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { SocialMediaService } from 'src/app/services/social-media.service';
import { SocialMedia } from 'src/app/dtos/social-media';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';
import { AppInjector } from 'src/app/app-injector';

@Component({
  selector: 'app-social-media-connection-display',
  templateUrl: './social-media-connection-display.component.html'
})
export class SocialMediaConnectionDisplayComponent extends BaseComponent implements OnInit {

  @Input()
  public socialMediaEnum: SocialMediaEnum;

  @Input()
  public profile : SocialMediaProfile;

  public socialMedia: SocialMedia = SocialMedia.create();
  private socialMediaService: SocialMediaService;

  constructor() {
    super();
    this.socialMediaService = AppInjector.get(SocialMediaService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.socialMedia = this.socialMediaService.get(this.socialMediaEnum);
  }

}

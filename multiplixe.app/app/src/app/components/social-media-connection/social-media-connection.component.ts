import { Component, OnInit, Input } from '@angular/core';
import { SocialMediaService } from 'src/app/services/social-media.service';
import { BaseComponent } from '../base.component';
import { AppInjector } from 'src/app/app-injector';
import { SocialMedia } from 'src/app/dtos/social-media';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';

@Component({
  selector: 'app-social-media-connection',
  templateUrl: './social-media-connection.component.html'
})
export class SocialMediaConnectionComponent extends BaseComponent implements OnInit {

  @Input()
  public socialMediaEnum: SocialMediaEnum;

  @Input()
  public profilesConnected: SocialMediaProfile[] = [];

  private socialMediaService: SocialMediaService;
  public socialMedia: SocialMedia = SocialMedia.create();

  constructor() {
    super();

    this.socialMediaService = AppInjector.get(SocialMediaService);

  }

  ngOnInit() {
    super.ngOnInit();
    this.socialMedia = this.socialMediaService.get(this.socialMediaEnum);
  }

  link() {
    this.redirectRestrict('minhas-conexoes/' + this.socialMedia.url);
  }
}

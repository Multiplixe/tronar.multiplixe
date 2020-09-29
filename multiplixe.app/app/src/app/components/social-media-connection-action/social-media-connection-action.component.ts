import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BaseComponent } from '../base.component';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { SocialMedia } from 'src/app/dtos/social-media';
import { SocialMediaService } from 'src/app/services/social-media.service';
import { AppInjector } from 'src/app/app-injector';

@Component({
  selector: 'app-social-media-connection-action',
  templateUrl: './social-media-connection-action.component.html'
})
export class SocialMediaConnectionActionComponent extends BaseComponent implements OnInit {

  @Input()
  public socialMediaEnum: SocialMediaEnum;

  public socialMedia: SocialMedia = SocialMedia.create();
  private socialMediaService: SocialMediaService;

  @Output()
  public actionChild = new EventEmitter();

  constructor() {
    super();
    this.socialMediaService = AppInjector.get(SocialMediaService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.socialMedia = this.socialMediaService.get(this.socialMediaEnum);
  }

  action() {
    this.actionChild.next();
  }

}


import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from '../base.component';
import { SocialMediaProfile } from 'src/app/dtos/social-media-profile';

@Component({
  selector: 'app-social-media-profile-dashboard',
  templateUrl: './social-media-profile-dashboard.component.html'
})
export class SocialMediaProfileDashboardComponent
  extends BaseComponent
  implements OnInit {

  @Input()
  public profile: SocialMediaProfile = SocialMediaProfile.create();



  constructor() {
    super();
  }

  ngOnInit() { 
  } 

}

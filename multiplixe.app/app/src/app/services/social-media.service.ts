import { Injectable } from '@angular/core';
import { SocialMediaEnum } from '../enums/social-media.enum';
import { SocialMedia } from '../dtos/social-media';

@Injectable({
  providedIn: 'root'
})
export class SocialMediaService {

  constructor() {
  }

  get(e: SocialMediaEnum) {

    let socialMedia = SocialMedia.create();

    socialMedia.id = e.valueOf();

    if (e == SocialMediaEnum.twitter) {
      socialMedia.name = "Twitter";
      socialMedia.ico = "icon_twitter.png";
      socialMedia.url = "twitter";
    }
    else if (e == SocialMediaEnum.facebook) {
      socialMedia.name = "Facebook";
      socialMedia.ico = "icon_fb.png";
      socialMedia.url = "facebook";
    }
    else if (e == SocialMediaEnum.twitch) {
      socialMedia.name = "Twitch";
      socialMedia.ico = "icon_twitch.png";
      socialMedia.url = "twitch";
    }    
    else if (e == SocialMediaEnum.youtube) {
      socialMedia.name = "Youtube";
      socialMedia.ico = "icon_youtube.png";
      socialMedia.url = "youtube";
    }    
    return socialMedia;
  }
}

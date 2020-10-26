import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { FacebookService } from './facebook.service';
import { ISocialMedia } from './social-media.interface';
import { TwitterService } from './twitter.service';

export function SocialMediaFactory(redesocial: SocialMediaEnum): ISocialMedia {

    var service: ISocialMedia;

    if (redesocial == SocialMediaEnum.facebook) {
        service  = new FacebookService();
    }
    else if (redesocial == SocialMediaEnum.twitter) {
        service  = new TwitterService();
    }

    return service;

}
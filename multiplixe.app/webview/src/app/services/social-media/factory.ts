import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { FacebookService } from './facebook.service';
import { ISocialMedia } from './social-media.interface';
import { TwitchService } from './twitch.service';
import { TwitterService } from './twitter.service';
import { YoutubeService } from './youtube.service';

export function SocialMediaFactory(redesocial: SocialMediaEnum): ISocialMedia {

    var service: ISocialMedia;

    if (redesocial == SocialMediaEnum.facebook) {
        service  = new FacebookService();
    }
    else if (redesocial == SocialMediaEnum.twitter) {
        service  = new TwitterService();
    }
    else if (redesocial == SocialMediaEnum.twitch) {
        service  = new TwitchService();
    }
    else if (redesocial == SocialMediaEnum.youtube) {
        service  = new YoutubeService();
    }

    return service;

}
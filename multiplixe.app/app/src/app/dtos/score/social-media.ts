import { GenericItem } from '../generic-item';
import { SocialMediaProfile } from '../social-media-profile';

export class SocialMedia extends GenericItem {
   public percent: number = 0;
   public points: number = 0;
   public connected: boolean = false;
   public profile: SocialMediaProfile;

   static create() {
      return new SocialMedia();
   }
}
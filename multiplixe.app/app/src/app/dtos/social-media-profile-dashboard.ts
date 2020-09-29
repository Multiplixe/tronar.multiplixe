import { SocialMediaProfile } from './social-media-profile';

export class SocialMediaProfileDashboard {
   public profiles: SocialMediaProfile[] = [];
   public profile: SocialMediaProfile = SocialMediaProfile.create();

   static create() {
      return new SocialMediaProfileDashboard();
   }
}
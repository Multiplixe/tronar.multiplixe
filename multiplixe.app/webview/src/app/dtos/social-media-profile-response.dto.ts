import { SocialMediaProfileDto } from './social-media-profile.dto';

export class SocialMediaProfileResponseDto {
   public profiles: SocialMediaProfileDto[] = [];
   public profile: SocialMediaProfileDto;

   static create() {
      return new SocialMediaProfileResponseDto();
   }
}
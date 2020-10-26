import { SocialMediaEnum } from '../enums/social-media.enum';

export class SocialMediaProfileDto {
    constructor(
        public socialMedia: SocialMediaEnum,
        public profileId: string,
        public name: string,
        public imageUrl: string,
        public token: string) {
    }

    static create(socialMedia: SocialMediaEnum, profileId: string, name: string, imageUrl: string, token: string) {
        return new SocialMediaProfileDto(socialMedia, profileId, name, imageUrl, token);
    }


}
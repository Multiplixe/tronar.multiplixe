import { SocialMediaProfileResponseDto } from 'src/app/dtos/social-media-profile-response.dto';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { Envelope } from 'src/app/envelopes/envelope';

export interface ISocialMedia {
    id: SocialMediaEnum;
    name: string;
    ico: string;
    url: string;
    explainText: string;
    explainItemsText: string[];

    connect(): void;
    save(profile: SocialMediaProfileDto);
    get(): Promise<Envelope<SocialMediaProfileResponseDto>>;

}
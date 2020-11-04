import { SocialMediaProfileResponseDto } from 'src/app/dtos/social-media-profile-response.dto';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { Envelope } from 'src/app/envelopes/envelope';
import { BaseService } from '../base.service';
import { HttpService } from '../http.service';

export class BaseSocialMediaService extends BaseService {
    public id: SocialMediaEnum = SocialMediaEnum.none;
    public name: string = '';
    public ico: string = '';
    public url: string = '';
    public explainText: string = '';
    public explainItemsText: string[] = [];

    public httpService: HttpService;

    constructor() {
        super();
        this.httpService = new HttpService();
    }

    async save(profile: SocialMediaProfileDto) {
        return this.httpService.postAPI<SocialMediaProfileDto>(this.getUrl(profile.socialMedia), profile);
    }

    async get(socialMedia: SocialMediaEnum) {
        return this.httpService.getAPI<SocialMediaProfileResponseDto>(this.getUrl(socialMedia));
    }

    async _get(socialMedia: SocialMediaEnum) {
        return this.httpService.getAPI<SocialMediaProfileResponseDto>(this.getUrl(socialMedia));
    }

    private getUrl(socialMedia: SocialMediaEnum) {
        return "restrito/perfil/" + SocialMediaEnum[socialMedia];
    }

    async processError(error: any, message: string, successCallback: Function = null) {
        return super.processError(error, message, successCallback);
    }

}
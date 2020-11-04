import { Injectable } from '@angular/core';
import { BaseSocialMediaService } from './base-social-media.service';
import { ISocialMedia } from './social-media.interface';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';

@Injectable({
    providedIn: "root"
})
export class TwitterService
    extends BaseSocialMediaService
    implements ISocialMedia {

    constructor() {

        super();

        this.id = SocialMediaEnum.twitter;
        this.name = "Twitter";
        this.ico = "icon_twitter.png";
        this.explainText = "Para conquistar Pixels com o Twitter você precisa interagir com a conta da Vorax da seguinte forma:";
        this.explainItemsText.push("Curtir os tweets (retweet não são contabilizados)");

    }

    async save(profile: SocialMediaProfileDto) {
        return super.save(profile);
    }

    async get() {
        return super.get(SocialMediaEnum.twitter);
    }

    async connect() {

        try {
            var response = await this.getAuthUrl();
            window.location.href = response.item;
        }
        catch (e) {
            super.processError(e, 'Ocorreu algum erro ao tentar se conectar.', async () => { 
                this.connect();
            });
        }

    }

    private async getAuthUrl() {
        return this.httpService.getAPI<string>('/restrito/twitter/oauth/authenticate');
    }

    async process(token: string, verifier: string) {
        return this.httpService.getAPI<any>('/restrito/twitter/oauth/process/' + token + '/' + verifier);
    }


}

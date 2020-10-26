import { Injectable } from '@angular/core';
import { BaseSocialMediaService } from './base-social-media.service';
import { ISocialMedia } from './social-media.interface';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';

@Injectable({
    providedIn: "root"
})
export class TwitchService
    extends BaseSocialMediaService
    implements ISocialMedia {

    constructor() {

        super();

        this.id = SocialMediaEnum.twitch;
        this.name = "Twitch";
        this.ico = "icon_twitch.png";
        this.explainText = "Para conquistar Pixels com a Twitch é bem simples, basta você assistir as transmissões do canal da Vorax:";
        this.explainItemsText.push("Você precisa habilitar a Extension do canal e pronto, quanto mais assistir, mais Pixels ganha.");

    }

    async save(profile: SocialMediaProfileDto) {
        return super.save(profile);
    }

    async get() {
        return super.get(SocialMediaEnum.twitch);
    }

    async connect() {

        try {
            var response = await this.getAuthUrl();
            window.location.href = response.item;
        }
        catch (e) {
            super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
        }

    }

    private async getAuthUrl() {
        return this.httpService.getAPI<string>('/restrito/twitch/oauth/authorize');
    }

    async process(code: string) {
        return this.httpService.getAPI<any>('/restrito/twitch/oauth/process/' + code);
    }

}

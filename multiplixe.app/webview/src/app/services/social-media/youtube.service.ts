import { Injectable } from '@angular/core';
import { BaseSocialMediaService } from './base-social-media.service';
import { ISocialMedia } from './social-media.interface';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';
import { youtubeEnvironment } from 'src/environments/environment'

@Injectable({
    providedIn: "root"
})
export class YoutubeService
    extends BaseSocialMediaService
    implements ISocialMedia {

    constructor() {

        super();

        this.id = SocialMediaEnum.youtube;
        this.name = "Youtube";
        this.ico = "icon_youtube.png";
        this.explainText = "Para conquistar Pixels com o Youtube você precisa interagir a conta da Vorax:";
        this.explainItemsText.push("Use as hastags #falkol e #goblue nos chats das lives");
    }

    async save(profile: SocialMediaProfileDto) {
        return super.save(profile);
    }

    async get() {
        return super.get(SocialMediaEnum.youtube);
    }

    async connect() {

        try {
            await super.isAuthenticated();
            window.location.href = youtubeEnvironment.oauth2Url;
        }
        catch (e) {
            super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
        }

    }

    async process(token: any) {

        return new Promise(async (resolve, rejects) => {

            if (!token.access_token) {
                rejects();
                return;
            }

            let response = await this.getUserInfo(token.access_token);

            let profile = this.parseProfile(response, token)

            if (profile) {
                this.save(profile)
                resolve(profile);
            }
            else {
                rejects();
            }

        });

    }

    private getUserInfo(token: string) {

        let url = youtubeEnvironment.userInfoUrl + token;

        return this.httpService.get<any>(url, null);

    }

    private parseProfile(user: any, token: any) {

        let profile: SocialMediaProfileDto = null;

        if (user &&
            user.items &&
            user.items.length) {

            let item = user.items[0];

            profile = new SocialMediaProfileDto(
                SocialMediaEnum.youtube,
                item.id,
                item.snippet.title,
                item.snippet.thumbnails.default.url,
                JSON.stringify(token));
        }

        return profile;
    }


}

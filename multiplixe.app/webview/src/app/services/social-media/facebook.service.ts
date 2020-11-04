import { Injectable } from '@angular/core';
import { BaseSocialMediaService } from './base-social-media.service';
import { ISocialMedia } from './social-media.interface';
import { FacebookService as FacebookSDK, LoginResponse } from 'ngx-facebook';
import { SocialMediaProfileDto } from 'src/app/dtos/social-media-profile.dto';
import { facebookEnvironment } from '../../../environments/environment';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';

@Injectable({
    providedIn: "root"
})
export class FacebookService
    extends BaseSocialMediaService
    implements ISocialMedia {

    public FB: any;
    private facebookSDK: FacebookSDK;

    constructor() {

        super();

        this.id = SocialMediaEnum.facebook;
        this.name = "Facebook";
        this.ico = "icon_fb.png";
        this.explainText = "Para conquistar Pixels com o Facebook você precisa interagir com a conta da Vorax da seguinte forma:";
        this.explainItemsText.push("Curtir os posts");

        this.facebookSDK = new FacebookSDK();

        this.facebookSDK.init({
            appId: facebookEnvironment.app_id,
            xfbml: facebookEnvironment.xfbml,
            version: facebookEnvironment.version
        });
    }

    async saveProfile(profile: SocialMediaProfileDto) {

        try {
            await this.save(profile);
            window.location.reload();
        }
        catch (e) {
            super.processError(e, 'Ocorreu algum erro ao tentar se conectar.', async () => {
                await this.saveProfile(profile);
            });
        }
    }

    async get() {
        return super.get(SocialMediaEnum.facebook);
    }

    async connect() {

        try {

            var response: LoginResponse = await this.facebookSDK.login({
                return_scopes: true,
                enable_profile_selector: true,
                scope: 'pages_show_list,pages_messaging,pages_read_engagement,pages_manage_metadata'
            });

            if (response && response.status == "connected") {

                console.log("response", response)

                var userInfo = await this.facebookSDK.api("/me?fields=id,name,picture,accounts{app_id,id,access_token,name,category,username,website,page_token,cover,picture}", "get", { access_token: response.authResponse.accessToken })

                let imageUrl = '';

                if (userInfo.picture &&
                    userInfo.picture.data &&
                    userInfo.picture.data.url) {
                    imageUrl = userInfo.picture.data.url;
                }

                let token = JSON.stringify(response);

                let profile = SocialMediaProfileDto.create(SocialMediaEnum.facebook, userInfo.id, userInfo.name, imageUrl, token);

                await this.saveProfile(profile);
            }
            else {
                console.log("NOTIFICAR APP", "Não foi possível fazer a conexão com o Facebook");
                //this.showToast("Não foi possível fazer a conexão com o Facebook");
            }
        }
        catch (e) {
            super.processError(e, 'Ocorreu algum erro ao tentar se conectar.');
        }
    }
}

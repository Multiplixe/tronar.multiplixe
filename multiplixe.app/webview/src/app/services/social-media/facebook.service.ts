import { Injectable } from '@angular/core';
import { BaseSocialMediaService } from './base-social-media.service';
import { ISocialMedia } from './social-media.interface';
import { SocialMediaEnum } from 'src/app/enums/social-media.enum';

@Injectable({
    providedIn: "root"
})
export class FacebookService
    extends BaseSocialMediaService
    implements ISocialMedia {

    public FB: any;

    constructor() {

        super();

        this.id = SocialMediaEnum.facebook;
        this.name = "Facebook";
        this.ico = "icon_fb.png";
        this.explainText = "Para conquistar Pixels com o Facebook você precisa interagir com a conta da Vorax da seguinte forma:";
        this.explainItemsText.push("Curtir os posts");
    }

    async get() {
        return super.get(SocialMediaEnum.facebook);
    }

    private async getAuthUrl() {
        return this.httpService.getAPI<string>('restrito/facebook/oauth/authorize');
    }

    async process(code: string, empresaId: string, username: string) {
        
        let o = { 
            "code": code,
            "empresaId" : empresaId,
            "username" : username
        };

        return this.httpService.postAPI<any>('restrito/facebook/oauth/process/', o);
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
}

import { Injectable } from '@angular/core';

import { BaseService } from './base.service';
import { HttpService } from './http.service';
import { AppInjector } from '../app-injector';
import { SocialMediaProfile } from '../dtos/social-media-profile';
import { SocialMediaConnections } from '../dtos/social-media-connections';
import { SocialMediaProfileDashboard } from '../dtos/social-media-profile-dashboard';
import { SocialMediaEnum } from '../enums/social-media.enum';

@Injectable({
    providedIn: 'root'
})
export class SocialMediaProfileService extends BaseService {

    private httpService: HttpService;

    constructor() {
        super()

        this.httpService = AppInjector.get(HttpService)

    }

    saveFacebook(profile: SocialMediaProfile) {
        return this.httpService.postAPI<SocialMediaProfile>("restrito/perfil/facebook", profile);
    }

    saveTwitter(profile: SocialMediaProfile) {
        return this.httpService.postAPI<SocialMediaProfile>("restrito/perfil/twitter", profile);
    }

    saveTwitch(profile: SocialMediaProfile) {
        return this.httpService.postAPI<SocialMediaProfile>("restrito/perfil/twitch", profile);
    }    

    getProfilesConnection() {
        return this.httpService.getAPI<SocialMediaConnections>("restrito/perfil/perfis-conectados");
    }

   getSocialMediaProfile(e:SocialMediaEnum) {
        return this.httpService.getAPI<SocialMediaProfileDashboard>("restrito/perfil/" + SocialMediaEnum[e]);
    }   

}
